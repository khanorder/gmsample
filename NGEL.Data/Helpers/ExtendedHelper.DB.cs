using NGEL.Data.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;
using Newtonsoft.Json;

namespace NGEL.Data.Helpers
{
    public static partial class ExtendedHelper
    {
        public static bool CheckDBEnableData(this MySqlDataReader rdr, int index)
        {
            if (rdr.FieldCount <= index)
            {
                return false;
            }

            if (rdr.IsDBNull(index) == true)
            {
                return false;
            }

            return true;
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out int retData)
        {
            retData = -1;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index].GetType() == typeof(Int64))
            {
                retData = (int)((Int64)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt64))
            {
                retData = (int)((UInt64)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(Int32))
            {
                retData = (int)((Int32)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt32))
            {
                retData = (int)((UInt32)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(Int16))
            {
                retData = (int)((Int16)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt16))
            {
                retData = (int)((UInt16)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(System.Byte))
            {
                retData = (System.Byte)rdr[index];
            }
            else if (rdr[index].GetType() == typeof(System.SByte))
            {
                retData = (System.SByte)rdr[index];
            }
            else if (rdr[index] is string)
            {
                retData = int.Parse(((string)rdr[index]).ToString());
            }
            else
            {
                retData = (int)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out int? retData)
        {
            retData = -1;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (null == rdr[index])
            {
                retData = null;
            }
            else if (rdr[index].GetType() == typeof(Int64))
            {
                retData = (int)((Int64)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt64))
            {
                retData = (int)((UInt64)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(Int32))
            {
                retData = (int)((Int32)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt32))
            {
                retData = (int)((UInt32)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(Int16))
            {
                retData = (int)((Int16)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(UInt16))
            {
                retData = (int)((UInt16)rdr[index]);
            }
            else if (rdr[index].GetType() == typeof(System.Byte))
            {
                retData = (System.Byte)rdr[index];
            }
            else if (rdr[index].GetType() == typeof(System.SByte))
            {
                retData = (System.SByte)rdr[index];
            }
            else if (rdr[index] is string)
            {
                retData = int.Parse(((string)rdr[index]).ToString());
            }
            else
            {
                retData = (int)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out Int64 retData)
        {
            retData = 0;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }
            
            if (rdr[index] is string)
            {
                retData = Int64.Parse(((string)rdr[index]).ToString());
            }
            else if (rdr[index] is int)
            {
                retData = Int64.Parse(((int)rdr[index]).ToString());
            }
            else if (rdr[index] is UInt32)
            {
                retData = Int64.Parse(((UInt32)rdr[index]).ToString());
            }
            else if (rdr[index] is UInt64)
            {
                retData = Int64.Parse(((UInt64)rdr[index]).ToString());
            }
            else
            {
                retData = (Int64)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out UInt64 retData)
        {
            retData = 0;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index] is string)
            {
                retData = UInt64.Parse(((string)rdr[index]).ToString());
            }
            else if (rdr[index] is int)
            {
                retData = UInt64.Parse(((int)rdr[index]).ToString());
            }
            else if (rdr[index] is UInt32)
            {
                retData = UInt64.Parse(((UInt32)rdr[index]).ToString());
            }
            else if (rdr[index] is Int64)
            {
                retData = UInt64.Parse(((Int64)rdr[index]).ToString());
            }
            else
            {
                retData = (UInt64)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out double retData)
        {
            retData = 0;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index] is string)
            {
                retData = double.Parse(((string)rdr[index]).ToString());
            }
            else if (rdr[index] is decimal)
            {
                retData = double.Parse(((decimal)rdr[index]).ToString());
            }
            else
            {
                retData = (double)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out float retData)
        {
            retData = 0;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index] is string)
            {
                retData = float.Parse(((string)rdr[index]).ToString());
            }
            else if (rdr[index] is decimal)
            {
                retData = float.Parse(((decimal)rdr[index]).ToString());
            }
            else
            {
                retData = (float)rdr[index];
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out string retData)
        {
            retData = "";

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            retData = ((string)rdr[index]).ToString();
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out DateTime retData)
        {
            retData = new DateTime();

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index] is string)
            {
                retData = Convert.ToDateTime(rdr[index]);
            }
            else
            {
                retData = DateTime.SpecifyKind(((DateTime)rdr[index]), DateTimeKind.Utc);
            }

        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out bool retData)
        {
            retData = false;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index].GetType() == typeof(Int64))
            {
                retData = ((Int64)rdr[index]) != 0;
            }
            else if (rdr[index].GetType() == typeof(Int32))
            {
                retData = ((Int32)rdr[index]) != 0;
            }
            else if (rdr[index] is string)
            {
                retData = (int.Parse(((string)rdr[index]).ToString())) != 0;
            }
            else if (rdr[index].GetType() == typeof(System.Byte))
            {
                retData = ((System.Byte)rdr[index]) != 0;
            }
            else if (rdr[index].GetType() == typeof(System.SByte))
            {
                retData = ((System.SByte)rdr[index]) != 0;
            }
            else
            {
                retData = (int)rdr[index] != 0;
            }
        }

        public static void ConvertDBData(this MySqlDataReader rdr, int index, out Guid retData)
        {
            retData = Guid.Empty;

            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            if (rdr[index].GetType() == typeof(Guid))
            {
                retData = (Guid)rdr[index];
            }
            else if (string.IsNullOrWhiteSpace((string)rdr[index]))
            {
                retData = Guid.Empty;
            }
            else
            {
                retData = Guid.Parse(((string)rdr[index]).ToString());
            }
        }

        public static void ConvertDBData<T>(this MySqlDataReader rdr, int index, out List<T> retData)
        {
            retData = new List<T>();
            if (rdr.CheckDBEnableData(index) == false)
            {
                return;
            }

            try
            {
                var data = JsonConvert.DeserializeObject<List<T>>(((string)rdr[index]).ToString());
                if (null != data)
                    retData = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
