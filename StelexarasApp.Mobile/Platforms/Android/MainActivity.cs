using Android.App;
using Android.Content.PM;

namespace StelexarasApp
{
    [Activity(
        Name = "com.companyname.stelexarasapp.MainActivity",
        Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
