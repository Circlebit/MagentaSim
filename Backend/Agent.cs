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

    }

    class Buyer : Agent
    {

    }
}
