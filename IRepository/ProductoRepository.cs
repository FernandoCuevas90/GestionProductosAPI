using GestionProductosAPI.Data; 
using GestionProductosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProductosAPI.IRepository
{
    public class ProductoRepository : IRepository<Producto>
    {
        private readonly GestionProductosContext _context;

        public ProductoRepository(GestionProductosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> Get()

            => await _context.Productos.ToListAsync();

        public async Task<Producto> GetByID(int id)

            => await _context.Productos.FindAsync(id);

        public async Task Add(Producto producto)

            => await _context.Productos.AddAsync(producto);

        public void Update(Producto producto)
        {
            _context.Productos.Attach(producto);
            _context.Productos.Entry(producto).State = EntityState.Modified;
        }

        public void Delete(Producto producto)

            => _context.Productos.Remove(producto);

        public async Task save()

            => await _context.SaveChangesAsync();

        public IEnumerable<Producto> Search(Func<Producto, bool> filter) =>
            _context.Productos.Where(filter).ToList();
       //Búsqueda en la base de datos
    }
}


    
