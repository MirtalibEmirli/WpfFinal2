using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WpfFinal.Services;

namespace WpfFinal.Models;

public class Store : INotifyPropertyChanged
{
    private static int nextStoreID = 1;

    public Store()
    {
        StoreID = nextStoreID;
        nextStoreID++;
    }

    private int storeID;

    [Key]
    public int StoreID
    {
        get { return storeID; }
        set { storeID = value; OnPropertyChanged(); }
    }

    private string? branchName;

    private string? password;
    private string mail;

    public string Mail
    {
        get { return mail; }
        set { mail = value; OnPropertyChanged(); }
    }

    public string? BranchName
    {
        get { return branchName; }
        set { branchName = value; OnPropertyChanged(); }
    }

    public string? Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

