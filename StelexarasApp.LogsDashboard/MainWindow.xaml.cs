using StelexarasApp.LogsDashboard.Views;
using System.Windows;

namespace StelexarasApp.LogsDashboard;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ViewErrorLogs_Click(object sender, RoutedEventArgs e)
    {
        var crudLogsWindow = new ErrorsLogsWindow();
        crudLogsWindow.Show();
    }

    private void ViewCrudLogs_Click(object sender, RoutedEventArgs e)
    {
        var errorLogsWindow = new CRUDLogsWindow();
        errorLogsWindow.Show();
    }
}