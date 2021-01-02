namespace CleanApp.Core.DTOs
{
    /// <summary>
    /// Viviendas
    /// </summary>
    public class HomeDto
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        public string HomeAddress { get; set; }

        public string HomeCity { get; set; }

        public string HomeCountry { get; set; }

        public string HomeZipCode { get; set; }
    }
}
