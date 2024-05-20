using System.Windows;
using System.Windows.Navigation;
using WpfFinal.Viewmodels;
namespace WpfFinal.Views;



public partial class Firstwindow : NavigationWindow
{
    public Firstwindow()
    {
        InitializeComponent();
        DataContext = new FirstViewModel();
    }
}
