namespace ProyectoFinalDicsys.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
    }
}
