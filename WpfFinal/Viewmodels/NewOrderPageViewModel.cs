
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;

namespace WpfFinal.Viewmodels;

  public class NewOrderPageViewModel:BaseViewModel
{
    public int StoreID;

    #region Properties

    private Page currentView;

    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }

    private int? total;

    public int? Total
    {
        get { return total; }
        set { total = value; OnPropertyChanged(); }
    }

    #endregion

    #region Collections
    private ObservableCollection<Product>? allProducts;
    public ObservableCollection<Product>? AllProducts { get => allProducts; set { allProducts = value; OnPropertyChanged(); } }


    private ObservableCollection<Order>? currentSell;
    public ObservableCollection<Order>? CurrentSell { get => currentSell; set { currentSell = value; OnPropertyChanged(); } }
    #endregion

    #region Ctors
    public NewOrderPageViewModel()
    {
        
    }
    public NewOrderPageViewModel(int storeID)
    {
        Total = 0;
        Database.DeserializeProducts();

        CancelCommand = new RelayCommand(CancelCommandExecute);
        StoreID = storeID;
        CurrentSell = new ObservableCollection<Order>();
        AddCommand = new RelayCommand(AddItemtoSell, IsAdd);
        SellCommand = new RelayCommand(SellChecks);
        try
        {
            AllProducts = new ObservableCollection<Product>(Database.Products.Where(x => x.StoreId == StoreID).ToList());

        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    #region Commands
    public ICommand CancelCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand SellCommand { get; set; }

    #endregion
    
    #region Functions
    private void CancelCommandExecute(object obj)
    {
        if (obj is Page p)
        {
            CurrentView = App.Container.GetInstance<SellPage>();

            CurrentView.DataContext = new SellPageViewModel(StoreID);
            p.NavigationService.Navigate(CurrentView);
        }

    }

    public bool IsAdd(object? obj)
    {
        if (obj is Product p1)
        {
            return true;
        }
        return false;
    }
    private void AddItemtoSell(object? obj) {
        if (obj is Product p1)
        {


 
            Order or = new Order();
            or.ProductID = p1.Id;
            or.Name = p1.Name;
            or.Price = p1.Price;
            or.Count = 1;
            or.storeID = StoreID;
            var curritem = currentSell.FirstOrDefault(x => x.ProductID == or.ProductID);
            if(curritem != null)
            {
                currentSell.Remove(curritem);
                if(curritem.Count + 1 > p1.count)
                {
                    MessageBox.Show("Anbarda mehsul bu qederdir");
                }
                else
                {
                    curritem.Count += 1;
                }
                curritem.Price = p1.Price * curritem.Count;

              
                currentSell.Add(curritem);
            }
            else
            {
                CurrentSell.Add(or);
            }
            Total += p1.Price;
        }
    }

    private void SellChecks(object? obj)
    {
        Database.DeserializeOrders();
        Database.DeserializeProducts();
        foreach (Order item in CurrentSell)
        {
            if (Database.Orders.Count > 0)
            {
                item.orderID = Database.Products.Last().Id + 1;
            }
            else
            {
                item.orderID = 1;
            }
             item.selldate = DateTime.Now;

            Database.Orders.Add(item);
            Database.Products.FirstOrDefault(x => x.Id == item.ProductID).count -= item.Count;
        }
        Database.OrderSaveChanges();
        Database.ProductSaveChanges();
        currentSell.Clear();
        Total = 0;

        if (obj is Page p)
        {
            CurrentView = App.Container.GetInstance<SellPage>();

            CurrentView.DataContext = new SellPageViewModel(StoreID);
            p.NavigationService.Navigate(CurrentView);
        }
    }

    #endregion


}
