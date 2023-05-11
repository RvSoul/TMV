using Newtonsoft.Json;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.PrintServer.Model;

namespace TMV.PrintServer.Comm
{
    public class WordUtil
    {
        XWPFDocument document = null;
        public MemoryStream WordWrite(PrintDto printDto)
        {
            var path = Application.StartupPath + @"\printmodel.docx";
            MemoryStream ms = new();

            using (var file = File.OpenRead(path))
            {
                document = new XWPFDocument(file);
                if (document == null)
                {
                    return ms;
                }
                var TableTestList = printDto.ModelToDic();
                //遍历word中的段落
                foreach (var para in document.Paragraphs)
                {
                    foreach (var be in para.Body.BodyElements)
                    {
                        if (be.GetType() == typeof(XWPFParagraph))
                        {
                            var be1 = (XWPFParagraph)be;
                            var st = be1.ParagraphText;
                            foreach (var item in TableTestList)
                            {
                                if (be1.ParagraphText.Contains("${"+item.Key+"}"))
                                {
                                    st = st.Replace("${" + item.Key + "}", item.Value);
                                    be1.ReplaceText(be1.ParagraphText, st);
                                }
                            }
                           
                        }
                    }
                }
                //遍历表格      
                foreach (var table in document.Tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                foreach (var be in para.Body.BodyElements)
                                {
                                    if (be.GetType() == typeof(XWPFParagraph))
                                    {
                                        var be1 = (XWPFParagraph)be;
                                        foreach (var item in TableTestList)
                                        {
                                            if (be1.ParagraphText.Contains("${"+item.Key+"}"))
                                            {
                                                be1.ReplaceText(be1.ParagraphText, item.Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            document.Write(ms);
           // WordToPdfWithWPS(ms, Application.StartupPath + @"\printmodel.pdf");
            SaveToFile(ms);
            return ms;
        }
        public void WordToPdfWithWPS(MemoryStream stream,string targetPath)
        {
            try
            {
              
                Word.Application _word = new Word.Application();
                //打开_filePath的word文件
                var doc = _word.Documents.Open(stream);
                //转换文件，输出保存
                doc.ExportAsFixedFormat(targetPath.Replace(".docx", ".pdf"), Word.WdExportFormat.wdExportFormatPDF);
                doc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaveToFile(MemoryStream ms)
        {
            var path = Application.StartupPath + @"\newprintmodel.docx";
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();


                fs.Write(data, 0, data.Length);
                fs.Flush();
                data = null;
            }
        }
    }
   
}
