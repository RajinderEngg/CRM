using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxExtentions
{
    public static class Extentions
    {
        public static List<dynamic> ToDynamicList(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }

        public static dynamic ToDynamicObject(this DataRow dt) =>
            JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(dt));

        public static string ReadAllText(this Stream obj) => 
            new StreamReader(obj).ReadToEnd();

        public static T ToTypeof<T>(this Stream obj)=>
            JsonConvert.DeserializeObject<T>(new StreamReader(obj).ReadToEnd());

        public static T ToTypeof<T>(this Object obj) =>
            JsonConvert.DeserializeObject<T>(obj.ToString());

        public static T ToTypeof<T>(this string obj) =>
            JsonConvert.DeserializeObject<T>(obj);

        public static string ToJson(this object obj) =>
            JsonConvert.SerializeObject(obj);
    }
}
