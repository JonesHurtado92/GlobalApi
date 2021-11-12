using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.Wrappers
{
    public class PageFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PageFilter()
        {
            PageNumber = 1;
            PageSize = 5;
        }
        public PageFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 5 ? 5 : pageSize;
        }
    }
}
