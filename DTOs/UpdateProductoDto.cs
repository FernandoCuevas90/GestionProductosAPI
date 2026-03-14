using System.ComponentModel.DataAnnotations;

namespace GestionProductosAPI.DTOs
{
    public class UpdateProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock {  get; set; }

    }
}
