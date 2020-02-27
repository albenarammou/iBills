using iBills.Models;
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


        public NewItemViewModel(INavigationService navigationService)
                    : base(navigationService)
        {
            GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
            SaveNewCommand = new DelegateCommand(SaveNew, CanSaveNew);

            async void SaveNew()
            {
                NewItem = new Item
                {
                    Text = _NewItemText,
                    Description = _NewItemDescription,
                    Done = false
                };
                _ = await DataStore.SaveItemAsync(NewItem);
                //LoadItemsCommand.Execute(AllItems); 
            }

            bool CanSaveNew()
            {
                return true;
            }
        }

    }

}
