using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionProductosAPI.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock {  get; set; }

    }
}

/*
Models → definen los datos (Producto)

Data → comunica con la base de datos (DbContext)

Services → lógica de negocio, maneja productos

Controllers → exponen la API para que se puedan consumir desde Postman, HTML/JS, etc.
 */