using Aspose.Words.XAttr;
using Spire.Doc;
using System.Drawing;
using System.Drawing.Printing;
using TMV.PrintServer.Comm;
using TMV.PrintServer.Model;
using static System.Drawing.Printing.PrinterSettings;

namespace TMV.PrintServer
{
    public class PrintServer
    {
		DbContext dbContext;
		string msg = "";
		public PrintServer(DbContext _dbContext)
		{
			dbContext= _dbContext;
		}
		public string BeginPtint(string msgStr)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(msgStr))
				{
					WordUtil wordUtil = new();
					var printdata = dbContext.db.Queryable<TransportationRecords, TransportPlan, Car, ScalageRecords>((a, b, c, d) => a.CarId == c.Id && a.CollieryId == b.Id && a.Id == d.TId)
					.Where((a, b, c) => c.PlateNumber.ToString() == msgStr)
					.Select((a, b, c, d) => new PrintDto()
					{
						unit = b.MEIKDW,
						scalenumber = d.ScaleId.ToString(),
						number = a.Code,
						shipper = b.SendUnit,
						consignee = b.ReceiptUnit,
						name = b.CargoName,
						carryunit = b.Carrier,
						specification = "",
						splatenumber = c.PlateNumber,
						roughweight = a.RoughWeight.ToString(),
						tareweight = a.TareWeight.ToString(),
						buckleweight = a.KouWeight.ToString(),
						netweight = a.NetWeight.ToString(),
						shipnumber = b.ShipName,
						truckscar = "",
						emptycar = "",
						trucktime = "",
						lighttime = ""
					}).Mapper(it =>
					{
						it.scalenumber = dbContext.db.Queryable<Scale>().Where(x => x.Id.ToString() == it.scalenumber).First()?.Name;
					}).First();
					if (printdata == null)
					{
						msg += $"\r\n没有找到车牌号为{msgStr}的记录";
					}
					else
					{
						var strm = wordUtil.WordWrite(printdata);
						Print(strm);
					}
				}
			}
			catch (Exception ex)
			{
				msg += "\r\n解析数据错误：" + ex.Message;
			}
			return msg;
		}
		public string Print(MemoryStream memStream)
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
					msg+= "\r\n请检查打印机是否正常连接";
                }
            }
            finally
            {
                //doc.Close();
            }
			return msg;

		}
		
	}
}
