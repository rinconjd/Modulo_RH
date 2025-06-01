using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;
using System;
using System.Collections.Generic;

namespace RecursosHumanosAPI.Services
{
    public class TransaccionService
    {
        private readonly ITransaccionRepository _repo;

        public TransaccionService(ITransaccionRepository repo)
        {
            _repo = repo;
        }

        public List<Transaccion> ObtenerTodos() => _repo.GetAll();

        public Transaccion? ObtenerPorId(Guid id) => _repo.GetById(id);

        public void Crear(Transaccion transaccion)
        {
            _repo.Create(transaccion);
        }

        public void Actualizar(Transaccion transaccion) => _repo.Update(transaccion);

        public string ActualizarTransaccion(Transaccion transaccion)
        {
            var existente = _repo.GetById(transaccion.Id);
            if (existente == null)
                return "Transacción no encontrada";

            _repo.Update(transaccion);
            return "Transacción actualizada";
        }

        public void Eliminar(Guid id) => _repo.Delete(id);


    }
}