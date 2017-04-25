
using SQLite;


namespace Rad
{

        public interface ISQLite
        {

            SQLiteConnection GetConnection();
        }
}
