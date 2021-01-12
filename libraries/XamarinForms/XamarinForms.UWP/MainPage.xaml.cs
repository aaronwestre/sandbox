using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace XamarinForms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new XamarinForms.App());
        }
    }
}
