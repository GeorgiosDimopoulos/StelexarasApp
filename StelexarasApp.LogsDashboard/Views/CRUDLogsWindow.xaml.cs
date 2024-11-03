using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Logs;
using System.Windows;

namespace StelexarasApp.LogsDashboard.Views;

/// <summary>
/// Interaction logic for CRUDLogsPage.xaml
/// </summary>
public partial class CRUDLogsWindow : Window
{
    public CrudLogEntry [] CrudLogEntries { get; set; }

    public CRUDLogsWindow()
    {
        InitializeComponent();

        DataContext = this;
        CrudLogEntries = [];

        InitializeCrudLogs();

        LogsDataGrid.Items.Clear();
        LogsDataGrid.ItemsSource = CrudLogEntries;
    }

    private void InitializeCrudLogs()
    {
        if (LogsDataGrid.ItemsSource != null)
        {
            LogsDataGrid.ItemsSource = null;
        }
        var logsStrg = LogFileWriter.ReadLogs();
        CrudLogEntries = new CrudLogEntry [logsStrg.Length];

        for (int i = 0; i < logsStrg.Length; i++)
        {
            var log = logsStrg [i].Split(',');
            CrudLogEntries [i] = new CrudLogEntry
            {
                CrudType = GetLogEntryType(log [0]),
                MethodName = log [1],
                Message = log [2],
                Timestamp = DateTime.Parse(log [3])
            };
        }
    }

    private static CrudType GetLogEntryType(string v)
    {
        return v switch
        {
            "CREATE" => CrudType.Create,
            "READ" => CrudType.Read,
            "UPDATE" => CrudType.Update,
            "DELETE" => CrudType.Delete,
            _ => CrudType.Unknown,
        };
    }
}
