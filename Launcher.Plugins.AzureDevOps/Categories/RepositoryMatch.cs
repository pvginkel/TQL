﻿using Launcher.Abstractions;
using Launcher.Plugins.AzureDevOps.Services;
using Launcher.Plugins.AzureDevOps.Support;
using Launcher.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.Text.Json.Nodes;

namespace Launcher.Plugins.AzureDevOps.Categories;

internal class RepositoryMatch
    : IRunnableMatch,
        ISearchableMatch,
        ISerializableMatch,
        ICopyableMatch
{
    private readonly RepositoryMatchDto _dto;

    public string Text => $"{_dto.ProjectName}/{_dto.RepositoryName}";
    public ImageSource Icon => Images.Repositories;
    public MatchTypeId TypeId => TypeIds.Repository;

    public RepositoryMatch(RepositoryMatchDto dto)
    {
        _dto = dto;
    }

    public async Task<IEnumerable<IMatch>> Search(
        ISearchContext context,
        string text,
        CancellationToken cancellationToken
    )
    {
        var cache = context.GetDataCached($"{GetType().FullName}:{_dto}", GetRepositoryFilePaths);

        if (!cache.IsCompleted)
            await context.DebounceDelay(cancellationToken);

        var filePaths = await cache;

        return await Task.Run(
            () =>
                context.Filter(
                    context
                        .Prefilter(filePaths)
                        .Select(
                            p =>
                                new RepositoryFilePathMatch(new RepositoryFilePathMatchDto(_dto, p))
                        )
                ),
            cancellationToken
        );
    }

    private async Task<ImmutableArray<string>> GetRepositoryFilePaths(
        IServiceProvider serviceProvider
    )
    {
        var gitClient = await serviceProvider
            .GetRequiredService<AzureDevOpsApi>()
            .GetClient<GitHttpClient>(_dto.Url);

        using var stream = await gitClient
            .GetClient()
            .GetStreamAsync(
                $"{_dto.ProjectName}/_apis/git/repositories/{_dto.RepositoryName}/FilePaths"
            );

        var node = JsonNode.Parse(stream);

        return node!.AsObject()["paths"]!
            .AsArray()
            .Select(p => p!.GetValue<string>())
            .ToImmutableArray();
    }

    public Task Run(IServiceProvider serviceProvider, Window owner)
    {
        serviceProvider.GetRequiredService<IUI>().LaunchUrl(_dto.GetUrl());

        return Task.CompletedTask;
    }

    public string Serialize()
    {
        return JsonSerializer.Serialize(_dto);
    }

    public Task Copy(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<IClipboard>().CopyUri(Text, _dto.GetUrl());

        return Task.CompletedTask;
    }
}

internal record RepositoryMatchDto(string Url, string ProjectName, string RepositoryName)
{
    public string GetUrl() =>
        $"{Url.TrimEnd('/')}/{Uri.EscapeDataString(ProjectName)}/_git/{Uri.EscapeDataString(RepositoryName)}";
};
