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
    }

    class Offer
    {
        public Seller Seller { get; set; }

        public double PricePerUnit { get; set; }
        public uint MaxUnits { get => Seller.Storage; }
        



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
        public void Buy(Offer offer, uint units)
        {
            offer.Transact(this, units);
        }
    }
}
