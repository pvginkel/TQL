﻿using Tql.Abstractions;
using Tql.Plugins.Azure.Services;

namespace Tql.Plugins.Azure.ConfigurationUI;

internal partial class ConfigurationControl : IConfigurationPage
{
    private readonly ConfigurationManager _configurationManager;
    private readonly IUI _ui;

    private new ConfigurationDto DataContext => (ConfigurationDto)base.DataContext;

    public Guid PageId => AzurePlugin.ConfigurationPageId;
    public string Title => Labels.ConfigurationControl_General;
    public ConfigurationPageMode PageMode => ConfigurationPageMode.AutoSize;

    public ConfigurationControl(ConfigurationManager configurationManager, IUI ui)
    {
        _configurationManager = configurationManager;
        _ui = ui;

        InitializeComponent();

        base.DataContext = ConfigurationDto.FromConfiguration(configurationManager.Configuration);
    }

    public void Initialize(IConfigurationPageContext context) { }

    public Task<SaveStatus> Save()
    {
        _configurationManager.UpdateConfiguration(DataContext.ToConfiguration());

        return Task.FromResult(SaveStatus.Success);
    }

    private void _add_Click(object? sender, RoutedEventArgs e) => Edit(null);

    private void _edit_Click(object sender, RoutedEventArgs e) =>
        Edit((ConnectionDto)_connections.SelectedItem);

    private void Edit(ConnectionDto? connection)
    {
        var editConnection = connection?.Clone() ?? new ConnectionDto(Guid.NewGuid());

        var window = new EditWindow(_ui)
        {
            Owner = Window.GetWindow(this),
            DataContext = editConnection
        };

        if (window.ShowDialog().GetValueOrDefault())
        {
            if (connection != null)
                DataContext.Connections[_connections.SelectedIndex] = editConnection;
            else
                DataContext.Connections.Add(editConnection);
        }
    }

    private void _delete_Click(object? sender, RoutedEventArgs e)
    {
        DataContext.Connections.Remove((ConnectionDto)_connections.SelectedItem);
    }

    private void _documentation_Click(object? sender, RoutedEventArgs e)
    {
        _ui.OpenUrl("https://github.com/TQLApp/TQL/wiki/Azure-plugin");
    }
}
