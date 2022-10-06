/*****************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                        */
/* d:2022-05-10 o:Gökhan ÖNAL  e:Error Utils                                             */
/*****************************************************************************************/

using System;
using System.Collections.Generic;
using VeriKitap.DataModule;

namespace VeriKitap.Utils
{
    public class ErrorUtils
    {

        public static void SaveError(string pMethod, string pPage, Exception pException)
        {
            VPConnection sqlConn = null;
            try
            {
                // SQL Sorgusuyla parametrelerini hazırlıyoruz...
                string sqlText = "INSERT INTO Hatalar (Method,Sayfa,Hata,Tarih) VALUES (@Method,@Sayfa,@Hata,@Tarih)";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Method", pMethod);
                parameters.Add("@Sayfa", pPage);
                parameters.Add("@Hata", pException.Message);
                parameters.Add("@Tarih", DateTime.Now);
                
                // Veritabanı bağlantısını alıp, sorguyu çalıştırıyoruz
                sqlConn = VPConnectionFactory.GetDefaultConnection();
                sqlConn.ExecuteNonQuery(sqlText, parameters);
            }
            finally 
            {
                // Connection dispose ediliyor..
                if (sqlConn != null)
                    sqlConn.Dispose();
            }
        }

    }
}