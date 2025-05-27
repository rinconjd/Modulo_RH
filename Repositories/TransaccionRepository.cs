using RecursosHumanosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursosHumanosAPI.Repositories
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly AppDbContext _context;

        public TransaccionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Transaccion> GetAll()
        {
            return _context.Set<Transaccion>().ToList();
        }

        public Transaccion? GetById(Guid id)
        {
            return _context.Set<Transaccion>().FirstOrDefault(t => t.Id == id);
        }

        public void Create(Transaccion transaccion)
        {
            _context.Set<Transaccion>().Add(transaccion);
            _context.SaveChanges();
        }

        public void Update(Transaccion transaccion)
        {
            _context.Set<Transaccion>().Update(transaccion);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var transaccion = _context.Set<Transaccion>().Find(id);
            if (transaccion != null)
            {
                _context.Set<Transaccion>().Remove(transaccion);
                _context.SaveChanges();
            }
        }
    }
}