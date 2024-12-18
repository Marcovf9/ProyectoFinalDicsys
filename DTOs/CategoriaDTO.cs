namespace ProyectoFinalDicsys.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ProductoDTO> Productos { get; set; }
    }

}
