using RecursosHumanosAPI.Models;

namespace RecursosHumanosAPI.Repositories
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
        Cliente? GetById(int id);
        void Create(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);

        void Detach(Cliente cliente);
    }
}