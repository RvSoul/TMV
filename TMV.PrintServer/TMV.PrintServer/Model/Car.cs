using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer.Model
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    [SugarTable("Car")]
    public class Car
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DefaultValue("车牌号")]
        public string PlateNumber { get; set; }


        /// <summary>
        /// 车辆类型
        /// </summary> 
        public int Type { get; set; }
        /// <summary>
        /// 车厢长
        /// </summary> 
        public string SizeC { get; set; }
        /// <summary>
        /// 车厢宽
        /// </summary> 
        public string SizeK { get; set; }
        /// <summary>
        /// 车厢高
        /// </summary> 
        public string SizeG { get; set; }
        /// <summary>
        /// 测量人员
        /// </summary> 
        public string Surveyor { get; set; }
        /// <summary>
        /// 拉筋一
        /// </summary> 
        public string TieBar1 { get; set; }
        /// <summary>
        /// 拉筋二
        /// </summary> 
        public string TieBar2 { get; set; }
        /// <summary>
        /// 拉筋三
        /// </summary> 
        public string TieBar3 { get; set; }
        /// <summary>
        /// 额定载重量
        /// </summary> 
        public int RatedWeight { get; set; }
        /// <summary>
        /// 行驶证号
        /// </summary> 
        public string ExerciseCode { get; set; }
        /// <summary>
        /// 电子标签号
        /// </summary> 
        public string TAgCode { get; set; }
        /// <summary>
        /// 空水空油重量
        /// </summary> 
        public int EmptyWeight { get; set; }
        /// <summary>
        /// 满水满油重量
        /// </summary> 
        public int FullWeight { get; set; }
        /// <summary>
        /// 驾驶员名称
        /// </summary> 
        public string DriverName { get; set; }
        /// <summary>
        /// 性别
        /// </summary> 
        public int Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary> 
        public int Age { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary> 
        public string NativePlace { get; set; }
        /// <summary>
        /// 驾照编号
        /// </summary> 
        public string DrivingCode { get; set; }
        /// <summary>
        /// 建档人
        /// </summary> 
        public string AddName { get; set; }
        /// <summary>
        /// 建档时间
        /// </summary> 
        public DateTime AddTime { get; set; }
    }
}
