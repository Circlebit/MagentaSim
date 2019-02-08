using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Agent
    {
        /// <summary>
        /// Money the Agent can spend
        /// </summary>
        public double Cash { get; set; }

        /// <summary>
        /// Number of items in storage
        /// </summary>
        public uint Storage { get; set; }
    }

    class Seller : Agent
    {
        public Offer Offer { get; set; }

        public Seller(double initCash, uint initStorage)
        {
            Cash = initCash;
            Storage = initStorage;
            Offer = new Offer(this);
        }
    }

    /// <summary>
    /// a number of units a Seller is offering to a price 
    /// </summary>
    class Offer
    {
        public Seller Seller { get; set; }

        public double PricePerUnit { get; set; }
        public uint MaxUnits { get => Seller.Storage; }
        
        public Offer(Seller seller)
        {
            Seller = seller;
            PricePerUnit = double.MaxValue;
        }

        public void Transact(Buyer buyer, uint units)
        {
            double total = PricePerUnit * units;
            if(units <= MaxUnits)
            {
                Seller.Storage -= units;
                buyer.Cash -= total;

                Seller.Cash += total;
                buyer.Storage += units;
            }
        }

    }


    class Buyer : Agent
    {
        public uint MaxStorage { get; set; }
        public uint FreeStorage { get => MaxStorage - Storage; }

        public static uint TotalBuyers { get; private set; }
        public uint BuyerID { get; private set; }

        private Market Market { get; set; }

        public Buyer(Market market, double initCash, uint initStorage, uint maxStorage)
        {
            Market = market;
            Cash = initCash;
            Storage = initStorage;
            MaxStorage = maxStorage;
            TotalBuyers++;
            BuyerID = TotalBuyers;
        }

        internal void Act()
        {
            if (FreeStorage > 0)
            {
                var bestOffer = FindBestOffer();
                uint amount = bestOffer.MaxUnits;
                if (amount > FreeStorage)
                {
                    amount = FreeStorage;
                }
                else if ( bestOffer.MaxUnits < FreeStorage)
                {
                    amount = bestOffer.MaxUnits;
                }
                Buy(bestOffer, amount);
            }
            else
            {
                Console.WriteLine($"Buyer {BuyerID}: Can't buy anything, my storage is full.");
            }
        }

        private Offer FindBestOffer()
        {
            var SellerWithBestOffer = Market.Sellers.Aggregate<Seller>((curMin, x) => x.Offer.PricePerUnit < curMin.Offer.PricePerUnit ? x : curMin);
            return SellerWithBestOffer.Offer;
        }

        public void Buy(Offer offer, uint units)
        {
            offer.Transact(this, units);
        }
    }
}
