using iBills.Models;
using iBills.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace iBills.ViewModels
{
    public class ItemsViewModel : BindableBase
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        protected INavigationService NavigationService { get; private set; }

        public Item Item { get; set; }
        public ObservableCollection<Item> AllItems { get; set; }
        public Command LoadItemsCommand { get; set; }
        public DelegateCommand<object> ItemTappedCommand { get; }

        private readonly IPageDialogService _pageDialogService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }



        public ItemsViewModel(IPageDialogService pageDialogService,
            IDataStore<Item> dataProvider)
        {
            AllItems = new ObservableCollection<Models.Item>();
           // LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(AllItems);

        }


        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        AllItems.Clear();
        //        List<Models.Item> items = await DataStore.GetItemsAsync();
        //        foreach (var item in items)
        //        {
        //            AllItems.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }

}



