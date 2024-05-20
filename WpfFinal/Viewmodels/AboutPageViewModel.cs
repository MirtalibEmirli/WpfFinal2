using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFinal.Commands;
using WpfFinal.Models;
namespace WpfFinal.Viewmodels;

public class AboutPageViewModel:BaseViewModel
{

    #region Properties
    private string _textt;
    public string Textt
    {
        get { return _textt; }
        set { _textt = value; OnPropertyChanged(); }
    }

    #endregion

    #region Ctor
    public AboutPageViewModel()
    {
        Textt = @"
The MRPos system has been in service since 2023.
Our Pos system will help you manage your little shop.
Our address is Caspian Plaza, 10th floor. 
If you have a problem, You can contact us by clicking contact us 
This app will work with more vape shops. 
As you know, the number of electronic cigarette users has been increasing in our country for more than 15 years.
The Vapeshops that are concerned with this need a system to manage it, we are giving you this feature.";

        BackCommand = new RelayCommand(BackCommandExecute);
    }

    #endregion

}
