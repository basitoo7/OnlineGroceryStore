using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace GroceryStore.Models
{
    public class DbHelper
    {
        //private Microsoft.Practices.EnterpriseLibrary.Data.Database db;
        //private DbCommand dbCommand;

        //public DbHelper()
        //{
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("dbBawerchikhana");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static IList<T> MapDataTableToList<T>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }

            IList<T> list = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                T instance = Activator.CreateInstance<T>();
                dr.Fill<T>(ref instance);
                list.Add(instance);
            }

            return list;
        }


        public static IList<T> MapDataReaderToList<T>(System.Data.SqlClient.SqlDataReader dr)
        {
            if (dr == null || !dr.HasRows)
            {
                return new List<T>();
            }

            IList<T> list = new List<T>();
            while (dr.Read())
            {
                T instance = Activator.CreateInstance<T>();
                dr.Fill<T>(ref instance);
                list.Add(instance);
            }
            return list;
        }

        public static T MapDataTableToObject<T>(DataTable dt)
        {

            T instance = default(T);
            if (dt == null)
            {
                return instance;
            }


            if (dt.Rows.Count > 0)
            {

                instance = Activator.CreateInstance<T>();

                DataRow dr = dt.Rows[0];
                dr.Fill<T>(ref instance);

            }


            return instance;
        }


        public static T MapDataReaderToObject<T>(System.Data.SqlClient.SqlDataReader dr)
        {
            T instance = Activator.CreateInstance<T>();
            if (dr == null && dr.HasRows)
            {
                return instance;
            }

            while (dr.Read())
            {
                dr.Fill<T>(ref instance);
            }
            return instance;
        }

        public static string BuildQuery(List<string> columnNames, string tableName, string whereClause)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(String.Join(",", columnNames.ToArray()));
                query.Append(" FROM ");
                query.Append(tableName);
                query.Append(" ");
                query.Append(whereClause);
                return query.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static void logSQLCommand(DbCommand DbCommand)
        //{
        //    String str = String.Empty;
        //    str = String.Format("\n\n Command:{0} \n\r Parameter Details \n\r", DbCommand.CommandText);
        //    foreach (SqlParameter item in DbCommand.Parameters)
        //    {
        //        str += String.Format("Name : {0}, value {1} \n\r", item.ParameterName, item.Value);
        //    }

        //    System.Configuration.
        //    using (StreamWriter file = new StreamWriter(System.Configuration.ConfigurationManager.AppSettings["SqlCommandFilePath"].ToString(), true, Encoding.UTF8))
        //    {
        //        file.Write(str);
        //    }
        //}

        public static string SerializeList<T>(List<T> tData)
        {
            XmlSerializer s = new XmlSerializer(typeof(List<T>));
            StringWriter outStream = new StringWriter();
            s.Serialize(outStream, tData);

            return outStream.ToString();
        }
        public static List<T> DeserializeList<T>(string tData)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            StringReader reader = new StringReader(tData);

            return (List<T>)serializer.Deserialize(reader);
        }
        public static string Serialize<T>(T tData)
        {
            XmlSerializer s = new XmlSerializer(tData.GetType());
            StringWriter outStream = new StringWriter();
            s.Serialize(outStream, tData);

            return outStream.ToString();
        }
        public static T Deserialize<T>(string tData)
        {
            var serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(tData);

            return (T)serializer.Deserialize(reader);
        }
    }

    public static class DataRowExtention
    {
        /// <summary>
        /// Fills the T object this the row data. (the T object must have "ColumnAttribute" attribute on it's fields or properties)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object to fill</param>

        public static void Fill<T>(this DataRow oRow, ref T obj)
        {
            if (oRow == null)
                throw new ArgumentNullException("DataRow cannot be null");
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null"); //Set Fields


            //Set Properties
            PropertyInfo[] oProperties = typeof(T).GetProperties();
            for (int i = 0; i < oProperties.Length; i++)
            {
                PropertyInfo oProperty = oProperties[i];
                if (!oProperty.CanWrite)
                    continue;

                if (oRow.Table.Columns[oProperty.Name] != null) // checking is column  exist in data table or not
                {
                    if (oRow[oProperty.Name] != System.DBNull.Value)
                        oProperty.SetValue(obj, oRow[oProperty.Name], null);
                    else
                        oProperty.SetValue(obj, null, null);
                }




            }
        }



        public static void Fill<T>(this System.Data.SqlClient.SqlDataReader oRow, ref T obj)
        {

            //Set Properties
            PropertyInfo[] oProperties = typeof(T).GetProperties();
            for (int i = 0; i < oProperties.Length; i++)
            {
                PropertyInfo oProperty = oProperties[i];
                if (!oProperty.CanWrite)
                    continue;

                if (oRow.HasColumn(oProperty.Name))// checking if column  exists in data table or not
                {
                    if (oRow[oProperty.Name] != System.DBNull.Value)
                        oProperty.SetValue(obj, oRow[oProperty.Name], null);
                    //else
                    //    oProperty.SetValue(obj, oColumnAttributeName.DefaultValue, null);
                }

            }
        }

        public static bool HasColumn(this IDataRecord r, string columnName)
        {
            try
            {
                return r.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        /// This Function will Accept  any List  and will return the Data Table of that list
        /// </summary>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static void DeepCopy<T>(this IList<T> result, IEnumerable<T> listToCopy) where T : class
        {

            if (listToCopy != null && listToCopy.Count() != 0)
            {
                if (typeof(T).IsSerializable)
                {
                    foreach (T mem in listToCopy)
                    {
                        MemoryStream ms = new MemoryStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ms, mem);
                        ms.Position = 0;

                        T resObj = (T)bf.Deserialize(ms);
                        result.Add(resObj);
                    }
                }
                else
                {
                    throw new ApplicationException("Type not serializable");
                }

            }
        }


        public static T DeepCopy<T>(this T objectToCopy) where T : class
        {
            if (objectToCopy == null)
                return null;

            if (objectToCopy != null)
            {
                if (typeof(T).IsSerializable)
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, objectToCopy);

                    return (T)bf.Deserialize(ms);

                }
                else // for object who is not serializable
                {

                    return (T)Copy_Others(objectToCopy);
                    //result = Copy_Others();
                }

            }
            else
            {
                throw new ApplicationException("Type not serializable");
            }

        }

        public static void DeepCopy<T>(this T result, T objectToCopy) where T : class
        {
            if (objectToCopy != null)
            {
                if (typeof(T).IsSerializable)
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, objectToCopy);

                    result = (T)bf.Deserialize(ms);

                }
                else // for object who is not serializable
                {

                    result = (T)Copy_Others(objectToCopy);
                    //result = Copy_Others();
                }

            }
            else
            {
                throw new ApplicationException("Type not serializable");
            }






        }


        static object Copy_Others(object obj)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                string assemblyQualifiedName = type.AssemblyQualifiedName.Replace("[]", string.Empty);
                Type elementType = Type.GetType(assemblyQualifiedName, false, true);

                var array = obj as Array;
                if (elementType != null)
                {
                    Array copied = Array.CreateInstance(elementType, array.Length);
                    for (int i = 0; i < array.Length; i++)
                    {
                        copied.SetValue(Copy_Others(array.GetValue(i)), i);
                    }
                    return Convert.ChangeType(copied, obj.GetType());
                }
                else
                {
                    return null;
                }
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());
                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue == null)
                        continue;
                    field.SetValue(toret, Copy_Others(fieldValue));
                }
                return toret;
            }
            else
                throw new ArgumentException("Unknown type");
        }
    }

    public static class DTConversionHelper
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            if (table.Columns.Count > 0 && table.Columns.Contains("OrderDetails"))
            {
                table.Columns.Remove("OrderDetails");
            }

            return table;
        }
    }
}