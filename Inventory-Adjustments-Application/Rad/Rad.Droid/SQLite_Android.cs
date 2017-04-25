
using System.IO;
using Xamarin.Forms;
using Rad.Droid;



[assembly: Dependency(typeof(SQLite_Android))]
namespace Rad.Droid
{
    
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }

        #region ISQLite implementation

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Data.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);


            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }

        #endregion
    }
}