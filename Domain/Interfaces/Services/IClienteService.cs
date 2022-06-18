using Domain.DomainModel;
using Domain.Entities;


namespace Domain.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        Cliente GetByCPF(string cpf);
        new Task<ResponseModel> Add(Cliente cliente);

        IEnumerable<Cliente> GetByNome(string nome);
    }
}
