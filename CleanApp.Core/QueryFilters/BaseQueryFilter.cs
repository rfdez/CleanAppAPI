namespace CleanApp.Core.QueryFilters
{
    public abstract class BaseQueryFilter
    {
        /// <summary>
        /// Tamaño de la paginación
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Número de la página
        /// </summary>
        public int PageNumber { get; set; }
    }
}
