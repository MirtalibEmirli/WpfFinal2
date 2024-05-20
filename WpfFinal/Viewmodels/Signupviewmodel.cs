using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;
using System.Windows.Input;
using WpfFinal.Commands;
using WpfFinal.Databases;
using WpfFinal.Models;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace WpfFinal.Viewmodels;
public class Signupviewmodel : BaseViewModel
{

    #region Properties


    int? msg;

    private string? errorMassage;
    public string? ErrorMassage { get => errorMassage; set { errorMassage = value; OnPropertyChanged(); } }



    private int? codemail;

    public int? Codemail
    {
        get { return codemail; }
        set { codemail = value; }
    }


    private Store? store;
    public Store? NewStore
    {
        get { return store; }
        set { store = value; OnPropertyChanged(); }
    }
    #endregion

    #region Commands
    public ICommand RegisterCommand { get; set; }
    public ICommand GetCode { get; set; }

    #endregion

    #region Ctor
    public Signupviewmodel()
    {
        Random random = new Random();
        msg = random.Next(200, 999);
        NewStore = new Store();
        BackCommand = new RelayCommand(BackCommandExecute);
        RegisterCommand = new RelayCommand(RegisterCommandExecute, IsRegisterCommand);
        GetCode = new RelayCommand(mailing, IsGetCodeCommand);

    }
    #endregion

    #region Functions
    public bool IsGetCodeCommand(object? obj)
    {
        return NewStore.Mail != null && Regex.IsMatch(NewStore.Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
    public bool IsRegisterCommand(object? obj)
    {
        if (NewStore != null && NewStore.BranchName != null)
        {
            if (NewStore.Mail != null)
                if (Codemail == msg)
                    if (NewStore.Password != null)
                    {
                        return true;
                    }
        }
        return false;
    }

    public void RegisterCommandExecute(object obj)
    {

        if (Database.StoreDB == null)
            Database.StoreDB = new ObservableCollection<Store>();

        if (NewStore != null)
        {
            Database.StoreDB.Add(NewStore);
            Database.StoreSaveChanges();
            NewStore = new Store();
            if (obj is Page page)
            {
                page.NavigationService.GoBack();
            }
        }

       
    }
    void mailing(object? obj)
    {

        string senderEmail = "mirtalibemirli498@gmail.com";
        string senderPassword = "aytndmgzqcukvmds";


        string recipientEmail = NewStore.Mail;

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

