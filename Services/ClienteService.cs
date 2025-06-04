using RecursosHumanosAPI.Models;
using RecursosHumanosAPI.Repositories;

namespace RecursosHumanosAPI.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public List<Cliente> ObtenerTodos() => _repo.GetAll();

        public Cliente? ObtenerPorId(int id) => _repo.GetById(id);

        public void Crear(Cliente cliente) => _repo.Create(cliente);

        public void Actualizar(Cliente cliente) => _repo.Update(cliente);

        public string ActualizarCliente(Cliente cliente)
        {
            var cliExistente = _repo.GetById(cliente.Id);
            if (cliExistente == null)
                return "Cliente no encontrado";

            // Desconecta la entidad existente del contexto
            _repo.Detach(cliExistente);

            // Actualiza el cliente
            _repo.Update(cliente);

            return "Cliente actualizado";
        }

        public void Eliminar(int id) => _repo.Delete(id);
    }
}