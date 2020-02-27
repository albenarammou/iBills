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

        private string _ItemText = "Text";
        public string ItemText
        {
            get => _ItemText;
            set => SetProperty(ref _ItemText, value);
        }

        private string _ItemDescription = "Description";
        public string ItemDescription
        {
            get => _ItemDescription;
            set => SetProperty(ref _ItemDescription, value);
        }

        private bool _ItemDone = false;
        public bool ItemDone
        {
            get => _ItemDone;
            set => SetProperty(ref _ItemDone, value);
        }

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
                List<Models.Item> items = await DataStore.GetItemsAsync(); //Exeption!!!
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


