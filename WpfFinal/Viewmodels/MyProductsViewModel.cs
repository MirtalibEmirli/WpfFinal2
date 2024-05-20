using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFinal.Models;
using WpfFinal.Commands;
using System.Security.Cryptography;
using WpfFinal.Databases;
using System.Windows.Input;
using WpfFinal;
using System.ComponentModel;
using WpfFinal.Pages;
using System.Windows.Controls;
namespace WpfFinal.Viewmodels;

public class MyProductsViewModel : BaseViewModel
{
    public int ID;
   

    #region Collections

    private ObservableCollection<Product>? allProducts;
    public ObservableCollection<Product>? AllProducts { get => allProducts; set { allProducts = value; OnPropertyChanged(); } }

    #endregion

    #region Commands
    public ICommand DelCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand BackCommand2 { get; set; }
    #endregion

    #region Ctors

    public MyProductsViewModel()
    {

    }
    public MyProductsViewModel(int id)
    {
        Database.DeserializeProducts();
        ID = id;
        BackCommand2 = new RelayCommand(BackCommand2Execute);
        EditCommand = new RelayCommand(EditCommandExecute, IsEditable);
        DelCommand = new RelayCommand(DelCommandExecute, CanDelete);
        try
        {
            AllProducts = new ObservableCollection<Product>(Database.Products.Where(x => x.StoreId == ID).ToList());

        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    #region Functions
    private void DelCommandExecute(object? obj)
    {
        if (obj is Product p2)
        {

            Database.DeserializeProducts();
            var t = Database.Products.FirstOrDefault(x => x.Id == p2.Id);
            Database.Products.Remove(t);
            Database.ProductSaveChanges();
            Database.DeserializeProducts();
            AllProducts = new ObservableCollection<Product>(Database.Products.Where(x => x.StoreId == ID).ToList());

        }

    }

    private bool CanDelete(object? obj)
    {
        if(obj is Product p1 &&p1!=null)
        {
            return true;
        }

        return false;
    }

    public bool IsEditable(object? obj)
    {
        if (obj is Product p1)
        {
            return true;
        }
        return false;
    }


    public void EditCommandExecute(object? obj)
    {

        if(obj is  Product p2) {

            var t = new EditPage();
            t.DataContext = new EditPageViewModel(p2,ID);
            App.Container.GetInstance<MyProductsPage>().NavigationService.Navigate(t);
        }
      
    }

    public void BackCommand2Execute(object? obj)
    {
        if (obj is Page page)
        {
            var Sellpage = new SellPage();
            Sellpage.DataContext = new SellPageViewModel(ID);
            page.NavigationService.Navigate(Sellpage);
        }
    }
    #endregion
}
