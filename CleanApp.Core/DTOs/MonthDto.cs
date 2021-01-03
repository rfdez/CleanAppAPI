namespace CleanApp.Core.DTOs
{
    public class MonthDto
    {
        /// <summary>
        /// Id del mes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Valor numérico del mes
        /// </summary>
        public int MonthValue { get; set; }
        /// <summary>
        /// Id del año al que pertenece el mes
        /// </summary>
        public int YearId { get; set; }
    }
}
