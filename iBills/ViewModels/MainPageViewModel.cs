using iBills.Models;
using iBills.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public MainPageViewModel(INavigationService navigationService, IDataStore<Item> DataStore)
            : base(navigationService, DataStore)
        {
            Title = "All Bills";
            AllItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(DataStore));
            LoadItemsCommand.Execute(AllItems);

            //notificationManager = DependencyService.Get<INotificationManager>();
            //notificationManager.NotificationReceived += (sender, eventArgs) =>
            //{
            //    var evtData = (NotificationEventArgs)eventArgs;
            //    ShowNotification(evtData.Title, evtData.Message);
            //};


            GoToDetailsCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("ItemDetails"));
            ItemTappedCommand = new DelegateCommand<object>(ItemTapped); 
        }

        //void ShowNotification(string title, string message)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        var msg = new Label()
        //        {
        //            Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
        //        };
        //        //stackLayout.Children.Add(msg);
        //    });
        //}

        async private void ItemTapped(object obj)
        {
            var item = obj as Item;
            if (item == null)
            {
                return;
            }
            var parameters = new NavigationParameters();
            parameters.Add("item",item);
            if (item.Done)
            {
                NotificationNumber++;
                string title = $"Bill! #{item.Text} #{NotificationNumber}";
                string message = $"Hello! #{item.Description} {NotificationNumber} notifications!";
                NotificationManager.ScheduleNotification(title, message);
            }
            await NavigationService.NavigateAsync("ItemDetails", parameters);
        }

        async Task ExecuteLoadItemsCommand(IDataStore<Item> DataStore)
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                AllItems.Clear();
                List<Item> items = await DataStore.GetItemsAsync(); 
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


