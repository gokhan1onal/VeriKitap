/****************************************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                                               */
/* d:2022-10-05 o:Gökhan ÖNAL  e:CheckIn sayfası                                                                */
/****************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI;
using VeriKitap.DataModule;
using VeriKitap.Utils;

namespace VeriKitap
{
    public partial class CheckIn : Page
    {
        static string checkOutId = "", kitapId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            VPConnection sqlConn = null;
            try
            {
                kitapId = Request.QueryString["id"]?.ToString() ?? "";
                if (string.IsNullOrEmpty(kitapId))  // Kitap ID'si gönderilmeden sayfaya erişilirse ana sayfaya yönlendir
                    Response.Redirect("default.aspx");

                sqlConn = VPConnectionFactory.GetDefaultConnection();
                string durum = sqlConn.ExecuteScalar("SELECT Durum FROM Kitaplar WHERE Id=@P1", new KeyValue("@P1", kitapId))?.ToString() ?? "";
                if (string.IsNullOrEmpty(durum) || durum.Equals("0")) // Durum boş gelirse böyle bir kitap yoktur veya bu id'ye sahip kitap zaten checkIn yapılmışsa ana sayfaya yönlendir
                    Response.Redirect("default.aspx");

                // CheckOut bilgisi alınıyor
                DataRow drCheckOut = sqlConn.GetDataRow("SELECT TOP 1 Id,AdSoyad,CepTel,Teslim FROM CheckOuts WHERE KitapId=@Id ORDER BY Id DESC", new KeyValue("@Id", kitapId));
                checkOutId = drCheckOut["Id"].ToString();
                txtTeslimAlan.Text = drCheckOut["AdSoyad"].ToString();
                txtCepTel.Text = drCheckOut["CepTel"].ToString();
                txtGercekTeslim.Text = DateTime.Now.ToString("dd.MM.yyyy");

                DateTime teslimTarihi = new DateTime(1900, 1, 1);
                DateTime.TryParse(drCheckOut["Teslim"].ToString(), out teslimTarihi);

                txtTeslimTarihi.Text = teslimTarihi.ToString("dd.MM.yyyy");
                txtCeza.Text = MyGetCezaTutari(teslimTarihi).ToString() + " TL";
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

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            VPConnection sqlConn = null;
            try
            {
                string sqlText = "UPDATE CheckOuts SET GercekTeslim=@GercekTeslim,Ceza=@Ceza WHERE Id=@Id";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@GercekTeslim", DateTime.Now);
                parameters.Add("@Ceza", Convert.ToDouble(txtCeza.Text.Replace("TL","")));
                parameters.Add("@Id", checkOutId);
                sqlConn = VPConnectionFactory.GetDefaultConnection();

                // Bir hata oluşursa geri almak için transaction başlatılıyor
                sqlConn.BeginTransaction();
                int status = sqlConn.ExecuteNonQuery(sqlText, parameters);
                if (status > 0)
                {
                    // CheckOut yapılabildiyse, kitap durumunu güncelle
                    sqlConn.ExecuteNonQuery("UPDATE Kitaplar SET Durum=@Durum WHERE Id=@Id",
                        new KeyValue("@Durum", "0"), new KeyValue("@Id", kitapId));
                }
                sqlConn.Commit(); // transaction bitiriliyor
            }
            catch (Exception ex)
            {
                ErrorUtils.SaveError(MethodBase.GetCurrentMethod().Name, Request.Url.OriginalString, ex);
            }
            finally
            {
                // Sql connection transaction modundaysa, işlemler geri alınıyor...
                if (sqlConn.InTransaction())
                    sqlConn.Rollback();

                if (sqlConn != null)
                    sqlConn.Dispose();

                // Kullanıcı işlemden sonra ana sayfaya yönlendirilsin
                Response.Redirect("default.aspx");
            }
        }

        /// <summary>
        /// Add: Gökhan ÖNAL
        /// Kullanıcı kitabı geç teslim ettiyse, geç teslim ettiği İŞ GÜNÜ kadar para cezası hesaplayıp geriye döndürür.
        /// </summary>
        /// <returns></returns>
        private double MyGetCezaTutari(DateTime pTeslimTarihi)
        {
            try
            {
                double tutar = 0;
                DateTime tarih = pTeslimTarihi;
                while (DateTime.Now.Date > tarih.Date)
                {
                    // döngüde ki tarih iş günü ise 5 TL para cezası ekle
                    if (DayUtils.isWorkDay(tarih))
                        tutar += 5;

                    tarih = tarih.AddDays(+1);
                }

                return tutar;
            }
            catch (Exception ex)
            {
                ErrorUtils.SaveError(MethodBase.GetCurrentMethod().Name, Request.Url.OriginalString, ex);
                throw null;
            }
        }
    }
}