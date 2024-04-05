using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace ToDoList.Utils
{
    public static class DataReaderExtensions
    {
        public static short? GetNullableInt16(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<short>();
            else
                return dr.GetInt16(colIndex);
        }

        public static short? GetNullableInt16(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableInt16(colIndex);
        }

        public static int GetInt16(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetInt16(colIndex);
        }
        public static int? GetNullableInt32(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<int>();
            else
                return dr.GetInt32(colIndex);
        }

        public static int? GetNullableInt32(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableInt32(colIndex);
        }
        public static int GetNullableInt32(this SqlDataReader dr, string colName, int defaultValue)
        {
            int colIndex = dr.GetOrdinal(colName);
            int? retval = dr.GetNullableInt32(colIndex);
            if (!retval.HasValue)
                retval = defaultValue;
            return retval.Value;
        }
        public static int GetInt32(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetInt32(colIndex);
        }
        public static long GetInt64(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetInt64(colIndex);
        }
        public static long? GetNullableInt64(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<long>();
            else
                return dr.GetInt64(colIndex);
        }
        public static long? GetNullableInt64(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableInt64(colIndex);
        }
        public static bool? GetNullableBool(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<bool>();
            else
                return dr.GetBoolean(colIndex);
        }
        public static bool GetNullableBool(this SqlDataReader dr, int colIndex, bool bdefault)
        {
            if (dr.IsDBNull(colIndex))
                return bdefault;
            else
                return dr.GetBoolean(colIndex);
        }
        public static bool GetNullableBool(this SqlDataReader dr, string colName, bool bdefault)
        {
            int colIndex = dr.GetOrdinal(colName);
            if (dr.IsDBNull(colIndex))
                return bdefault;
            else
                return dr.GetBoolean(colIndex);
        }
        public static bool? GetNullableBool(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableBool(colIndex);
        }

        public static bool GetBoolean(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetBoolean(colIndex);
        }

        public static double? GetNullableDouble(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<double>();
            else
                return dr.GetDouble(colIndex);
        }
        public static double? GetNullableDouble(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableDouble(colIndex);
        }
        public static double GetDouble(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetDouble(colIndex);
        }

        public static decimal? GetNullableDecimal(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<decimal>();
            else
                return dr.GetDecimal(colIndex);
        }
        public static decimal GetDecimal(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetDecimal(colIndex);
        }
        public static float? GetNullableFloat(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<float>();
            else
                return dr.GetFloat(colIndex);
        }
        public static float? GetNullableFloat(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableFloat(colIndex);
        }
        public static float GetFloat(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetFloat(colIndex);
        }
        public static string GetNullableString(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return null;
            else
                return dr.GetString(colIndex);
        }
        public static string GetNullableString(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableString(colIndex);
        }
        public static string GetNullableString(this SqlDataReader dr, string colName, string defaultVal)
        {
            int colIndex = dr.GetOrdinal(colName);
            string retval = dr.GetNullableString(colIndex);
            if (retval == null)
                retval = defaultVal;
            return retval;
        }
        public static DateTime? GetNullableDateTime(this SqlDataReader dr, int colIndex)
        {
            if (dr.IsDBNull(colIndex))
                return new Nullable<DateTime>();
            else
                return dr.GetDateTime(colIndex);
        }
        public static DateTime? GetNullableDateTime(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetNullableDateTime(colIndex);
        }
        public static DateTime GetDateTime(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetDateTime(colIndex);
        }
        public static DateTimeOffset GetDateTimeOffset(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetDateTimeOffset(colIndex);
        }
        public static string GetString(this SqlDataReader dr, string colName)
        {
            int colIndex = dr.GetOrdinal(colName);
            return dr.GetString(colIndex);
        }

        public static dynamic ToDynamic(this SqlDataReader dr)
        {
            dynamic retval = new ExpandoObject();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string name = dr.GetName(i);
                object value = dr[i];
                if (value == DBNull.Value)
                    value = null;

                (retval as IDictionary<string, Object>).Add(name, value);
            }
            return retval;
        }

        public static T Bind<T>(this SqlDataReader dr) where T : new()
        {
            T retval = new T();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var prop = typeof(T).GetProperties().Where(x => x.Name == dr.GetName(i)).SingleOrDefault();
                if (prop != null)
                {
                    bool notMapped = false;
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        NotMappedAttribute notMappedAttr = attr as NotMappedAttribute;
                        notMapped = notMappedAttr != null;
                        if (notMapped)
                            break;
                    }
                    if (!notMapped)
                    {
                        object objvalue = null;
                        try
                        {
                            objvalue = dr.GetFieldValue<object>(dr.GetOrdinal(prop.Name));
                        }
                        catch (Exception se)
                        {

                        }
                        if (objvalue == System.DBNull.Value)
                            objvalue = null;

                        prop.SetValue(retval, objvalue);
                    }
                }
            }

            return retval;
        }


        public static async Task<T> BindAsync<T>(this SqlDataReader dr) where T : new()
        {
            T retval = new T();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var prop = typeof(T).GetProperties().Where(x => x.Name == dr.GetName(i)).SingleOrDefault();
                if (prop != null)
                {
                    bool notMapped = false;
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        NotMappedAttribute notMappedAttr = attr as NotMappedAttribute;
                        notMapped = notMappedAttr != null;
                        if (notMapped)
                            break;
                    }
                    if (!notMapped)
                    {
                        object objvalue = null;
                        try
                        {
                            objvalue = await dr.GetFieldValueAsync<object>(dr.GetOrdinal(prop.Name));
                        }
                        catch (Exception se)
                        {

                        }
                        if (objvalue == System.DBNull.Value)
                            objvalue = null;

                        prop.SetValue(retval, objvalue);
                    }
                }
            }

            return retval;
        }
        public static List<T> BindToList<T>(this SqlDataReader dr) where T : new()
        {
            List<T> retval = new List<T>();
            while (dr.Read())
            {
                retval.Add(Bind<T>(dr));
            }
            return retval;
        }


        public static async Task<List<T>> BindToListAsync<T>(this SqlDataReader dr) where T : new()
        {
            List<T> retval = new List<T>();
            while (await dr.ReadAsync())
            {
                retval.Add(await BindAsync<T>(dr));
            }
            return retval;
        }
    }
}