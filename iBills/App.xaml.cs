using Prism;
using Prism.Ioc;
using iBills.ViewModels;
using iBills.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iBills.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iBills
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            DependencyService.Register<DbDataStore>();
            await NavigationService.NavigateAsync("MenuPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<ItemDetails, ItemDetailsViewModel>();
            containerRegistry.RegisterForNavigation<NewItem, NewItemViewModel>();
        }
    }
}
