using Domain.DomainModel;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Cliente GetByCPF(string cpf)
        {
            return _repository.GetByCPF(cpf);
        }

        public IEnumerable<Cliente> GetByNome(string nome)
        {
            return _repository.GetByNome(nome);
        }

        async Task<ResponseModel> IClienteService.Add(Cliente cliente)
        {
            Cliente clienteExistente = new Cliente();
            ResponseModel retorno = new ResponseModel();
            clienteExistente = GetByCPF(cliente.DocCPF);
            if (clienteExistente == null)
            {
                try
                {
                    _repository.Add(cliente);
                    await _repository.SaveChangesAsync();
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Cliente cadastrado com sucesso";
                }
                catch (Exception ex)
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = ex.ToString();
                }
                
            } else
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Cliente já cadastrado na base de dados";
            }

            return retorno;
        }
    }
}
