using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfFinal.Models;

namespace WpfFinal.Services;

public class PageService
{
    public static void ChangePageService<TView, TViewModel>(object? obj,int? id)
   where TView : Page where TViewModel : BaseViewModel
    {
        Page? page = obj as Page;
        if (page is not null)
        {
            TView? PageViewModel = App.Container?.GetInstance<TView>();
            var t = App.Container?.GetInstance<TViewModel>();
            PageViewModel!.DataContext = t;

            page.NavigationService.Navigate(PageViewModel);
        }
    }
}
