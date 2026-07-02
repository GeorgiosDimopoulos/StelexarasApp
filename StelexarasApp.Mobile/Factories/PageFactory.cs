namespace StelexarasApp.Mobile.Factories;

internal class PageFactory : IPageFactory
{
    private IServiceProvider serviceProvider;

    public PageFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public TPage Create<TPage>(params object[] args) where TPage : Page
    {
        return ActivatorUtilities.CreateInstance<TPage>(serviceProvider, args);
    }
}
