using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using SimpleInjector;
using WpfFinal.Pages;
using WpfFinal.Viewmodels;
using WpfFinal.ViewModels;

namespace WpfFinal;

public partial class App : Application
{
    public static SimpleInjector.Container Container { get; set; } = new SimpleInjector.Container();

    public App()
    {
        RegisterViews();
    }
    public void RegisterViews()
    {
        Container.RegisterSingleton<AddPage>();
        Container.RegisterSingleton<ReportPage>();
        Container.RegisterSingleton<ProfilPage>();
        Container.RegisterSingleton<EditPage>();
        Container.RegisterSingleton<AboutPage>();
        Container.RegisterSingleton<MainPage>();
        Container.RegisterSingleton<MyProductsPage>();
        Container.RegisterSingleton<SellPage>();
        Container.RegisterSingleton<SignInPage>();
        Container.RegisterSingleton<SignupPage>();
    }

}
