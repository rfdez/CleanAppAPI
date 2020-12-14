namespace CleanApp.Core.Enumerations
{
    public enum ErrorCodes
    {
        /// <summary>
        /// Esta respuesta significa que el servidor no pudo interpretar la solicitud dada una sintaxis inválida.
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Es necesario autenticar para obtener la respuesta solicitada. Esta es similar a 403, pero en este caso, la autenticación es posible.
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// El cliente no posee los permisos necesarios para cierto contenido, por lo que el servidor está rechazando otorgar una respuesta apropiada.
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// El servidor no pudo encontrar el contenido solicitado. Este código de respuesta es uno de los más famosos dada su alta ocurrencia en la web.
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Esta respuesta puede ser enviada cuando una petición tiene conflicto con el estado actual del servidor.
        /// </summary>
        Conflict = 409,
        /// <summary>
        /// El servidor ha encontrado una situación que no sabe cómo manejarla.
        /// </summary>
        InternalServerError = 500
    }
}
