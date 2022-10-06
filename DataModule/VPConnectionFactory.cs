/*****************************************************************************************/
/* Copyright (c) 2022-2022, VeriPark Yazılım A.Ş.                                        */
/* d:2022-05-10 o:Gökhan ÖNAL  e:SQL Connection oluşturmak için kullanılan class         */
/*****************************************************************************************/

using System;
using System.Configuration;
using System.Reflection;

namespace VeriKitap.DataModule
{
    public class VPConnectionFactory
    {

        public static VPConnection CreateConnection(string pConnectionString)
        {
            if (string.IsNullOrEmpty(pConnectionString))
                return null;

            VPConnection result = new VPMsSQL(pConnectionString);
            try
            {
                result.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod().Name + "_" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Add Gökhan ÖNAL
        /// Web.config de ki varsayılan connection string'e ait connectionu oluşturup geriye döndürür. 
        /// </summary>
        /// <returns></returns>
        public static VPConnection GetDefaultConnection()
        {
            try
            {
                return CreateConnection(ConfigurationManager.ConnectionStrings["VeriKitapConnectionString"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod().Name + "_" + ex.Message);
            }
        }

    }
}