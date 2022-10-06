/*****************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                        */
/* d:2022-05-10 o:Gökhan ÖNAL  e:Günlerle ilgili metotların bulunduğu util               */
/*****************************************************************************************/

using System;
using System.Collections.Generic;

namespace VeriKitap.Utils
{
    public class DayUtils
    {

        /// <summary>
        /// Add: Gökhan ÖNAL
        /// <paramref name="pDate"/> parametresi ile belirtilen tarih İŞ GÜNÜ mü onu tespit eder.
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns>Evet ise true, değilse false döndürür.</returns>
        public static bool isWorkDay(DateTime pDate)
        {
            try
            {
                HashSet<DateTime> Holidays = new HashSet<DateTime>();
                Holidays.Add(new DateTime(DateTime.Now.Year, 1, 1));
                Holidays.Add(new DateTime(DateTime.Now.Year, 4, 23));
                Holidays.Add(new DateTime(DateTime.Now.Year, 5, 1));
                Holidays.Add(new DateTime(DateTime.Now.Year, 5, 19));
                Holidays.Add(new DateTime(DateTime.Now.Year, 7, 15));
                Holidays.Add(new DateTime(DateTime.Now.Year, 8, 30));
                Holidays.Add(new DateTime(DateTime.Now.Year, 10, 29));
                Holidays.Add(new DateTime(DateTime.Now.Year, 12, 31));

                return pDate.DayOfWeek != DayOfWeek.Sunday && pDate.DayOfWeek != DayOfWeek.Saturday && !Holidays.Contains(pDate.Date) ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}