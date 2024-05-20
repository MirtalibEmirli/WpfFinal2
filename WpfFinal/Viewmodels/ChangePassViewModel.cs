using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;

namespace WpfFinal.Viewmodels;

public class ChangePassViewModel:BaseViewModel
{

    #region Properties
    private Page currentView;

    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }
    public int  StoreID{ get; set; }
    private  Store store;
    public Store OrginalStore
    {
        get { return store; }
        set { store = value; OnPropertyChanged(); }
    }
    private string orginalPass;

    public string OrginalPass
    {
        get { return orginalPass; }
        set { orginalPass = value; OnPropertyChanged(); }
    }
    private string copyPass2;

    public string CopyPass2
    {
        get { return copyPass2; }
        set { copyPass2 = value; OnPropertyChanged(); }
    }
    private string copyPass1;

    public string CopyPass1
    {
        get { return copyPass1; }
        set { copyPass1 = value; OnPropertyChanged(); }
    }
    
    #endregion

    #region Commands
    public ICommand ChangepassCommand { get; set; }
  
    #endregion

    #region Ctors

    public ChangePassViewModel()
    {
            
    }
    public ChangePassViewModel(int ID)
    {
        Database.DeserializeStore();
        StoreID = ID;
        OrginalStore = Database.StoreDB.FirstOrDefault(x => x.StoreID == StoreID);
        ChangepassCommand = new RelayCommand(ChangepassCommandExecute,ISChangepassCommandExecute);
        BackCommand = new RelayCommand(BackCommandExecute);
        CopyPass1 = "";
        CopyPass2 = "";
        OrginalPass = "";
    }


    #endregion

    #region Functions
    private bool ISChangepassCommandExecute(object arg)  
    {
        if (OrginalPass == OrginalStore.Password && OrginalPass != CopyPass1 && CopyPass1 == CopyPass2 && CopyPass2 != "")   return true;
        
        return false;
    }

    private void ChangepassCommandExecute(object obj)
    {
        
        Database.DeserializeStore();
        var itemToRemove = Database.StoreDB.FirstOrDefault(x => x.StoreID == StoreID);
        if (itemToRemove != null)
        {
            Database.StoreDB.Remove(itemToRemove);
        }

        OrginalStore.Password = CopyPass2 ?? OrginalPass;
        Database.StoreDB.Add(OrginalStore);
        Database.StoreSaveChanges();

        if (obj is Page p)
        {
            CurrentView = App.Container.GetInstance<ProfilPage>();

            CurrentView.DataContext = new ProfilePageViewModel(StoreID);
            p.NavigationService.Navigate(CurrentView);
        }
        CopyPass1 = "";
        CopyPass2 = "";
        OrginalPass= "";
        OrginalStore = new Store();

    }
    #endregion
}
