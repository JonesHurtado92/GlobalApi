using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTickets.DataContracts;

namespace WebApiTickets.Settings
{
    public class MongoSettings: IMongoSettings
    {
        public string Server{ get; set; }
        public string Database { get; set; }

    }
}
