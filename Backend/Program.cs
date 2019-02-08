using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var myMarket = new Market("myMarket.sqlite");
            myMarket.OpenConnection();
            myMarket.CreateTestTable();

            myMarket.AddSellers(10, 0, 100);
            myMarket.AddBuyers(10, 1000, 0, 200);
        }
    }

    class Market
    {
        /// <summary>
        /// Filename for SQLite database
        /// </summary>
        public string Filename { get; private set; }
        private SQLiteConnection Connection { get; set; }

        public List<Seller> Sellers { get; set; }
        public List<Buyer> Buyers { get; set; }

        /// <summary>
        /// number of time steps that have passed
        /// </summary>
        public uint TimePassed { get; private set; }

        public Market(string filename)
        {
            Filename = filename;
            Connection = new SQLiteConnection(
                $"Data Source = {Filename}; " +
                $"Version = 3;");
            SQLiteConnection.CreateFile(filename);

            Sellers = new List<Seller>();
            Buyers = new List<Buyer>();

            TimePassed = 0;
        }

        public void Step()
        {
            FirstToBack(Sellers);

            FirstToBack(Buyers);
            foreach(Buyer buyer in Buyers)
            {
                buyer.Act();
            }
        }

        public void FirstToBack<T>(List<T> list)
        {
            T item = list[0];
            list.RemoveAt(0);
            list.Add(item);
        }

        public void AddSellers(uint amount, double initCash, uint initStorage)
        {
            for (int i = 0; i < amount; i++)
            {
                Sellers.Add(new Seller(initCash, initStorage));
            }
        }

        public void AddBuyers(uint amount, double initCash, uint initStorage, uint maxStorage)
        {
            for (int i = 0; i < amount; i++)
            {
                Buyers.Add(new Buyer(this, initCash, initStorage, maxStorage));
            }
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
