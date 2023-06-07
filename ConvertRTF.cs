using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Post
{
    class ConvertRTF
    {
        public void saveRTFfile(string filename, TextPointer start, TextPointer end)
        {
            TextRange range = new TextRange(start, end);
            FileStream fstream = new FileStream(filename, FileMode.OpenOrCreate);
            range.Save(fstream, DataFormats.Rtf);
            fstream.Close();
        }
        public void loadRTFfile(string filename, TextPointer start, TextPointer end)
        {
            TextRange range = new TextRange(start, end);
            FileStream fstream = new FileStream(filename, FileMode.OpenOrCreate);
            range.Load(fstream, DataFormats.Rtf);
            fstream.Close();
        }
    }
}
