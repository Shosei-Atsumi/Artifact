using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace EXTUBE.DataSet
{
    class DatabaseController
    {
        private readonly string APP_NAME = "EXTUBE";
        private readonly string DATA_FILE = "data.sqlite3";
        private SQLiteConnection con;

        public void Initialize()
        {
            //C:\Users\username\AppData\Roaming\EXTUBE\Data
            string dataFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_NAME);

            //アプリケーションフォルダを作成
            if (System.IO.Directory.Exists(dataFolderPath) != true)
            {
                System.IO.Directory.CreateDirectory(dataFolderPath);
            }

            string dbfile = System.IO.Path.Combine(dataFolderPath, DATA_FILE);

            con = new SQLiteConnection("Data Source=" + dbfile);

            //DBオープン
            con.Open();

            //テーブル作成
            SQLiteCommand cmd = con.CreateCommand();

            //テーブル作成
            cmd.CommandText = "Create Table If Not Exists DownloadMovie ("
                + "movie_id Integer,"
                + "title Text,"
                + "resolution Integer)";
            cmd.ExecuteNonQuery();

            //DBクローズ
            con.Close();
        }
    }
}
