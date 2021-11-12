using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.DataContracts
{
    public interface IMongoSettings
    {
        string Server { get; set; }
        string Database { get; set;}
    }
}
