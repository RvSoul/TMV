using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TMV.Core.CM
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    [SugarTable("Car")]
    public partial class Car
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DefaultValue("车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 空水空油重量
        /// </summary>
        [DefaultValue("空水空油重量")]
        public int EmptyWeight { get; set; }

        /// <summary>
        /// 满水满油重量
        /// </summary>
        [DefaultValue("满水满油重量")]
        public int FullWeight { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        [DefaultValue("驾驶员名称")]
        public string DriverName { get; set; }

        /// <summary>
        /// 驾照编号
        /// </summary>
        [DefaultValue("驾照编号")]
        public string DrivingCode { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DefaultValue("注册时间")]
        public DateTime AddTime { get; set; }
         
    }
}
