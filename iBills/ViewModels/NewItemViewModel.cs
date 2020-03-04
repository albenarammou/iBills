using iBills.Models;
using iBills.Services;
using iBills.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace iBills.ViewModels
{
    public class NewItemViewModel : ViewModelBase
    {
        public Item NewItem { get; set; }
        public DelegateCommand GoToMainCommand { get; }
        public DelegateCommand SaveNewCommand { get; private set; }

        private string _NewItemText = "Text";
        public string NewItemText
        {
            get => _NewItemText;
            set => SetProperty(ref _NewItemText, value);
        }

        private string _NewItemDescription = "Description";
        public string NewItemDescription
        {
            get => _NewItemDescription;
            set => SetProperty(ref _NewItemDescription, value);
        }

        private DateTime _NewItemdDate = DateTime.Now;
        public DateTime NewItemdDate 
        {
            get => _NewItemdDate;
            set => SetProperty(ref _NewItemdDate, value);
        }

        public NewItemViewModel(INavigationService navigationService, IDataStore<Item> DataStore)
                    : base(navigationService, DataStore)
        {
            GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
            SaveNewCommand = new DelegateCommand(SaveNew, CanSaveNew);

            async void SaveNew()
            {
                NewItem = new Item
                {
                    Text = _NewItemText,
                    Description = _NewItemDescription,
                    Date = _NewItemdDate,
                    Done = false
                };
                _ = await DataStore.SaveItemAsync(NewItem);
                _ = await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage");
            }

            bool CanSaveNew()
            {
                return true;
            }
        }

    }

}
