using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer
{
    public class WordUtil
    {
        XWPFDocument document = null;
        public void WordWrite(string word) 
        {
            var path = Application.StartupPath+ @"\printmodel.doc";
            using (var file=File.OpenRead(path))
            {
                document = new XWPFDocument(file);
            }
                
        }
    }
}
