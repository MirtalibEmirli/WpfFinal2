
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;
using WpfFinal.Services;
using WpfFinal.ViewModels;

namespace WpfFinal.Viewmodels;

public class SignInPageViewModel: BaseViewModel
{

    #region Properties
    private Store store;
    public Store NewStore { get { return store; } set { store = value; OnPropertyChanged(); } }

    #endregion

    #region Commands
    public ICommand Entercommand { get; set; }
    public ICommand BackCommand2 { get; set; }
    #endregion
 
    #region Ctor
    public SignInPageViewModel()
    {
        NewStore = new Store();
        Entercommand = new RelayCommand(EnterExecute,IsEnterable);
        BackCommand2 = new RelayCommand(BackCommand2Execute);
    }

    #endregion

    #region Functions
    public bool IsEnterable(object? obj)
    {
        Database.DeserializeStore();
        var mystores = Database.StoreDB;
        if(mystores != null) {
            foreach (var item in mystores)
            {
                if( NewStore.BranchName == item.BranchName)
                {
                    if( NewStore.Password== item.Password)
                    {
                        NewStore.Mail = item.Mail;
                        NewStore.StoreID = item.StoreID;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void EnterExecute(object? obj)
    {
        if(obj is Page page)
        {
            var Sellpage = new SellPage();
            Sellpage.DataContext = new SellPageViewModel(NewStore.StoreID);
            page.NavigationService.Navigate(Sellpage);
        }
        NewStore = new Store();

    }

    public void BackCommand2Execute(object? obj)
    {
        if (obj is Page page)
        {
            var mpage = new MainPage();
            mpage.DataContext = new MainPageViewModel();
            page.NavigationService.Navigate(mpage);
        }
    }

    #endregion
}
