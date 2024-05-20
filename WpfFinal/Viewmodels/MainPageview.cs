using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Models;
using WpfFinal.Pages;
using WpfFinal.Viewmodels;

namespace WpfFinal.ViewModels;

public class MainPageViewModel : BaseViewModel
{
  
    #region Commands
    public ICommand CommandOpen { get; set; }
    public ICommand SignInCommand { get; set; }
    public ICommand SignUpCommand { get; set; }
    public ICommand AboutCommand { get; set; }
    public ICommand CloseCommand { get; set; }
    #endregion

    #region Ctor

    public MainPageViewModel()
    {
        CommandOpen = new RelayCommand(OpenWebPage);
        SignInCommand = new RelayCommand(SignInCommandExecute);
        SignUpCommand = new RelayCommand(SignUpCommandExecute);
        AboutCommand = new RelayCommand(AboutUsPageOpen);
        CloseCommand = new RelayCommand(CloseCommandExecute);
    }

    #endregion

    #region Functions
    public void AboutUsPageOpen(object? obj)
    {
        if (obj is Page page)
        {
            var Aboutpage = new AboutPage();
            Aboutpage.DataContext = new AboutPageViewModel();
            page.NavigationService.Navigate(Aboutpage);

        }
    }
  

    private void CloseCommandExecute(object? obj)
    {
        if (obj is Page page)
        {
            Window window = Window.GetWindow(page);

            window?.Close();
        }
    }

    private void OpenWebPage(object parameter)
    {
        Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", "https://linktr.ee/MrPosSystems");

    }

    private void SignInCommandExecute(object obj)
    {
        if (obj is Page page)
        {
            var signInPage = new SignInPage();
            signInPage.DataContext = new SignInPageViewModel();
            page.NavigationService.Navigate(signInPage);
        }
    }

    private void SignUpCommandExecute(object obj)
    {
        if (obj is Page page)
        {
            var signUpPage = new SignupPage();
            signUpPage.DataContext = new Signupviewmodel(); // Burada düzeltme
            page.NavigationService.Navigate(signUpPage);
        }
    }


    #endregion


}
