﻿using CommunityToolkit.Mvvm.Input;
using Invoice_GenUI.Models;
using Invoice_GenUI.Models.Services;

namespace Invoice_GenUI.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
        }
        [RelayCommand]
        public void GoToCreateClient()
        {
            Navigation.NavigateTo<CreateClientViewModel>();
        }
        [RelayCommand]
        public void GoToInvoice()
        {
            Navigation.NavigateTo<InvoiceViewModel>();
        }
    }
}
