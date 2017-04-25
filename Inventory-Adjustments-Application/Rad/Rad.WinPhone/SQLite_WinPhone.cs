using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rad.WinPhone;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinPhone))]

namespace Rad.WinPhone
{
    public class SQLite_WinPhone : ISQLite 
    {
        public SQLite_WinPhone()
        {
        }

        #region ISQLite implementation
        
        //public SQLite.SQLiteConnection GetConnection()
        //{
        //    var fileName = "Data.db3";
        //    var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

            
        //    var connection = new SQLite.SQLiteConnection(path);

        //    return connection;
        //}

        #endregion
    }
}
