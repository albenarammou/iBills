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
        public ObservableCollection<Models.Item> AllItems { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Models.Item Item { get; set; }

        public DelegateCommand GoToDetailsCommand { get; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            AllItems = new ObservableCollection<Models.Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(AllItems);

            GoToDetailsCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("ItemDetails"));
            //GoToDetailsCommand = new DelegateCommand(ExecuteNavigateCommand);
        }

        private async void onItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var x = e.Item as myObject;
            //await Navigation.PushAsync(new DetailPage(x));
            var parameters = new NavigationParameters();
            parameters.Add("item", Item);
            await NavigationService.NavigateAsync("ItemDetails", parameters);
        }

        //async void ExecuteNavigateCommand() {
        //    var parameters = new NavigationParameters();
        //    Item = new Models.Item
        //    {
        //        Text = _ItemText,
        //        Description = _ItemDescription,
        //        Done = _ItemDone
        //    };

        //    parameters.Add("item", Item);
        //    await NavigationService.NavigateAsync("ItemDetails", parameters);
        //}

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


