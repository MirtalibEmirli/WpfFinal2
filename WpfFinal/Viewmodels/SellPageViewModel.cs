using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;
using WpfFinal.Services;
namespace WpfFinal.Viewmodels;
public class SellPageViewModel : BaseViewModel
{
    private int _id;
    #region Collections

    private ObservableCollection<Product>? products;
    public ObservableCollection<Product>? Products { get => products; set { products = value; OnPropertyChanged(); } }

    private ObservableCollection<Order>? allOrders;
    public ObservableCollection<Order>? AllOrders { get => allOrders; set { allOrders = value; OnPropertyChanged(); } }

    #endregion



    #region Commands
    public ICommand AddCommand { get; set; }
    public ICommand NewOrderCommand { get; set; }
    public ICommand AllproductsCommand { get; set; }
    public ICommand BackCommand2 { get; set; }
    public ICommand ReportCommand { get; set; }
    public ICommand ProfileCommand { get; set; }
    
    #endregion

    #region PAges

    private Page currentView;

    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }
    #endregion

    #region Ctors

    public SellPageViewModel()
    {

    }

    public SellPageViewModel(int id)
    {
        _id = id;
        BackCommand2 = new RelayCommand(BackCommand2Execute);
        AddCommand = new RelayCommand(AddCommandExecute);
        AllproductsCommand = new RelayCommand(AllproductsCommandExecute);
        NewOrderCommand = new RelayCommand(NewOrderExecute);
        ReportCommand = new RelayCommand(ReportCommandExecute);
        ProfileCommand = new RelayCommand(ProfileCommandExecute);
        Database.DeserializeOrders();
        AllOrders = new ObservableCollection<Order>(Database.Orders.Where(x => x.storeID == id && x.selldate.Date == DateTime.Now.Date).ToList());
    }





    #endregion

    #region Functions
    private void ProfileCommandExecute(object? obj)
    {

        if(obj is Page page)
        {

            CurrentView = App.Container.GetInstance<ProfilPage>();
            CurrentView.DataContext = new ProfilePageViewModel(_id);
            page.NavigationService.Navigate(CurrentView);   
                }
    }
    public void ReportCommandExecute(object? obj)
    {
        if(obj is Page page)
        {
            CurrentView = App.Container.GetInstance<ReportPage>();
            CurrentView.DataContext = new ReportPageViewModel(_id);
            page.NavigationService.Navigate(CurrentView);
        }

    }
    public void NewOrderExecute(object? obj)
    {

        if (obj is Page page1)
        {
            CurrentView = App.Container.GetInstance<NewOrderPage>();
            CurrentView.DataContext = new NewOrderPageViewModel(_id);
            page1.NavigationService.Navigate(CurrentView);

        }
    }
    public void AddCommandExecute(object? obj)
    {
        if (obj is Page page)
        {
            CurrentView = App.Container.GetInstance<AddPage>();

            CurrentView.DataContext = new AddPageViewModel(_id);
            page.NavigationService.Navigate(CurrentView);
        }

    }
    public void AllproductsCommandExecute(object? obj)
    {
        if (obj is Page page)
        {
            CurrentView = App.Container.GetInstance<MyProductsPage>();

            CurrentView.DataContext = new MyProductsViewModel(_id);
            page.NavigationService.Navigate(CurrentView);
        }

    }

    public void BackCommand2Execute(object? obj)
    {
        if (obj is Page page)
        {
            var Signpage = new SignInPage();
            Signpage.DataContext = new SignInPageViewModel();
            page.NavigationService.Navigate(Signpage);
        }
    }
    #endregion 
}
