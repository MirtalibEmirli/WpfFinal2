using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Databases;
using WpfFinal.Models;

namespace WpfFinal.Viewmodels;

public class AddPageViewModel : BaseViewModel
{

    #region Properties
    private Product p1;

    public Product P1
    {
        get { return p1; }
        set { p1 = value; }
    }
    #endregion

    #region Commands
    public ICommand AddCommand { get; set; }
    public ICommand AllproductsCommand { get; set; }
    #endregion

    #region Ctors
    public AddPageViewModel()
    {

    }


    public AddPageViewModel(int id)
    {
        P1 = new Product();
        P1.StoreId = id;
        BackCommand = new RelayCommand(BackCommandExecute);
        AddCommand = new RelayCommand(AddCommandExecute, ISAddCommand);
    }


    #endregion


    #region Functions
    public bool ISAddCommand(object? obj)
    {


        return P1.Name is not null && P1.Name.Length > 3 && P1.Price > 0 && P1.Description is not null;
    }


   
    public void AddCommandExecute(object? obj)
    {
        Database.DeserializeProducts();
        if (Database.Products.Count > 0)
        {
            P1.Id = Database.Products.Last().Id + 1;
        }
        else
        {
            P1.Id = 1;

        }
        P1.Name = P1.Name.ToLower();
        P1.Description = P1.Description.ToLower();
        var product = Database.Products.FirstOrDefault(x => x.Name == P1.Name && x.Description == P1.Description);
        if (product is not null)
        {
            product.count += P1.count;
        }
        else
        {
            Database.Products.Add(P1);
        }
        Database.ProductSaveChanges();
        P1 = new Product();
        if (obj is Page page)
        {
            page.NavigationService.GoBack();
        }
    }
    #endregion
}

