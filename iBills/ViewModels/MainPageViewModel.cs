using iBills.Models;
using iBills.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iBills.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public Item Item { get; set; }
        public ObservableCollection<Item> AllItems { get; set; }
        public Command LoadItemsCommand { get; set; }
        public DelegateCommand<object> ItemTappedCommand { get; }
        public DelegateCommand GoToDetailsCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            AllItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(AllItems);

            GoToDetailsCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("ItemDetails"));
            ItemTappedCommand = new DelegateCommand<object>(ItemTapped); 
        }

        async private void ItemTapped(object obj)
        {
            var item = obj as Item;
            if (item == null)
                return;
            var parameters = new NavigationParameters();
            parameters.Add("item",item);
            await NavigationService.NavigateAsync("ItemDetails", parameters);
        }
        
      
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

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
                IsBusy = false;
            }
        }
    }
}


