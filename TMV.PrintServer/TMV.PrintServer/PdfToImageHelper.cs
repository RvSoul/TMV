using O2S.Components.PDFRender4NET;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer
{
    public class PdfToImageHelper
    {
        public List<string> GetBase64StringArrayByPdfPath(string pdfPath)
        {
            if (pdfPath == null || pdfPath.Length == 0) return null;
            List<string> base64StringList = new List<string>();
            PDFFile pdfFile = PDFFile.Open(pdfPath);
            for (int index = 0; index < pdfFile.PageCount; index++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(index, 56 * 10);
                string base64Str = BitmapToBase64String(pageImage);
                if (base64Str != null && base64Str.Length > 0)
                {
                    base64StringList.Add(base64Str);
                }
            }
            pdfFile.Dispose();
            return base64StringList;
        }

        private  string ImageToBase64String(Image Picture)
        {
            MemoryStream ms = new MemoryStream();
            if (Picture == null)
                return null;
            Picture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] BPicture = new byte[ms.Length];
            BPicture = ms.GetBuffer();
            return Convert.ToBase64String(BPicture);
        }


        private  string BitmapToBase64String(Bitmap bitmap)
        {
            // 1.先将BitMap转成内存流
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            // 2.再将内存流转成byte[]并返回
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Flush();
            ms.Close();
            ms.Dispose();
            return Convert.ToBase64String(bytes);
        }
    }
}
