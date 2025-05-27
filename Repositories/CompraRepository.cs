using RecursosHumanosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecursosHumanosAPI.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Compra> GetAll()
        {
            return _context.Set<Compra>().ToList();
        }

        public Compra? GetById(Guid id)
        {
            return _context.Set<Compra>().FirstOrDefault(c => c.Id == id);
        }

        public void Create(Compra compra)
        {
            _context.Set<Compra>().Add(compra);
            _context.SaveChanges();
        }

        public void Update(Compra compra)
        {
            _context.Set<Compra>().Update(compra);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var compra = _context.Set<Compra>().Find(id);
            if (compra != null)
            {
                _context.Set<Compra>().Remove(compra);
                _context.SaveChanges();
            }
        }
    }
}