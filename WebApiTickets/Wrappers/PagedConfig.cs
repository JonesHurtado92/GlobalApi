using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTickets.Wrappers
{
    public class PagedConfig<T>: Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PagedConfig(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}
