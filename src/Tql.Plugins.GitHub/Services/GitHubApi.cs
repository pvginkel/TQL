﻿using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Octokit;
using Tql.Abstractions;
using Tql.Utilities;
using GraphQLConnection = Octokit.GraphQL.Connection;
using IWin32Window = System.Windows.Forms.IWin32Window;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace Tql.Plugins.GitHub.Services;

internal class GitHubApi(
    ILogger<GitHubApi> logger,
    IUI ui,
    ConfigurationManager configurationManager,
    IEncryption encryption
)
{
    private const string ClientId = "b5cf8dfb10c01dcfd22f";
    private const string Scope = "repo,project,read:org";
    private const string RedirectUri = "http://127.0.0.1:23119/";

    private readonly AsyncLock _lock = new();
    private readonly Dictionary<Guid, GitHubClient> _clients = new();
    private readonly Dictionary<Guid, GraphQLConnection> _connections = new();

    private static string GetClientSecret()
    {
#if DEBUG
        return Environment.GetEnvironmentVariable("GITHUB_OAUTH_SECRET")!;
#else
        return Encoding
            .UTF8
            .GetString(Convert.FromBase64String("""<![SECRET[GITHUB_OAUTH_SECRET]]>"""));
#endif
    }

    public async Task<GitHubClient> GetClient(Guid id)
    {
        using (await _lock.LockAsync())
        {
            return await GetClientUnsafe(id);
        }
    }

    public async Task<GraphQLConnection> GetConnection(Guid id)
    {
        using (await _lock.LockAsync())
        {
            if (!_connections.TryGetValue(id, out var connection))
            {
                var appVersion = GetType().Assembly.GetName().Version!;

                var client = await GetClientUnsafe(id);

                connection = new GraphQLConnection(
                    new Octokit.GraphQL.ProductHeaderValue("TQL", appVersion.ToString()),
                    client.Connection.Credentials.Password
                );

                _connections.Add(id, connection);
            }

            return connection;
        }
    }

    private async Task<GitHubClient> GetClientUnsafe(Guid id)
    {
        if (!_clients.TryGetValue(id, out var client))
        {
            var connection = configurationManager.Configuration.Connections.Single(p => p.Id == id);

            try
            {
                client = await CreateClient(connection);
            }
            catch
            {
                ui.ShowNotificationBar(
                    $"{GitHubPlugin.Id}/ConnectionFailed/{id}",
                    string.Format(
                        Labels.GitHubApi_UnableToConnect,
                        string.Format(Labels.GitHubApi_ResourceName, connection.Name)
                    ),
                    () => RetryConnect(id)
                );
                throw;
            }

            _clients[id] = client;
        }

        return client;
    }

    private async void RetryConnect(Guid id)
    {
        try
        {
            await GetClient(id);
        }
        catch
        {
            // Ignore.
        }
    }

    private async Task<GitHubClient> CreateClient(Connection connection)
    {
        var appVersion = GetType().Assembly.GetName().Version!;

        var client = new GitHubClient(new ProductHeaderValue("TQL", appVersion.ToString()));

        // PAT tokens are only used by unit tests.

        if (connection.PatToken != null)
        {
            client.Credentials = new Credentials(connection.PatToken, AuthenticationType.Bearer);
            return client;
        }

        var credentials = ReadCredentials(connection.Id);

        // Only use the token if the scope is the same.

        if (credentials is { Scope: Scope })
        {
            client.Credentials = new Credentials(
                credentials.AccessToken,
                AuthenticationType.Bearer
            );

            try
            {
                await client.User.Current();
            }
            catch
            {
                client.Credentials = Credentials.Anonymous;
            }
        }

        if (client.Credentials.AuthenticationType != AuthenticationType.Bearer)
        {
            await ui.PerformInteractiveAuthentication(
                new InteractiveAuthentication(
                    string.Format(Labels.GitHubApi_ResourceName, connection.Name),
                    client,
                    ui,
                    logger
                )
            );

            if (client.Credentials.AuthenticationType != AuthenticationType.Bearer)
                throw new GitHubAuthenticationException("Authentication failed");

            WriteCredentials(connection.Id, new CredentialsDto(client.Credentials.Password, Scope));
        }

        return client;
    }

    private CredentialsDto? ReadCredentials(Guid id)
    {
        try
        {
            var connection = configurationManager.Configuration.Connections.Single(p => p.Id == id);

            var credentials = encryption.DecryptString(connection.ProtectedCredentials);
            if (credentials == null)
                return null;

            return JsonSerializer.Deserialize<CredentialsDto>(credentials);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load credentials for connection {ConnectionId}", id);
        }

        return null;
    }

    private void WriteCredentials(Guid id, CredentialsDto credentialsDto)
    {
        var json = JsonSerializer.Serialize(credentialsDto);

        var protectedCredentials = encryption.EncryptString(json)!;

        configurationManager.UpdateCredentials(id, protectedCredentials);
    }

    private class InteractiveAuthentication(
        string resourceName,
        GitHubClient client,
        IUI ui,
        ILogger logger
    ) : IInteractiveAuthentication
    {
        public string ResourceName { get; } = resourceName;

        public async Task Authenticate(IWin32Window owner)
        {
            var workflow = new GitHubOAuthWorkflow(
                ClientId,
                GetClientSecret(),
                Scope,
                RedirectUri,
                ui,
                logger
            );

            client.Credentials = await workflow.Authorize();

            await client.User.Current();
        }
    }

    private record CredentialsDto(string AccessToken, string Scope);
}
