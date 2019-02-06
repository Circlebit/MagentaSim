using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var myMarket = new Market("myMarket.sqlite");
            myMarket.OpenConnection();
            myMarket.CreateTestTable();
        }
    }

    class Market
    {
        /// <summary>
        /// Filename for SQLite database
        /// </summary>
        public string Filename { get; private set; }
        private SQLiteConnection Connection { get; set; }


        public Market(string filename)
        {
            Filename = filename;
            Connection = new SQLiteConnection(
                $"Data Source = {Filename}; " +
                $"Version = 3;");
            SQLiteConnection.CreateFile(filename);
        }

        public void OpenConnection()
        {
            Connection.Open();
        }

        public void CreateTestTable()
        {
            string sql = "CREATE TABLE personen(vorname TEXT, nachname TEXT)";
            SQLiteCommand Command = new SQLiteCommand(sql, Connection);
            Command.ExecuteNonQuery();

            string sql2 = "INSERT INTO personen(vorname, nachname) VALUES ('Schorsch', 'Bengelmeier')";
            SQLiteCommand Command2 = new SQLiteCommand(sql2, Connection);
            Command2.ExecuteNonQuery();
        }
    }
}
