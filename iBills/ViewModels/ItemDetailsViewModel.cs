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
    public class ItemDetailsViewModel : BindableBase, INavigationAware
    {
        public Item Item { get; set; }
        public ObservableCollection<Item> AllItems { get; set; }

        public DelegateCommand GoToMainCommand { get; }
        public Command LoadItemsCommand { get; set; }
        public DelegateCommand SaveSelectedCommand { get; }
        public DelegateCommand DeleteSelectedCommand { get; }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ItemDetailsViewModel(INavigationService NavigationService, DbDataStore DataStore)

        {
            AllItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(AllItems);

            async Task ExecuteLoadItemsCommand()
            {

                //if (IsBusy)
                //    return;

                //IsBusy = true;

                try
                {
                    AllItems.Clear();
                    List<Models.Item> items = await DataStore.GetItemsAsync();
                    foreach (var item in items)
                    {
                        AllItems.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    //IsBusy = false;
                }
            }

            SaveSelectedCommand = new DelegateCommand(SaveSelected, CanSave);

            async void SaveSelected()
            {
                
                _ = await DataStore.SaveItemAsync(SelectedItem);
                LoadItemsCommand.Execute(AllItems); 
            }

            DeleteSelectedCommand = new DelegateCommand(DeleteSelected, CanSave);

            async void DeleteSelected()
            {

                _ = await DataStore.DeleteItemAsync(SelectedItem);
                LoadItemsCommand.Execute(AllItems); 
            }

            bool CanSave()
            {
                return true;
            }

            GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
        }






        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedItem = parameters["item"] as Item;
        }
    }
}
