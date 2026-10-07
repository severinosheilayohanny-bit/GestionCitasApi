namespace GestionCitasApi.DTOs
{
    public class CrearCitaDto
    {
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int PacienteId { get; set; }
    }
}