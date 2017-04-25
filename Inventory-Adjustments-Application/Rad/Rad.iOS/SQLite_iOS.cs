using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rad.iOS;
using SQLite;
using Xamarin.Forms;


[assembly: Dependency(typeof(SQLite_iOS))]
namespace Rad.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {
        }

        #region ISQLite implementation

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Data.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }

        #endregion
    }
}