﻿using Tql.Abstractions;
using Tql.Plugins.MicrosoftTeams.Services;

namespace Tql.Plugins.MicrosoftTeams.Categories;

internal class EmailsMatch : PersonMatchBase
{
    public override string Text { get; }
    public override ImageSource Icon => Images.Teams;
    public override MatchTypeId TypeId => TypeIds.Emails;

    public EmailsMatch(string text, IPeopleDirectory peopleDirectory)
        : base(peopleDirectory)
    {
        Text = text;
    }

    protected override IMatch CreateMatch(PersonDto dto)
    {
        return new EmailMatch(dto);
    }
}
