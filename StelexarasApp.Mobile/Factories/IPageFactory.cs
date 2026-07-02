namespace StelexarasApp.Mobile.Factories;

public interface IPageFactory
{
    TPage Create<TPage>(params object[] args) where TPage : Page;
}
