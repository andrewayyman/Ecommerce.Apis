using Ecommerce.Apis.DTOs;

namespace Ecommerce.Apis.Helpers
{
    // this class to be standard response for any endpoint work with pagination
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination( int pageIndex, int pageSize, IReadOnlyList<T> data, int count )
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }
    }
}