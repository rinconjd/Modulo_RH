using RecursosHumanosAPI.Models;
using System;
using System.Collections.Generic;

namespace RecursosHumanosAPI.Repositories
{
    public interface ITransaccionRepository
    {
        List<Transaccion> GetAll();
        Transaccion? GetById(Guid id);
        void Create(Transaccion transaccion);
        void Update(Transaccion transaccion);
        void Delete(Guid id);
    }
}