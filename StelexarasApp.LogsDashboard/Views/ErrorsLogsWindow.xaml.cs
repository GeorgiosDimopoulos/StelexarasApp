using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Logs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StelexarasApp.LogsDashboard.Views;

/// <summary>
/// Interaction logic for ErrorsLogsPage.xaml
/// </summary>
public partial class ErrorsLogsWindow : Window
{
    public ErrorLogEntry [] ErrorLogEntries { get; set; }

    public ErrorsLogsWindow()
    {
        InitializeComponent();

        DataContext = this;
        ErrorLogEntries = [];

        InitializeLogs();

        LogsDataGrid.Items.Clear();
        LogsDataGrid.ItemsSource = ErrorLogEntries;
    }

    private void InitializeLogs()
    {
        if (LogsDataGrid.ItemsSource != null)
        {
            LogsDataGrid.ItemsSource = null;
        }

        var logsStrg = LogFileWriter.ReadLogs();
        ErrorLogEntries = new ErrorLogEntry [logsStrg.Length];

        for (int i = 0; i < logsStrg.Length; i++)
        {
            var log = logsStrg [i].Split(',');
            ErrorLogEntries [i] = new ErrorLogEntry
            {
                ErrorType = GetLogEntryType(log [0]),
                MethodName = log [1],
                Message = log [2],
                Timestamp = DateTime.Parse(log [3])
            };
        }
    }

    private static ErrorType GetLogEntryType(string v)
    {
        return v switch
        {
            "DB ERROR" => ErrorType.DbError,
            "VALIDATION ERROR" => ErrorType.ValidationError,
            "UI WARNING" => ErrorType.UiWarning,
            "SERVICE ERROR" => ErrorType.ValidationError,
            _ => ErrorType.UnknownError,
        };
    }

    private void LogsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        if (e.Row.DataContext is ErrorLogEntry logEntry)
        {
            if (logEntry.ErrorType == ErrorType.DbError)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightCoral);
            }
            else if (logEntry.ErrorType == ErrorType.ServiceError)
            {
                e.Row.Background = new SolidColorBrush(Colors.OrangeRed);
            }
            else if (logEntry.ErrorType == ErrorType.UiWarning)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightYellow);
            }
            else if (logEntry.ErrorType == ErrorType.ValidationError)
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
