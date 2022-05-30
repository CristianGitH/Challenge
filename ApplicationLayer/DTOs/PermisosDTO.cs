namespace ApplicationLayer.DTOs
{
    /// <summary>
    /// Permission DTO
    /// </summary>
    public class PermisosDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Employee Name
        /// </summary>
        public string NombreEmpleado { get; set; }
        /// <summary>
        /// Employee Last Name
        /// </summary>
        public string ApellidoEmpleado { get; set; }
        /// <summary>
        /// Permission type
        /// </summary>
        public int TipoPermiso { get; set; }
    }
}
