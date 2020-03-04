using iBills.Models;
using iBills.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iBills.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        public DelegateCommand GoToDetailsCommand { get; }
        public DelegateCommand GoToMainCommand { get; }
        public DelegateCommand GoToNewCommand { get; }


        public MenuPageViewModel(INavigationService navigationService, IDataStore<Item> DataStore)
            : base(navigationService, DataStore)
        {
            GoToDetailsCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/ItemDetails"));
            GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
            GoToNewCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/NewItem"));
        }
    }
}
