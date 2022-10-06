/*****************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                        */
/* d:2022-05-10 o:Gökhan ÖNAL  e:MSSQL Connection                                        */
/*****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VeriKitap.DataModule
{
    public class VPMsSQL : VPConnection
    {

        public string url { get; private set; }
        public object connection { get; private set; }
        public object transaction { get; private set; }
        public int timeOut { get; private set; }

        public VPMsSQL(string connectionString)
        {
            url = connectionString;
            connection = new SqlConnection();
            transaction = null;
            timeOut = 3600;
        }


        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısını açar
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                if (((SqlConnection)connection).State == ConnectionState.Closed)
                {
                    ((SqlConnection)connection).ConnectionString = url;
                    ((SqlConnection)connection).Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısını kapatır
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            try
            {
                if (((SqlConnection)connection).State == ConnectionState.Open)
                    ((SqlConnection)connection).Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısını sonlandırıp dispose eder
        /// </summary>
        /// <returns></returns>
        public void Dispose()
        {
            try
            {
                if (connection != null)
                {
                    if (((SqlConnection)connection).State == ConnectionState.Open)
                        ((SqlConnection)connection).Close();

                    ((SqlConnection)connection).Dispose();
                    connection = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Transaction başlatır
        /// </summary>
        /// <returns></returns>
        public void BeginTransaction()
        {
            try
            {
                transaction = ((SqlConnection)connection).BeginTransaction();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Commit()
        {
            try
            {
                ((SqlTransaction)transaction).Commit();
                transaction = null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// İşlemleri geri almak için kullanılır
        /// </summary>
        /// <returns></returns>
        public void Rollback()
        {
            try
            {
                ((SqlTransaction)transaction).Rollback();
                transaction = null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısı açık mı? Evet ise true döndürür
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            try
            {
                if ((connection != null) && (((SqlConnection)connection).State == ConnectionState.Open)) return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı Transaction modunda mı? Evet ise true döndürür
        /// </summary>
        /// <returns></returns>
        public bool InTransaction()
        {
            try
            {
                if (((SqlTransaction)transaction) == null) return false; else return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Add Gökhan ÖNAL
        /// SQL sorgusunu varsa parametrelerle çalıştırır geriye etkilenen kayıt sayısını döndürür
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Varsa parametreler, varsayılan değer null</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, Dictionary<string, object> cmdParams = null)
        {
            CommandType cmdType = CommandType.Text;
            try
            {
                if (cmdText.Substring(0, 1).Equals("$"))
                {
                    cmdText = cmdText.Substring(1);
                    cmdType = CommandType.StoredProcedure;
                }
                using (SqlCommand cmd = new SqlCommand(cmdText, (SqlConnection)connection))
                {
                    if (transaction != null)
                        cmd.Transaction = (SqlTransaction)transaction;
                    if (cmdParams != null)
                        foreach (var param in cmdParams)
                            cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                    cmd.CommandTimeout = timeOut;
                    cmd.CommandType = cmdType;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int ExecuteNonQuery(string cmdText, params KeyValue[] cmdParams)
        {
            try
            {
                Dictionary<string, object> W_Params = new Dictionary<string, object>();
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    W_Params.Add(cmdParams[i].Key, cmdParams[i].Value);
                }

                return ExecuteNonQuery(cmdText, W_Params);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Add Gökhan ÖNAL
        /// SQL sorgusunu varsa parametrelerle çalıştırır geriye DataTable döndürür
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Varsa parametreler, varsayılan değer null</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, Dictionary<string, object> cmdParams = null)
        {
            CommandType cmdType = CommandType.Text;
            try
            {
                if (cmdText.Substring(0, 1).Equals("$"))
                {
                    cmdText = cmdText.Substring(1);
                    cmdType = CommandType.StoredProcedure;
                }
                using (SqlCommand cmd = new SqlCommand(cmdText, (SqlConnection)connection))
                {
                    if (transaction != null)
                        cmd.Transaction = (SqlTransaction)transaction;
                    if (cmdParams != null)
                        foreach (var param in cmdParams)
                            cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                    cmd.CommandTimeout = timeOut;
                    cmd.CommandType = cmdType;
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Add Gökhan ÖNAL
        /// SQL sorgusunu varsa parametrelerle çalıştırır geriye DataTable döndürür
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Varsa parametreler, varsayılan değer null</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, params KeyValue[] cmdParams)
        {
            try
            {
                Dictionary<string, object> W_Params = new Dictionary<string, object>();
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    W_Params.Add(cmdParams[i].Key, cmdParams[i].Value);
                }

                return ExecuteScalar(cmdText, W_Params);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// SQL sorgusunu varsa parametrelerle çalıştırır geriye DataTable döndürür
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Varsa parametreler, varsayılan değer null</param>
        /// <returns></returns>
        public DataTable GetDataTable(string cmdText, Dictionary<string, object> cmdParams = null)
        {
            DataTable result;
            SqlCommand cmd;
            CommandType cmdType = CommandType.Text;
            try
            {
                if (cmdText.Substring(0, 1).Equals("$"))
                {
                    cmdText = cmdText.Substring(1);
                    cmdType = CommandType.StoredProcedure;
                }
                result = new DataTable();
                if (transaction == null) cmd = new SqlCommand(cmdText, (SqlConnection)connection);
                else cmd = new SqlCommand(cmdText, (SqlConnection)connection, (SqlTransaction)transaction);
                if (cmdParams != null)
                    foreach (var param in cmdParams)
                        cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                cmd.CommandTimeout = timeOut;
                cmd.CommandType = cmdType;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        /// <summary>
        /// Add Gökhan ÖNAL
        /// SQL sorgusunu varsa parametrelerle çalıştırır geriye DataRow döndürür
        /// </summary>
        /// <param name="pSqlText">SQL Sorgusu</param>
        /// <param name="pParams">Varsa parametreler, varsayılan değer null</param>
        /// <returns></returns>
        public DataRow GetDataRow(string pSqlText, Dictionary<string, object> pParams = null)
        {
            try
            {
                DataTable W_DT = GetDataTable(pSqlText, pParams);
                if (W_DT != null && W_DT.Rows.Count > 0)
                    return W_DT.Rows[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataRow GetDataRow(string cmdText, params KeyValue[] cmdParams)
        {
            try
            {
                Dictionary<string, object> W_Params = new Dictionary<string, object>();
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    W_Params.Add(cmdParams[i].Key, cmdParams[i].Value);
                }

                return GetDataRow(cmdText, W_Params);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}