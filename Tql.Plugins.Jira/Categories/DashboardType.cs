﻿using Tql.Abstractions;
using Tql.Plugins.Jira.Services;
using Tql.Utilities;

namespace Tql.Plugins.Jira.Categories;

internal class DashboardType : MatchType<DashboardMatch, DashboardMatchDto>
{
    private readonly ConfigurationManager _configurationManager;

    public override Guid Id => TypeIds.Dashboard.Id;

    public DashboardType(
        IMatchFactory<DashboardMatch, DashboardMatchDto> factory,
        ConfigurationManager configurationManager
    )
        : base(factory)
    {
        _configurationManager = configurationManager;
    }

    protected override bool IsValid(DashboardMatchDto dto) =>
        _configurationManager.Configuration.HasConnection(dto.Url);
}
