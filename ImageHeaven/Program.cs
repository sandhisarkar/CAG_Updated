using System;
using System.Windows.Forms;
using NovaNet.Utils;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data.Odbc;
using System.Data;

namespace ImageHeaven
{
    public static class Program
    {

        //private txtLogger txlog;
        /// <summary>
        /// Program entry point.
        /// </summary>
        public static bool Logout = false;

        public static bool LogOut()
        {
            return Logout;
        }
        [STAThread]
        public static void IHMain(string[] args)
        {
            string yr;
            string mn;
            string dd;
            string qry = string.Empty;
            NovaNet.Utils.dbCon dbcon;
            OdbcConnection sqlCon;
            OdbcDataAdapter sqlAdap;
            OdbcCommand cmd;
            //txtLogger txLog = new txtLogger(Path.GetDirectoryName(Application.ExecutablePath), LogLevel.Beta);
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                ///For changing regional settings

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "dd/MM/yyyy");
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International", "sLongDate", "dd/MM/yyyy");
                ///

                string path = Path.GetDirectoryName(Application.ExecutablePath);
                if (File.Exists(path + "/EDMSLIC.ini") && (File.Exists(path + "/prKey.snk")))
                {
                    string lic = Utils.Crypto.Decrypt((path + "/prKey.snk"), (path + "/EDMSLIC.ini"));
                    lic = lic.Substring(lic.Length - 16, 16);
                    if (lic != string.Empty)
                    {
                        string stDateTime = lic.Substring(0, 8);
                        string endDateTime = lic.Substring(8, 8);
                        string currDt = string.Empty;

                        yr = stDateTime.Substring(0, 4);
                        mn = stDateTime.Substring(4, 2);
                        dd = stDateTime.Substring(6, 2);
                        stDateTime = dd + "/" + mn + "/" + yr;

                        yr = endDateTime.Substring(0, 4);
                        mn = endDateTime.Substring(4, 2);
                        dd = endDateTime.Substring(6, 2);
                        endDateTime = dd + "/" + mn + "/" + yr;

                        IFormatProvider culture = new CultureInfo("fr-Fr", true);
                        DateTime stDt = DateTime.ParseExact(stDateTime, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);

                        DateTime endDt = DateTime.ParseExact(endDateTime, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);

                        dbcon = new NovaNet.Utils.dbCon();
                        sqlCon = dbcon.Connect();


                        DateTime curDate = DateTime.ParseExact(dbcon.GetCurrenctDTTM(2, sqlCon), "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);

                        if ((stDt <= curDate) && (endDt >= curDate))
                        {

                            Application.Run(new frmMain(sqlCon));
                        }
                        else
                        {
                            MessageBox.Show("License has been expired. Contact with nevaeh Technology");
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid license. Contact with nevaeh Technology");
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid license. Contact with nevaeh Technology");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while doing the operation...." + ex.Message);
            }
        }

    }
}
