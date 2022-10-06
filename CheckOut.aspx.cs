/****************************************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                                               */
/* d:2022-10-05 o:Gökhan ÖNAL  e:CheckOut sayfası                                                               */
/****************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using VeriKitap.DataModule;
using VeriKitap.Utils;

namespace VeriKitap
{
    public partial class CheckOut : Page
    {
        static string kitapId = "";
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
                if(string.IsNullOrEmpty(durum) || durum.Equals("1")) // Durum boş gelirse böyle bir kitap yoktur veya bu id'ye sahip kitap zaten checkout yapılmışsa ana sayfaya yönlendir
                    Response.Redirect("default.aspx");
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
                string sqlText = "INSERT INTO CheckOuts (KitapId,AdSoyad,CepTel,Tarih,Teslim,GercekTeslim,Ceza) VALUES (@KitapId,@AdSoyad,@CepTel,@Tarih,@Teslim,@GercekTeslim,@Ceza)";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@KitapId", kitapId);
                parameters.Add("@AdSoyad", txtTeslimAlan.Text);
                parameters.Add("@CepTel", txtCepTel.Text.Replace(" ", ""));
                parameters.Add("@Tarih", DateTime.Now);
                parameters.Add("@Teslim", MyGetTeslimGunu());
                parameters.Add("@GercekTeslim", new DateTime(1900, 1, 1));
                parameters.Add("@Ceza", 0);
                sqlConn = VPConnectionFactory.GetDefaultConnection();

                // Bir hata oluşursa geri almak için transaction başlatılıyor
                sqlConn.BeginTransaction();
                int status = sqlConn.ExecuteNonQuery(sqlText, parameters);
                if(status > 0)
                {
                    // CheckOut yapılabildiyse, kitap durumunu güncelle
                    sqlConn.ExecuteNonQuery("UPDATE Kitaplar SET Durum=@Durum WHERE Id=@Id", 
                        new KeyValue("@Durum", "1"), new KeyValue("@Id", kitapId));
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
        /// Kullanıcının bugünden itibaren 15 İŞ GÜNÜ sonrası teslim etmesi gereken günü bulup geri döndürür.
        /// </summary>
        /// <returns></returns>
        private DateTime MyGetTeslimGunu()
        {
            try
            {
                int count = 15;    //Teslim etme tarihinden sonra 15 iş günü
                DateTime tarih = DateTime.Now; // Bugünün tarihinden itibaren
                while (count > 0)
                {
                    // ilgili tarih iş günüyse, eklenecek iş günü sayısını azalt
                    if (DayUtils.isWorkDay(tarih))
                        count--;

                    tarih = tarih.AddDays(1);
                }

                return tarih;
            }
            catch (Exception ex)
            {
                ErrorUtils.SaveError(MethodBase.GetCurrentMethod().Name, Request.Url.OriginalString, ex);
                throw null;
            }
        }
    }
}