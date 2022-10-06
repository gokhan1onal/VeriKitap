/*****************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                        */
/* d:2022-05-10 o:Gökhan ÖNAL  e:Connection                                              */
/*****************************************************************************************/

using System.Collections.Generic;
using System.Data;

namespace VeriKitap.DataModule
{
    public interface VPConnection
    {

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısını açar
        /// </summary>
        void Open();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısını kapatır
        /// </summary>
        void Close();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Açıksa veritabanı bağlantısını kapatır, dispose eder
        /// </summary>
        void Dispose();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Transaction başlatır
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Mevcut Transaction sonlandırılır
        /// </summary>
        void Commit();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Yapılan işlemleri geri alır
        /// </summary>
        void Rollback();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı bağlantısı açık mı?
        /// </summary>
        /// <returns></returns>
        bool IsOpen();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Veritabanı Transaction modunda mı? 
        /// </summary>
        /// <returns></returns>
        bool InTransaction();

        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu varsa <paramref name="cmdParams"/> parametreleri ile çalıştırır.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Sorgu parametreleri, parametre yoksa null gönderilir</param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText, Dictionary<string, object> cmdParams = null);

        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu varsa <paramref name="cmdParams"/> parametreleri ile çalıştırır.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Key ve Value belirterek sorgunun varsa parametrelerini ekleyebilirsiniz. Key string Value ise object değer alabilir</param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText, params KeyValue[] cmdParams);

        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu varsa <paramref name="cmdParams"/> parametreleri ile çalıştırıp geriye object değer döndürür.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Sorgu parametreleri, parametre yoksa null gönderilir</param>
        /// <returns></returns>
        object ExecuteScalar(string cmdText, Dictionary<string, object> cmdParams = null);

        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu varsa <paramref name="cmdParams"/> parametreleri ile çalıştırıp geriye object değer döndürür.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Key ve Value belirterek sorgunun varsa parametrelerini ekleyebilirsiniz. Key string Value ise object değer alabilir</param>
        /// <returns></returns>
        object ExecuteScalar(string cmdText, params KeyValue[] cmdParams);

        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu varsa <paramref name="cmdParams"/> parametreleri ile çalıştırıp geriye DataTable döndürür.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Sorgu parametreleri, parametre yoksa null gönderilir</param>
        /// <returns></returns>
        DataTable GetDataTable(string cmdText, Dictionary<string, object> cmdParams = null);


        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu çalıştırıp, geriye DataRow döndürür.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Sorgu parametreleri, parametre yoksa null gönderilir</param>
        /// <returns></returns>
        DataRow GetDataRow(string cmdText, Dictionary<string, object> pParams = null);


        /// <summary>
        /// Add Gökhan ÖNAL
        /// <paramref name="cmdText"/> ile gönderilen SQL sorgusunu çalıştırıp, geriye DataRow döndürür.
        /// </summary>
        /// <param name="cmdText">SQL Sorgusu</param>
        /// <param name="cmdParams">Key ve Value belirterek sorgunun varsa parametrelerini ekleyebilirsiniz. Key string Value ise object değer alabilir</param>
        /// <returns></returns>
        DataRow GetDataRow(string cmdText, params KeyValue[] cmdParams);

    }

    public class KeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }


        public KeyValue(string pKey, object pValue)
        {
            Key = pKey;
            Value = pValue;
        }

    }
}