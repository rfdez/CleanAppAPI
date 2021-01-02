using CleanApp.Core.Enumerations;

namespace CleanApp.Core.DTOs
{
    /// <summary>
    /// Autenticación del usuario en el sistema
    /// </summary>
    public class AuthenticationDto
    {
        /// <summary>
        /// Identificador de la autenticación
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identidficador único para la autenticación del usuario
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Contraseña del usuaio
        /// </summary>
        public string UserPassword { get; set; }

        public RoleType UserRole { get; set; }
    }
}
