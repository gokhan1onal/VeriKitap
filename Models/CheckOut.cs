/****************************************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                                               */
/* d:2022-10-05 o:Gökhan ÖNAL  e:CheckOut classı                                                                */
/****************************************************************************************************************/
using System;

namespace VeriKitap.Models
{
    public class CheckOut
    {
        public int Id { get; set; }
        public int KitapId { get; set; }
        public string AdSoyad { get; set; }
        public string CepTel { get; set; }
        public string Tckn { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime Teslim { get; set; }
        public DateTime GercekTeslim { get; set; }
        public int Ceza { get; set; }

        public CheckOut()
        {
            KitapId = 0;
            AdSoyad = "";
            CepTel = "";
            Tckn = "";
            Tarih = DateTime.Now;
            Teslim = new DateTime(1, 1, 1900);
            GercekTeslim = new DateTime(1, 1, 1900);
            Ceza = 0;
        }
    }
}