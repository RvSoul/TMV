using System.Drawing;
using System.Drawing.Printing;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
    public class PrintServer
    {
        StreamReader streamToPrint;
        Font printFont;
        public void Print(MemoryStream memStream)
        {
            try
            {
                StringCollection strList = PrinterSettings.InstalledPrinters;
                var list = strList.Cast<string>().ToList();
                printFont = new Font("Arial", 10);
                streamToPrint = new StreamReader(memStream);
                PrintDocument pd = new PrintDocument();
                //Deli DB-615KI
                if (list.Any(x => x == "Deli DB-615KII"))
                {
                    pd.PrinterSettings.PrinterName = "Deli DB-615KII";
                    pd.PrintPage += new PrintPageEventHandler(this.pdPrint);
                    pd.Print();
                }
                else
                {
                    //pd.PrintPage += new PrintPageEventHandler(this.pdPrint);
                    Console.WriteLine("请检查打印机是否正常连接");
                }
            }
            finally
            {
                streamToPrint.Close();
            }
        }
        private void pdPrint(object  sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;//取得绘图目标 
                float linesPerPage = 0;//页面的行号 
                float yPosition = 0;//制作字符串的纵向方位 
                int count = 0;//行计数器 
                float leftMargin = e.MarginBounds.Left;//左面距 
                float topMargin = e.MarginBounds.Top;//上边距 
                string line = null;// 行字符串
                                   //  System.Drawing.Font printFont = printFont;//当时的打印字体 
                SolidBrush myBrush = new SolidBrush(Color.Black);//刷子 
                linesPerPage = e.MarginBounds.Height / printFont.GetHeight(g);//每页可打印的行数 
                  //逐行的循环打印一页 
                while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
                {
                    yPosition = topMargin + (count * printFont.GetHeight(g));
                    g.DrawString(line, printFont, myBrush, leftMargin, yPosition, new StringFormat());
                    count++;
                }
                //假如本页打印完结而line不为空阐明还有没完结的页面这将触发下一次的打印事情鄙人一次的打印中lineReader会
                //主动读取前次没有打印完的内容由于lineReader是这个打印办法外的类的成员它能够记载当时读取的方位

                if (line != null)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        
    }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            var url = System.Windows.Forms.Application.StartupPath;
            //WordToPdfWithWPS(url + @"\newprintmodel.docx", url + @"\printmodel.pdf");
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
