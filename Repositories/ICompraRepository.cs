using RecursosHumanosAPI.Models;
using System;
using System.Collections.Generic;

namespace RecursosHumanosAPI.Repositories
{
    public interface ICompraRepository
    {
        List<Compra> GetAll();
        Compra? GetById(Guid id);
        void Create(Compra compra);
        void Update(Compra compra);
        void Delete(Guid id);
    }
}