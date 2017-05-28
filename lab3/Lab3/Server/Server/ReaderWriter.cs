using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SocketServer
{
    public class ReaderWriter
    {
        public string Reader(FileStream fstream)
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            string file2 = System.Text.Encoding.Default.GetString(array);
            return file2;
        }
        public ReaderWriter()
        { }
        public void Writer(FileStream fstream, string data)
        {
            byte[] array2 = System.Text.Encoding.Default.GetBytes(data);

            fstream.Write(array2, 0, array2.Length);
        }
    }
}
