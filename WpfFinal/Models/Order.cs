using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfFinal.Services;

namespace WpfFinal.Models;

public class Order: InotifyService
{

    public Order()
    {
        
    }

    private int? count;
    public int? Count
    {
        get { return count; }
        set { count = value; OnPropertyChanged(); }
    }
    private int? price;
    public int? Price
    {
        get { return price; }
        set { price = value; OnPropertyChanged(); }
    }

    public DateTime selldate { get; set; }


    public int orderID { get; set; }

    public int storeID { get; set; }

    private int productId;

    public int ProductID
    {
        get { return productId; }
        set { productId= value; OnPropertyChanged(); }
    }
    private string? name;

    public string? Name
    {
        get { return name; }
        set { name = value; OnPropertyChanged(); }
    }


}
