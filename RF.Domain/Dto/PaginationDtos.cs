using System.Collections.Generic;

namespace RF.Domain.Dto
{
    public class PagingOptionsDto
    {
        /// <summary>
        /// The current index of the page. Default is set to 0
        /// </summary>
        public int CurrentIndex { get; set; } = 0;

        /// <summary>
        /// The number of items per page. Default is set to 10
        /// </summary>
        public int HowManyPerPage { get; set; } = 10;

        /// <summary>
        /// The property we want to order the results by. Default is set to Id
        /// </summary>
        public string PropertyToOrderBy { get; set; } = "Id";
    }

    public class PagedSetDto<T>
    {
        /// <summary>
        /// The results of the query
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// The total number of items available
        /// </summary>
        public int Total { get; set; }
    }
}