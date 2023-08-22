using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace TRMDesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    //CultureInfo.CurrentCulture.IetfLanguageTag
                    // Int number 1045 - polish LCID
                    CultureInfo.GetCultureInfo(1045).IetfLanguageTag
                    )));
            base.OnStartup(e);


        }
    }
}
