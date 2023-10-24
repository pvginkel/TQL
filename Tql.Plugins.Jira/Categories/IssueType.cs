﻿using Tql.Abstractions;
using Tql.Plugins.Jira.Services;
using Tql.Utilities;

namespace Tql.Plugins.Jira.Categories;

internal class IssueType : IMatchType
{
    private readonly IconCacheManager _iconCacheManager;

    public Guid Id => TypeIds.Issue.Id;

    public IssueType(IconCacheManager iconCacheManager)
    {
        _iconCacheManager = iconCacheManager;
    }

    public IMatch Deserialize(string json)
    {
        return new IssueMatch(JsonSerializer.Deserialize<IssueMatchDto>(json)!, _iconCacheManager);
    }
}
