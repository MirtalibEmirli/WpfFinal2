using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfFinal.Services;
namespace WpfFinal.Models;



    public abstract class BaseViewModel : InotifyService
{
        #region BackCommandSection
        public ICommand BackCommand { get; set; }
        public void BackCommandExecute(object? obj)
        {
            Page? page = obj as Page;
            if (page is not null && page.NavigationService.CanGoBack)
                page.NavigationService.GoBack();
        }
        #endregion
    }

