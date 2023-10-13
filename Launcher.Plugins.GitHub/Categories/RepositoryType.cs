﻿using Launcher.Abstractions;
using Launcher.Plugins.GitHub.Services;
using Launcher.Utilities;

namespace Launcher.Plugins.GitHub.Categories;

internal class RepositoryType : IMatchType
{
    private readonly GitHubApi _api;
    public Guid Id => TypeIds.Repository.Id;

    public RepositoryType(GitHubApi api)
    {
        _api = api;
    }

    public IMatch Deserialize(string json)
    {
        return new RepositoryMatch(JsonSerializer.Deserialize<RepositoryMatchDto>(json)!, _api);
    }
}
