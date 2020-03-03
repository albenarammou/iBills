using iBills.Models;
using iBills.Services;
using iBills.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iBills.ViewModels
{
    public class ItemDetailsViewModel : ViewModelBase 
    {
        //public DelegateCommand GoToMainCommand { get; }
        public DelegateCommand SaveSelectedCommand { get; }
        public DelegateCommand DeleteSelectedCommand { get; }
        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ItemDetailsViewModel(INavigationService navigationService)
                    : base(navigationService)
        {
            SaveSelectedCommand = new DelegateCommand(SaveSelected, CanSave);
            async void SaveSelected()
            {
                _ = await DataStore.SaveItemAsync(SelectedItem);
                _ = await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage");
        }

            DeleteSelectedCommand = new DelegateCommand(DeleteSelected, CanSave);
            async void DeleteSelected()
            {
                _ = await DataStore.DeleteItemAsync(SelectedItem);
                _ = await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage");
            }

            bool CanSave()
            {
                return true;
            }

            //GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedItem = parameters["item"] as Item;
        }
    }
}
