using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class PagerDTO
    {
        private object p;
        private int? page;
        private int v;

        public PagerDTO(long totalItems, int? page, int pageSize = 30)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            FirstCurrentPageRow = 1 + (currentPage - 1) * pageSize;
        }

        public PagerDTO(object p, int? page, int v)
        {
            this.p = p;
            this.page = page;
            this.v = v;
        }

        public int CurrentPage { get; private set; }
        public int EndPage { get; private set; }
        public int FirstCurrentPageRow { get; private set; }
        public int PageSize { get; private set; }
        public int StartPage { get; private set; }
        public long TotalItems { get; private set; }
        public int TotalPages { get; private set; }
    }
}
