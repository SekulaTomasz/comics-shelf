using comics_shelf_api.core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Dtos
{
    public class PagedResult<T> where T : class
    {
		public List<T> Results { get; protected set; }
		public int CurrentPage { get; protected set; }
		public int PageSize { get; protected set; }
		public int RowCount { get; protected set; }
		public string SortField { get; protected set; }
		public PagedResult(List<T> results, int currentPage, int pageSize, int rowCount, string sortField)
		{
			Results = results;
			CurrentPage = currentPage;
			PageSize = pageSize;
			RowCount = rowCount;
			SortField = sortField;
		}

		public static PagedResult<T> Create(IQueryable<T> source, int currentPage,
			int pageSize, string sortField, OrderBy sortType)
		{
			var count = source.Count();

			var prop = typeof(T).GetProperty(sortField) ?? typeof(T).GetProperty("CreatedAt");
			List<T> items = null;
			switch (sortType)
			{
				case (OrderBy.Asc):
					items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => prop.GetValue(x, null)).ToList();
					break;
				case (OrderBy.Desc):
					items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(x => prop.GetValue(x, null)).ToList();
					break;
			}

			return new PagedResult<T>(items, currentPage, pageSize, count, prop.Name);
		}
	}
}
