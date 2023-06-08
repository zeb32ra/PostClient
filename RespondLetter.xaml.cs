using HtmlRtf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Post
{
    /// <summary>
    /// Логика взаимодействия для RespondLetter.xaml
    /// </summary>
    public partial class RespondLetter : Window
    {
        string to_;
        string from_;
        string sub_;
        string content;
        public RespondLetter(string to, string from, string sub, string con, bool r_or_w)
        {
            InitializeComponent();
            to_ = to;
            from_ = from;
            sub_ = sub;
            content = con;
            Totxt.Text = from_;
            if(r_or_w)
            {
                Totxt.IsEnabled = false;
            }
            else
            {
                Totxt.IsEnabled = true;
            }


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(Totxt.IsEnabled)
            {
                FoldersAndLetters win = new FoldersAndLetters();
                win.Show();
                this.Close();
            }
            else
            {
                LetterView win = new LetterView(from_, to_, sub_, content);
                win.Show();
                this.Close();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var credentials = ImapHelper.GetCredentials();
            HtmlRtfConverter.ToHtml(new TextRange(RichTxb.Document.ContentStart, RichTxb.Document.ContentEnd));
            MailMessage message = new MailMessage(credentials.Email, Totxt.Text, SubjectTxt.Text, File.ReadAllText("send.html"));
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(credentials.SmtpHost);
            smtp.Credentials = new NetworkCredential(credentials.Email, credentials.Pass);
            smtp.EnableSsl = true;
            smtp.Send(message);

            File.Delete("send.html");
        }
    }
}
