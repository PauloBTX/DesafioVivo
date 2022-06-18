using Domain.DomainModel;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Data.Repositories;

namespace Tests.Domain.Services
{
    public class ClienteServiceTest
    {
        [Fact]
        public async void Valida_CPF_Cliente_Cadastro()
        {
            Cliente cliente = new Cliente();
            cliente.NomeCompleto = "Paulo";
            cliente.DataNascimento = DateTime.Now.AddYears(-30);
            cliente.DocRgNumero = "1234567";
            cliente.DocRgUF = "DF";
            cliente.DocRgOrgaoEmissor = "SSP";
            cliente.DocCPF = "123x";
            cliente.Endereco = "Endereço Completo";
            cliente.Ativo = 1;
            cliente.DataCadastro = DateTime.Now;

            IClienteRepository clienteRepository = new ClienteRepository();
            IClienteService clienteService = new ClienteService(clienteRepository);
            ResponseModel cadastroCliente = await clienteService.Add(cliente);

            Assert.True(cadastroCliente.Sucesso.Equals(false));
        }

        [Fact]
        public async void Valida_RG_Cliente_Cadastro()
        {
            Cliente cliente = new Cliente();
            cliente.NomeCompleto = "Paulo";
            cliente.DataNascimento = DateTime.Now.AddYears(-30);
            cliente.DocRgNumero = "123x4567";
            cliente.DocRgUF = "DF";
            cliente.DocRgOrgaoEmissor = "SSP";
            cliente.DocCPF = "12312312387";
            cliente.Endereco = "Endereço Completo";
            cliente.Ativo = 1;
            cliente.DataCadastro = DateTime.Now;

            IClienteRepository clienteRepository = new ClienteRepository();
            IClienteService clienteService = new ClienteService(clienteRepository);
            ResponseModel cadastroCliente = await clienteService.Add(cliente);

            Assert.True(cadastroCliente.Sucesso.Equals(false));
        }
    }
}