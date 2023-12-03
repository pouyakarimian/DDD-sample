using MediatR;

namespace Crud.DDD.Core.Common.Pagination
{
    public record PagedResultDto<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int Count { get; private set; }
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;
        public IReadOnlyList<T> Data { get; private set; }
        public PagedResultDto(IReadOnlyList<T> data, int count, int pageIndex, int pageSize)
        {
            Count = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = data;
        }
        public static PagedResultDto<T> ToPagedList(IQueryable<T> source, int totalCount, int pageIndex, int pageSize)
        {
            var count = totalCount;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResultDto<T>(items, count, pageIndex, pageSize);
        }
    }

    public record PagingParamQuery<T> : BaseQuery<T>
    {
        private int _pageSize = 10;
        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (value > 0)
                {
                    _pageIndex = value;
                }
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > 0) ? value : _pageSize;
            }
        }
    }

    public record BaseQuery<T> : IRequest<T>
    {

    }
}
