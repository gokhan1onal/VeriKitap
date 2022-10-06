/****************************************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                                               */
/* d:2022-10-05 o:Gökhan ÖNAL  e:Kitap object                                                                   */
/****************************************************************************************************************/

namespace VeriKitap.Models
{
    public class Kitap
    {

        public int Id { get; set; }

        public string Baslik { get; set; }

        public string Isbn { get; set; }

        public int Yil { get; set; }

        public double Fiyat { get; set; }


        public Kitap()
        {
            Baslik = "";
            Isbn = "";
            Yil = 0;
            Fiyat = 0;
        }

    }
}