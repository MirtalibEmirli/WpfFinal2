using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WpfFinal.Services;

namespace WpfFinal.Models;

public class Product : InotifyService
{
    private int id;

    public Product()
    {
    
    }
    public int? count { get; set; }
  
    public int Id
    {
        get { return id; }
        set { id = value; OnPropertyChanged(); }
    }

    public static bool operator ==(Product p1, Product p2)
    {
        if (ReferenceEquals(p1, p2))
        {
            return true;
        }

        if (p1 is null || p2 is null)
        {
            return false;
        }

        return p1.Id == p2.Id &&
               p1.Name == p2.Name &&
               p1.Description == p2.Description &&
               p1.StoreId == p2.StoreId &&
               p1.Price == p2.Price;

    }

    public static bool operator !=(Product p1, Product p2)
    {
        return !(p1 == p2);
    }

    private string? name;

    public string? Name
    {
        get { return name; }
        set { name = value; OnPropertyChanged(); }
    }

    private string? description;

    public string? Description {
        get { return description; } 
        set { description = value; OnPropertyChanged(); }
    }

    private int storeId;

    public int StoreId
    {
        get { return storeId; }
        set { storeId = value; OnPropertyChanged(); }
    }

    private int? price;
    public int? Price
    {
        get { return price; }
        set { price = value; OnPropertyChanged(); }
    }

   

}
