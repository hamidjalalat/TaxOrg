using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace NazmMapping.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken, IQueryable<TDestination>? queryCount = null) where TDestination : class
            => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, queryCount,cancellationToken);
        public static PaginatedList<TDestination> PaginatedListSql<TDestination>(this List<TDestination> list, int pageNumber, int pageSize,int  count) where TDestination : class
           => PaginatedList<TDestination>.Create(list, pageNumber, pageSize, count);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
    }
}