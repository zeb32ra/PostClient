using HtmlRtf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Post
{
    /// <summary>
    /// Логика взаимодействия для LetterView.xaml
    /// </summary>
    public partial class LetterView : Window
    {
        string content;
        string from;
        string to;
        string sub;
        public LetterView(string f, string t, string th, string c)
        {
            InitializeComponent();
            Fromtxt.Text = f;
            Totxt.Text = t;
            Subtxt.Text = th;
            content = c;
            from = f;
            to = t;
            sub = th;
            LoadToReachTxb();

        }


        private void LoadToReachTxb()               //Конвертация в Rtf
        {
            HtmlRtfConverter.ToRtf(content);

            //подобно тому, что было в лекции про RichTextBox
            TextRange range = new TextRange(Reachtxb.Document.ContentStart, Reachtxb.Document.ContentEnd);
            FileStream fs = new FileStream("msg.rtf", FileMode.OpenOrCreate);
            range.Load(fs, DataFormats.Rtf);
            fs.Close();

            //для очистки
            File.Delete("msg.rtf");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FoldersAndLetters win  = new FoldersAndLetters();
            win.Show();
            this.Close();
        }

        private void Respond_Click(object sender, RoutedEventArgs e)
        {
            RespondLetter win = new RespondLetter(to, from, sub, content, true);
            win.Show();
            this.Close();
        }
    }
}
