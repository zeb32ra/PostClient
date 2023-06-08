using ImapX;
using ImapX.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для FoldersAndLetters.xaml
    /// </summary>
    public partial class FoldersAndLetters : Window
    {
        private CommonFolderCollection folders = ImapHelper.GetFolders();
        MessageCollection messages;
        Message ms;
        int count = 0;
        public FoldersAndLetters()
        {
            InitializeComponent();
            foreach(var folder in folders)
            {
                FolderCont.Items.Add(folder.Name);
            }
        }

        private void FolderCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            messages = ImapHelper.GetMessagesForFolder(FolderCont.SelectedItem.ToString());
            foreach(var message in messages) { 
                LettersCon.Items.Add(message.Subject);
            }
        }

        private void LettersCon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // transfer отправитель, получатель, тема и содержимое письма.
            foreach (var message in messages)
            {
                if (message.Subject == LettersCon.SelectedItem.ToString())
                {
                   
                    ms = message;
                    string from = ms.From.Address;
                    string to = ms.To[0].Address;
                    string theme = ms.Subject.ToString();
                    string content = ms.Body.Html.ToString();
                    if(count == 1)
                    {
                        LetterView win = new LetterView(from, to, theme, content);
                        win.Show();
                        this.Close();
                    }
                }
            }
        }

        private void Open_click(object sender, RoutedEventArgs e)
        {
            LetterView win = new LetterView(ms.From.Address, ms.To[0].Address, ms.Subject.ToString(), ms.Body.Html.ToString());
            win.Show();
            this.Close();
        }

        private void Answer_click(object sender, RoutedEventArgs e)
        {
            RespondLetter win = new RespondLetter(ms.From.Address, ms.To[0].Address, ms.Subject.ToString(), ms.Body.Html.ToString(), true);
            win.Show();
            this.Close();
        }

        private void WriteLetter_Click(object sender, RoutedEventArgs e)
        {
            RespondLetter win = new RespondLetter("", "", "", "", false);
            win.Show();
            this.Close();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            count = 0;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            count = 1;
        }
    }
}
