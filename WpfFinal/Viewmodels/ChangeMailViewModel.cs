using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfFinal.Databases;
using WpfFinal.Models;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using System.Text.RegularExpressions;
using WpfFinal.Pages;

namespace WpfFinal.Viewmodels;

    public class ChangeMailViewModel:BaseViewModel
    {


    #region Properties
    private Page currentView;

    public Page CurrentView
    {
        get { return currentView; }
        set { currentView = value; OnPropertyChanged(); }
    }
    public int StoreID { get; set; }
    public int msg { get; set; } //

    private Store store;
    public Store OrginalStore
    {
        get { return store; }
        set { store = value; OnPropertyChanged(); }
    }
    private string orginalMail;


    public string OrginalMail
    {
        get { return orginalMail; }
        set { orginalMail = value; OnPropertyChanged(); }
    }
   
    private int  code;


    public int Code1
    {
        get { return code; }
        set { code = value; OnPropertyChanged(); }
    }

    private string copyMail;

    public string CopyMail
    {
        get { return copyMail; }
        set { copyMail = value; OnPropertyChanged(); }
    }


    #endregion

    #region Ctors
    public ChangeMailViewModel()
    {
            
    }

    public ChangeMailViewModel(int ID)
    {
        Database.DeserializeStore();
            StoreID = ID;   
        Code1 = 0;
        CopyMail = "";
        OrginalStore = Database.StoreDB.FirstOrDefault(x => x.StoreID == StoreID);
        OrginalMail = OrginalStore.Mail;
        GetCodeCommand = new RelayCommand(mailing, IsGetCodeCommand);
        SaveCommand = new RelayCommand(SaveCommandExecute, ISave);
        BackCommand = new RelayCommand(BackCommandExecute);
        Random random = new Random();
        msg = random.Next(200, 999);
    }

    

    #endregion

    #region Commands
    public ICommand GetCodeCommand { get; set; }
    public ICommand SaveCommand { get; set; }

    #endregion

    #region Functions

    private void SaveCommandExecute(object obj)
    {
        Database.DeserializeStore();
        var itemToRemove = Database.StoreDB.FirstOrDefault(x => x.StoreID == StoreID);
        if (itemToRemove != null)
        {
            Database.StoreDB.Remove(itemToRemove);
        }

        OrginalStore.Mail = CopyMail ;
        Database.StoreDB.Add(OrginalStore);
        Database.StoreSaveChanges();

        if (obj is Page p)
        {
            CurrentView = App.Container.GetInstance<ProfilPage>();

            CurrentView.DataContext = new ProfilePageViewModel(StoreID);
            p.NavigationService.Navigate(CurrentView);
        }
        CopyMail = "";
        Code1=0;
        OrginalMail = "";
        OrginalStore = new Store();
    }

    private bool ISave(object arg)
    {
        if (msg == Code1&& CopyMail!=OrginalMail&&CopyMail!="")
        {
            return true;
        }
        return false;
    }
    public bool IsGetCodeCommand(object? obj)
    {
        return CopyMail != null && Regex.IsMatch(CopyMail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"); 
    }
    void mailing(object? obj)
    {

        string senderEmail = "mirtalibemirli498@gmail.com";
        string senderPassword = "aytndmgzqcukvmds";


        string recipientEmail = CopyMail;

        string smtpServer = "smtp.gmail.com";
        int smtpPort = 587;

        using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
        {
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
            {
                mailMessage.Subject = "Verification Code";


                mailMessage.Body = $"Your Code is {msg}";
                smtpClient.Send(mailMessage);



            }
        }
    }
    #endregion
}
