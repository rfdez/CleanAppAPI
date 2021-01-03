namespace CleanApp.Core.QueryFilters
{
    public class HomeQueryFilter : BaseQueryFilter
    {
        /// <summary>
        /// Dirección de la vivienda
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// Ciudad de la vivienda
        /// </summary>
        public string HomeCity { get; set; }

        /// <summary>
        /// País de la vivienda
        /// </summary>
        public string HomeCountry { get; set; }

        /// <summary>
        /// Código postal de la vivienda
        /// </summary>
        public string HomeZipCode { get; set; }
    }
}
