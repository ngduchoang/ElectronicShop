using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCuaHangDienTu
{
    public class WebClass
    {
        public static string getConnectionStringByName(string connectionStringName)
        {
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/QuanLyCuaHangDienTu");//ten website = project
            System.Configuration.ConnectionStringSettings connString;
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings[connectionStringName];
                if (connString != null)
                    return connString.ConnectionString;
            }
            return "";
        }
    }
}