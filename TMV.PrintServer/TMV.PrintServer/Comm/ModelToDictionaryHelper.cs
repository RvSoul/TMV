using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMV.PrintServer.Comm
{
    public static class ModelToDictionaryHelper
    {
        /// <summary>
        /// 第一种通过反射进行转换
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> ModelToDic<T>(this T obj) where T : class
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            if (obj == null)
            {
                return map;
            }
            Type t = obj.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (var item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(obj, null);
                if (value != null)
                {
                    map.Add(name, value.ToString());
                }
                else
                {
                    map.Add(name, "");
                }
            }
            return map;
        }

        /// <summary>
        /// 借助JsonConvert快速实现转换
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> ModelToDic2<T>(this T obj) where T : class
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            if (obj == null)
            {
                return map;
            }
            var objstr = JsonConvert.SerializeObject(obj);
            //string(json) 转 Dictionary
            map = JsonConvert.DeserializeObject<Dictionary<string, string>>(objstr);
            return map;
        }

    }
}
