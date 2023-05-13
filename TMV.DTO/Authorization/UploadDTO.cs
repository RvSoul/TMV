using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.DTO.Authorization
{
    public class UploadDTO
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 过衡序号，代表数据唯一性
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CHEPH { get; set; }

        /// <summary>
        /// 货物名称，名称中包含汉字“煤”
        /// </summary>
        public string PINZ { get; set; }

        /// <summary>
        /// 煤矿单位，与燃料系统保持一致
        /// </summary>
        public string MEIKDW { get; set; }

        /// <summary>
        /// 供应商名称，与燃料系统保持一致
        /// </summary>
        public string GONGYS { get; set; }

        /// <summary>
        /// 发站，与燃料系统保持一致
        /// </summary>
        public string FAZ { get; set; }

        /// <summary>
        /// 到站，与燃料系统保持一致
        /// </summary>
        public string DAOZ { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public int MAOZ { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        public int PIZ { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        public string FAHRQ { get; set; }

        /// <summary>
        /// 到货日期，车辆回皮结束后离厂时间
        /// </summary>
        public string DAOHRQ { get; set; }

        /// <summary>
        /// 口径类型
        /// </summary>
        public string KOUJ { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public string YUNSFS { get; set; }

        /// <summary>
        /// 检斤方式
        /// </summary>
        public string JIANJFS { get; set; }

        /// <summary>
        /// 进厂司磅员
        /// </summary>
        public string JINCJJY { get; set; }

        /// <summary>
        /// 出厂司磅员
        /// </summary>
        public string CHUCJJY { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        public string JINCSJ { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public string CHUCSJ { get; set; }

        /// <summary>
        /// 扣重
        /// </summary>
        public decimal KOUZ { get; set; }

        /// <summary>
        /// 进厂秤号
        /// </summary>
        public string JINCHH { get; set; }

        /// <summary>
        /// 出厂秤号
        /// </summary>
        public string CHUCHH { get; set; }

        /// <summary>
        /// 承运单位
        /// </summary>
        public string CHENGYDW { get; set; }

        /// <summary>
        /// 车次
        /// </summary>
        public string CHEC { get; set; }
         

    }
}
