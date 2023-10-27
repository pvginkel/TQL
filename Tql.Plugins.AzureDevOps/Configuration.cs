﻿namespace Tql.Plugins.AzureDevOps;

internal record Configuration(ImmutableArray<Connection> Connections)
{
    public static readonly Configuration Empty = new(ImmutableArray<Connection>.Empty);

    public static Configuration FromJson(string? configuration)
    {
        if (configuration == null)
            return Empty;

        return JsonSerializer.Deserialize<Configuration>(configuration)!;
    }

    public string ToJson() => JsonSerializer.Serialize(this);

    public bool HasConnection(string url)
    {
        return Connections.Any(p => p.Url == url);
    }
}

internal record Connection(string Name, string Url);
