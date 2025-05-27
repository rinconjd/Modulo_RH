using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;
using System;
using System.Collections.Generic;

namespace RecursosHumanosAPI.Services
{
    public class CompraService
    {
        private readonly ICompraRepository _repo;

        public CompraService(ICompraRepository repo)
        {
            _repo = repo;
        }

        public List<Compra> ObtenerTodos() => _repo.GetAll();

        public Compra? ObtenerPorId(Guid id) => _repo.GetById(id);

        public void Crear(Compra compra) => _repo.Create(compra);

        public void Actualizar(Compra compra) => _repo.Update(compra);

        public string ActualizarCompra(Compra compra)
        {
            var existente = _repo.GetById(compra.Id);
            if (existente == null)
                return "Compra no encontrada";

            _repo.Update(compra);
            return "Compra actualizada";
        }

        public void Eliminar(Guid id) => _repo.Delete(id);
    }
}