
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfFinal.Models;
using WpfFinal.Services;

namespace WpfFinal.Databases;
public static class Database
{
    static Database()
    {
        StoreDB = new ObservableCollection<Store>();
        Products = new ObservableCollection<Product>();
        Orders = new ObservableCollection<Order>();
        DeserializeStore();
    }



    #region Orderss

    public static ObservableCollection<Order>? Orders { get; set; }
    public static string? FnameOrders = "../../../DataBasejson/Orders.json";

    public static void OrderSaveChanges()
    {
        var opp12 = new JsonSerializerOptions();
        opp12.WriteIndented = true;
        string data2 = JsonSerializer.Serialize(Orders, opp12);
        File.WriteAllText(FnameOrders, data2);
    }

    public static void DeserializeOrders()
    {
        if (File.Exists(FnameOrders))
        {
            string jsonText = File.ReadAllText(FnameOrders);
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            Orders = JsonSerializer.Deserialize<ObservableCollection<Order>>(jsonText, options);

        }
        else
        {
            File.Create(FnameOrders).Dispose();
        }

    }
    #endregion

    #region Products

    public static ObservableCollection<Product>? Products { get; set; }
    public static string? Fnameproducts = "../../../DataBasejson/Products.json";

    public static void ProductSaveChanges()
    {
        var opp12 = new JsonSerializerOptions();
        opp12.WriteIndented = true;
        string data2 = JsonSerializer.Serialize(Products, opp12);
        File.WriteAllText(Fnameproducts, data2);
    }
    public static void DeserializeProducts()
    {
        if (File.Exists(Fnameproducts))
        {
            string jsonText = File.ReadAllText(Fnameproducts);
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
           Products = JsonSerializer.Deserialize<ObservableCollection<Product>>(jsonText, options);

        }
        else
        {
            File.Create(Fnameproducts).Dispose();
        }

    }
    #endregion

    #region Store
    public static string? Fname = "../../../DataBasejson/Stores.json";
    public static ObservableCollection<Store>? StoreDB { get; set; }

    public static void StoreSaveChanges()
    {
        var opp12 = new JsonSerializerOptions();
        opp12.WriteIndented = true;
        string data2 = JsonSerializer.Serialize(StoreDB, opp12);
        File.WriteAllText(Fname, data2);
    }

    public static void DeserializeStore()
    {
        if (File.Exists(Fname))
        {
            string jsonText = File.ReadAllText(Fname);
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            StoreDB = JsonSerializer.Deserialize<ObservableCollection<Store>>(jsonText, options);

        }
        else
        {
            File.Create(Fname).Dispose();
        }

    }
    #endregion



}

