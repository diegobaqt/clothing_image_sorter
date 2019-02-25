using SorterApp.Views;
using Xamarin.Forms;

namespace SorterApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);

            Children.Add(new HomePage { Icon = "ic_home" });
            Children.Add(new LoadImagePage { Icon = "ic_ml" });
            Children.Add(new DocumentationPage { Icon = "ic_doc" });
        }
    }
}
