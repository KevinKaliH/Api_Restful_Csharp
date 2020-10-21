using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMediaCore.Entidades.CustomEntities
{
    public class PageList<T> : List<T>
    {
        public int CurrrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrrentPage > 1;
        public bool HasNextPage => CurrrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrrentPage - 1 : (int?)null; 


        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);    //propiedad de List<T> heredada
        }

        public static PageList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
