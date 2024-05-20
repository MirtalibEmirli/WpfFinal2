using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Models;
using WpfFinal.Pages;
using WpfFinal.Databases; 
namespace WpfFinal.Viewmodels;

public class ProfilePageViewModel : BaseViewModel
{

    #region Properties
    public int StoreID { get; set; }
    private Store store;

    public Store Store
    {
        get { return store; }
        set { store = value; OnPropertyChanged(); }
    }

    #endregion
    #region Commands
    public ICommand BackCommand2 { get; set; }
    public ICommand ChangeEmailCommand { get; set; }
    public ICommand ChangePassCommand { get; set; }
    #endregion
    public ProfilePageViewModel()
    {
    }
    public ProfilePageViewModel(int id)
    {
        StoreID = id;
        Database.DeserializeStore();
        BackCommand2 = new RelayCommand(BackCommand2Execute);
        Store = Database.StoreDB.FirstOrDefault(x => x.StoreID == StoreID);
        ChangeEmailCommand = new RelayCommand(ChangeEmailCommandExecute);
        ChangePassCommand =new RelayCommand(ChangePassCommandExecute);
    }


    #region Functions
    private void ChangePassCommandExecute(object obj)
    {
        if (obj is Page page)
        {
            var cpage = new ChangePassPage();
            cpage.DataContext = new ChangePassViewModel(StoreID);
            page.NavigationService.Navigate(cpage);
        }
    }

    private void ChangeEmailCommandExecute(object obj)
    {
        if (obj is Page page)
        {
            var mpage = new ChangeMailPage();
            mpage.DataContext = new ChangeMailViewModel(StoreID); 
            page.NavigationService.Navigate(mpage);
        }
    }
    public void BackCommand2Execute(object? obj)
    {
        if (obj is Page page)
        {
            var Sellpage = new SellPage();
            Sellpage.DataContext = new SellPageViewModel(StoreID);
            page.NavigationService.Navigate(Sellpage);
        }
    }
    #endregion
}
