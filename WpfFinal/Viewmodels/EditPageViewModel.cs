using FirstFloor.ModernUI.Presentation;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Databases;
using WpfFinal.Models;
using WpfFinal.Pages;

namespace WpfFinal.Viewmodels;

public class EditPageViewModel : BaseViewModel
{
    #region Properties

    private Product? _OrginalProduct;

    public Product? OrginalProduct
    {
        get { return _OrginalProduct; ; }
        set { _OrginalProduct = value; OnPropertyChanged(); }
    }
    private Product? _CopyProduct;

    public Product? CopyProduct
    {
        get { return _CopyProduct; ; }
        set { _CopyProduct = value; OnPropertyChanged(); }
    }

    private Page currentView;

    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }

    #endregion
    public int StoreID { get; set; }
    #region Ctors

    public EditPageViewModel()
    {
        BackCommand = new RelayCommand(BackCommandExecute);

    }

    public EditPageViewModel(Product? orginal,int ID)
    {
        StoreID = ID;
        if (orginal is not null)
        {
            _OrginalProduct = orginal;

            CopyProduct = new Product();
            CopyProduct.Id = orginal.Id;
            CopyProduct.Name = orginal.Name;
            CopyProduct.Description = orginal.Description;
            CopyProduct.Price = orginal.Price;
            CopyProduct.count = orginal.count;
            CopyProduct.StoreId = orginal.StoreId;
        }

        BackCommand = new RelayCommand(BackCommandExecute);
        SaveCommand = new RelayCommand(SaveCommandExecute, IsSaveExecetuce);
    }
    #endregion

    #region Commands
    public ICommand SaveCommand { get; set; }
    #endregion

    #region Functions

    private void SaveCommandExecute(object obj)
    {
        Database.DeserializeProducts();
        var productR = Database.Products.FirstOrDefault(x => x.Id == OrginalProduct.Id);
        if (productR != null)
        {
            Database.Products.Remove(productR);
            Database.Products.Add(CopyProduct);
            Database.ProductSaveChanges();
            Database.DeserializeProducts();

        }
        if (obj is Page p)
        {
            CurrentView = App.Container.GetInstance<MyProductsPage>();

            CurrentView.DataContext = new MyProductsViewModel(StoreID); 
            p.NavigationService.Navigate(CurrentView);
        }
      
        CopyProduct = new Product();
    }

    private bool IsSaveExecetuce(object arg)
    {
        return  CopyProduct.Price!= OrginalProduct.Price|| CopyProduct.count!= OrginalProduct.count ||CopyProduct.Description!=OrginalProduct.Description; 
    }
    #endregion


}
