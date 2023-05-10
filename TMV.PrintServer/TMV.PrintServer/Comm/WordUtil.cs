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
            SaveToFile(ms);
            return ms;
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
