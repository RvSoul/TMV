using Spire.Doc;
using System.Drawing;
using System.Drawing.Printing;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
    public class PrintServer
    {
        public void Print(MemoryStream memStream)
        {
            try
            {
                StringCollection strList = PrinterSettings.InstalledPrinters;
                var list = strList.Cast<string>().ToList();
                Document doc = new Document();
                doc.LoadFromStream(memStream,FileFormat.Docx);
                //Deli DB-615KI
                if (list.Any(x => x == "Deli DB-615KII"))
                {
                    
                    ///后台打印
                    PrintDocument pd = doc.PrintDocument;
                    pd.PrinterSettings.PrinterName = "Deli DB-615KII";
                    pd.PrinterSettings.DefaultPageSettings.Landscape = true;
                    pd.DefaultPageSettings.PaperSize = new PaperSize("aa", 800, 366);
                    pd.OriginAtMargins = true;
                    Margins margins = new Margins(20, 50, 0, 0);
                    pd.DefaultPageSettings.Margins = margins;
                    //pd.PrintPage += new PrintPageEventHandler(this.pdPrint);
                    pd.Print();
                    doc.Close();
                }
                else
                {
                    Console.WriteLine("请检查打印机是否正常连接");
                }
            }
            finally
            {
                //doc.Close();
            }
        }
        /// <summary>
        /// 设置纸张打印方向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdPrint(object  sender, PrintPageEventArgs e)
        {
            try
            {
                var width = e.PageBounds.Width;
                var height = e.PageBounds.Height;
                if (e.PageSettings.Landscape)
                {
                    Swap(ref width, ref height);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void Swap(ref int i, ref int j)
        {
            int tmp = i;
            i = j;
            j = tmp;
        }
    }
}
