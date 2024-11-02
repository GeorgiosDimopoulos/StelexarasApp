using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Logs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StelexarasApp.LogsDashboard;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public LogEntry [] LogEntries { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;
        LogEntries = [];

        InitializeLogs();

        LogsDataGrid.Items.Clear();
        LogsDataGrid.ItemsSource = LogEntries;
    }

    private void InitializeLogs()
    {
        if (LogsDataGrid.ItemsSource != null)
        {
            LogsDataGrid.ItemsSource = null;
        }

        var logsStrg = LogFileWriter.ReadLogs();
        LogEntries = new LogEntry [logsStrg.Length];

        for (int i = 0; i < logsStrg.Length; i++)
        {
            var log = logsStrg [i].Split(',');
            LogEntries [i] = new LogEntry
            {
                Type = GetLogEntryType(log [0]),
                MethodName = log [1],
                Message = log [2],
                Timestamp = DateTime.Parse(log [3])
            };
        }
    }

    private static LogErrorType GetLogEntryType(string v)
    {
        return v switch
        {
            "DB ERROR" => LogErrorType.DbError,
            "VALIDATION ERROR" => LogErrorType.ValidationError,
            "UI WARNING" => LogErrorType.UiWarning,
            "SERVICE ERROR" => LogErrorType.ValidationError,
            _ => LogErrorType.UnknownError,
        };
    }

    private void LogsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        if (e.Row.DataContext is LogEntry logEntry)
        {
            if (logEntry.Type == LogErrorType.DbError)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightCoral);
            }
            else if (logEntry.Type == LogErrorType.ServiceError)
            {
                e.Row.Background = new SolidColorBrush(Colors.OrangeRed);
            }
            else if (logEntry.Type == LogErrorType.UiWarning)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightYellow);
            }
            else if (logEntry.Type == LogErrorType.ValidationError)
            {
                e.Row.Background = new SolidColorBrush(Colors.IndianRed);
            }
            else
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
            }
        }
    }

}