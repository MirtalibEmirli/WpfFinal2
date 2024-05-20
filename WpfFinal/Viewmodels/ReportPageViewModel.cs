using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;

namespace WpfFinal.Viewmodels;

    public class ReportPageViewModel:BaseViewModel
{
    #region Collections
    private ObservableCollection<Order> selledProducts;

    public ObservableCollection<Order> SelledProducts
    {
        get { return selledProducts; }
        set { selledProducts = value; OnPropertyChanged(); }
    }

    #endregion


    #region Properties
    public int StoreID { get; set; }


    private int? total;

    public int? Total
    {
        get { return total; }
        set { total = value;OnPropertyChanged(); }
    }

    private Page currentView;
    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }

    private DateTime sellDate;
    public DateTime SellDate
    {
        get { return sellDate; }
        set
        {
                sellDate = value;
                OnPropertyChanged( );
        }
    }
    private DateTime lastDate;
    public DateTime LastDate
    {
        get { return lastDate; }
        set
        {
            lastDate = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Ctors 

    public ReportPageViewModel()
    {
    }
    public ReportPageViewModel(int id)
    {
        StoreID = id;
        BackCommand = new RelayCommand(BackCommandExecute);
        ShowCommand = new RelayCommand(execute: ShowCommandExecute);
        Database.DeserializeOrders();
        SelledProducts = new ObservableCollection<Order>(Database.Orders.Where(x=> x.storeID==StoreID&&(x.selldate>SellDate&&x.selldate< LastDate)));
        SellDate = DateTime.Now;
        LastDate = DateTime.Now;
        Total = 0;
    }

   

    #endregion

    #region Commands
    public ICommand ShowCommand { get; set; }

    #endregion

    #region Functions
    private void ShowCommandExecute(object obj)
    {
        SelledProducts = new ObservableCollection<Order>(Database.Orders.Where(x => x.storeID == StoreID && (x.selldate > SellDate && x.selldate < LastDate)));
        foreach (var item in SelledProducts)
        {
            Total += item.Price;
        }
    }

    #endregion
}
