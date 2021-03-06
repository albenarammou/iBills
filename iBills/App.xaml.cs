﻿using Prism;
using Prism.Ioc;
using iBills.ViewModels;
using iBills.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iBills.Services;
using iBills.Models;

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
            DependencyService.Register<NotificationEventArgs>();
            DependencyService.Register<INotificationManager>();

            await NavigationService.NavigateAsync("MenuPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IDataStore<Item>>(new DbDataStore());
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ItemDetails, ItemDetailsViewModel>();
            containerRegistry.RegisterForNavigation<NewItem, NewItemViewModel>();
        }
    }
}
