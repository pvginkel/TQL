using Tql.Abstractions;
using Tql.Plugins.MicrosoftTeams.Services;
using Tql.Utilities;

namespace Tql.Plugins.MicrosoftTeams.Categories;

internal class TeamsCallType : IMatchType
{
    private readonly ConfigurationManager _configurationManager;

    public Guid Id => TypeIds.TeamsCall.Id;

    public TeamsCallType(ConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public IMatch? Deserialize(string json)
    {
        var dto = JsonSerializer.Deserialize<PersonDto>(json)!;
        if (!_configurationManager.Configuration.HasDirectory(dto.DirectoryId))
            return null;

        return new TeamsCallMatch(dto);
    }
}
