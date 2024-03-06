namespace ML
{
    public class Tarea
    {
        public int IdTarea { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public bool Estado { get; set; }

        public List<ML.Tarea> Tareas { get; set; }
    }
}
