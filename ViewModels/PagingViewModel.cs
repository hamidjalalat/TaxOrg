using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
	public class PagingViewModel<T>
	{
		public List<T> Items { get; }
		public int PageNumber { get; }
		public int PageSize { get; }
		public int TotalPages { get; }
		public int TotalCount { get; }

        public PagingViewModel(List<T> items, int totalCount, int totalPages, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
            PageSize = pageSize;
			TotalPages = totalPages;
			TotalCount = totalCount;
			Items = items;
		}
	}
}
