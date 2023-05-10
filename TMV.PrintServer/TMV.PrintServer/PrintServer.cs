using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
    public class PrintServer
    {
        public void Print()
        {
            try
            {
                StringCollection strList = PrinterSettings.InstalledPrinters;
                var list = strList.Cast<string>().ToList();
                PrintDocument pd = new PrintDocument();
                //Deli DB-615KI
                if (list.Any(x => x == "Deli DB-615KII"))
                {
                    pd.PrinterSettings.PrinterName = "Deli DB-615KII";
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Print();
                }
                else
                {
                    Console.WriteLine("请检查打印机是否正常连接");
                }

            }
            finally
            {
                //streamToPrint.Close();
            }
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            var url = System.Windows.Forms.Application.StartupPath;
            WordToPdfWithWPS(url + @"\newprintmodel.docx", url + @"\printmodel.pdf");
            PdfToImageHelper pdfToImageHelper = new();
            var sd = pdfToImageHelper.GetBase64StringArrayByPdfPath(url + @"\printmodel.pdf");
            var base64 = sd[0];
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Image mImage = Image.FromStream(memStream);
            ev.Graphics.DrawImage(mImage, new System.Drawing.Point(100, 240));
            ev.HasMorePages = false;
        }
        public void WordToPdfWithWPS(string sourcePath, string targetPath)
        {
            try
            {
                Word.Application _word = new Word.Application();
                //打开_filePath的word文件
                var doc = _word.Documents.Open(sourcePath);
                //转换文件，输出保存
                doc.ExportAsFixedFormat(targetPath.Replace(".doc", ".pdf"), Word.WdExportFormat.wdExportFormatPDF);
                doc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
