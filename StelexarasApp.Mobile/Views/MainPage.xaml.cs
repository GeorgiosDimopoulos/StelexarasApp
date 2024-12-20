﻿using Microsoft.AspNetCore.SignalR.Client;
using StelexarasApp.Services;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Mobile.Views.PaidiaViews;
using StelexarasApp.Mobile.Views.StaffViews;
using StelexarasApp.Mobile.Views.TeamsViews;

namespace StelexarasApp.Mobile.Views;

public partial class MainPage : ContentPage
{
    private readonly IExpenseService _expenseService;
    private readonly IStaffService _stelexiService;
    private readonly IPaidiaService _paidiaService;
    private readonly IDutyService _dutiesService;
    private readonly ITeamsService _teamsService;
    private readonly SignalrService _signalRService;
    private readonly IServiceProvider _serviceProvider;
    private HubConnection? _connection;

    public MainPage(
        IStaffService peopleService,
        IDutyService dutyService,
        IPaidiaService paidiaService,
        ITeamsService teamsService,
        IExpenseService expenseService,
        SignalrService signalRService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _dutiesService = dutyService;
        _stelexiService = peopleService;
        _paidiaService = paidiaService;
        _teamsService = teamsService;
        _expenseService = expenseService;
        _signalRService = signalRService;
        _serviceProvider = serviceProvider;

        // SetSignalConenction();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // await _connection.StartAsync();
    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        await _connection.InvokeAsync("SendMessage", "MAUIClient", "Hello from MAUI App!");
    }

    private void SetSignalConenction()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://yourserverurl/myhub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Dispatcher.Dispatch(() =>
            {
                DisplayAlert("New Message", $"{user}: {message}", "OK");
            });
        });

        _connection.Closed += async (error) =>
        {
            await Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Connection Closed", "Connection to the server was closed. Attempting to reconnect...", "OK");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            });
        };
    }

    private void OnMessageReceived(string message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            DisplayAlert("Message Received", message, "OK");
        });
    }

    private async void SendMessageButton_Clicked(object sender, EventArgs e)
    {
        await _signalRService.SendMessageAsync("Hello from MAUI!");
    }

    private async void OnExpensesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ExpensesPage(_expenseService));
    private async void OnTeamsPageButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new GeneralTeamsPage(_paidiaService, _teamsService));

    private async void OnStaffButtonClicked(object sender, EventArgs e)
    {
        var staffPage = ActivatorUtilities.CreateInstance<StaffPage>(_serviceProvider, _stelexiService);
        await Navigation.PushAsync(staffPage);
    }

    private async void OnDutiesButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DutiesPage(_dutiesService));
    private async void OnPaidiaButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PaidiaPage(_paidiaService));
}