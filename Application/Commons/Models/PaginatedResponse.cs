namespace Application.Commons.Models
{
    /// <summary>
    /// Represents a paginated response for a collection of items.
    /// </summary>
    /// <typeparam name="TEntity">The type of the items in the response.</typeparam>
    public class PaginatedResponse<TEntity>( IReadOnlyCollection<TEntity> items, int count, int pageNumber, int pageSize)
    {
        /// <summary>
        /// Gets the collection of items in the current page.
        /// </summary>
        public IReadOnlyCollection<TEntity> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        private int PageNumber { get; } = pageNumber;

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        private int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

        /// <summary>
        /// Gets the total number of items across all pages.
        /// </summary>
        public int TotalCount { get; } = count;

        /// <summary>
        /// Indicates whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates whether there is a next page.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        // Optional: You can add a property for PageSize if needed
        public int PageSize { get; } = pageSize; 
    }
}