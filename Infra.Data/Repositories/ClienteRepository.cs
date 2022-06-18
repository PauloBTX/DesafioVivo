using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        //public ClienteRepository(VivoContext context) : base(context) { }

        public Cliente GetByCPF(string cpf)
        {
            return Db.Set<Cliente>().Where(p => p.DocCPF == cpf).SingleOrDefault();
        }

        public IEnumerable<Cliente> GetByNome(string nome)
        {
            return Db.Set<Cliente>().Where(p => p.NomeCompleto.Contains(nome)).Take(500);
        }
    }
}
