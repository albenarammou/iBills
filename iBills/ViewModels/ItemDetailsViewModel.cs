﻿using iBills.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iBills.ViewModels
{
    public class ItemDetailsViewModel : ViewModelBase
    {
        //public Item Item { get; set; }
        //public ItemDetailsViewModel(Item item = null)
        //{
        //    Title = item?.Text;
        //    Item = item;
        //}

        
        public DelegateCommand GoToMainCommand { get; }
        public ItemDetailsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Item Details Page";
            GoToMainCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("/MenuPage/NavigationPage/MainPage"));
        }
    }
}
