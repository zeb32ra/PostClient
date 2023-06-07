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
        }
    }
}
