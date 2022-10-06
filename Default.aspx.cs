/****************************************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                                               */
/* d:2022-10-05 o:Gökhan ÖNAL  e:Ana sayfa                                                                      */
/****************************************************************************************************************/

using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using VeriKitap.DataModule;
using VeriKitap.Utils;

namespace VeriKitap
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            VPConnection sqlConn = null;
            try
            {
                string sqlText = @"SELECT 
                                   Id,Baslik,Isbn,Yil,Fiyat,
                                   CASE
                                       WHEN Durum = 0  THEN ' Check-in'
                                       ELSE ' Check-out'
                                   END AS Durum
                                   FROM Kitaplar";
                sqlConn = VPConnectionFactory.GetDefaultConnection();
                DataTable dtBooks = sqlConn.GetDataTable(sqlText);
                gvBooks.DataSource = dtBooks;
                gvBooks.DataBind();
            }
            catch (Exception ex)
            {
                ErrorUtils.SaveError(MethodBase.GetCurrentMethod().Name, Request.Url.OriginalString, ex);
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Dispose();
            }
        }

    }
}