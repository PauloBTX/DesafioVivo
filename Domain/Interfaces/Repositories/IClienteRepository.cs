using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Cliente GetByCPF(string cpf);
        IEnumerable<Cliente> GetByNome(string nome);
    }
}
