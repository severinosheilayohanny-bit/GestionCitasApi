namespace GestionCitasApi.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public List<Cita> Citas { get; set; } = new List<Cita>();
    }
}
