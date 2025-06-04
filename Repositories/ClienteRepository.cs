using RecursosHumanosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanosAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Cliente> GetAll()
        {
            return _context.Clientes
                .Include(c => c.Usuario)
                .ToList();
        }

        public Cliente? GetById(int id)
        {
            return _context.Clientes
                .Include(c => c.Usuario)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }

        public void Detach(Cliente cliente)
        {
            var entry = _context.Entry(cliente);
            if (entry != null)
            {
                entry.State = EntityState.Detached; // Desconecta la entidad del contexto
            }
        }
    }
}