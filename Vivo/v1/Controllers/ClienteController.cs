using Domain.DomainModel;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Vivo.Validators.V1;

namespace Vivo.Controllers.V1
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class ClienteAPI : ControllerBase
    {

        private readonly ILogger<ClienteAPI> _logger;
        private readonly IClienteService _service;
        private ClienteValidator validator;

        public ClienteAPI(ILogger<ClienteAPI> logger, IClienteService service)
        {
            _logger = logger;
            _service = service;
            validator = new ClienteValidator();
        }

        [HttpGet("[action]/{nome}"), MapToApiVersion("1.0")]
        public IActionResult GetByNome(string nome)
        {
            _logger.LogInformation("GetByNome/" + nome);
            IEnumerable <Cliente> clientes = _service.GetByNome(nome);
            if (clientes == null) return Ok("Nenhum cliente encontrado");
            else return Ok(clientes);
        }

        [HttpGet("[action]/{id}"), MapToApiVersion("1.0")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("GetById/" + id);
            Cliente cliente = _service.GetById(id);
            if (cliente == null) return Ok("Nenhum cliente encontrado");
            else return Ok(cliente);
        }

        [HttpGet("[action]"), MapToApiVersion("1.0")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll");
            IEnumerable<Cliente> clientes = _service.GetAll();
            if (clientes == null) return Ok("Nenhum cliente encontrado");
            else return Ok(clientes);
        }

        [HttpPost("[action]"), MapToApiVersion("1.0")]
        public async Task<IActionResult> AtualizarCliente([FromBody] Cliente cliente)
        {
            _logger.LogInformation("UpdateCliente: " + cliente);
            if (cliente == null) return BadRequest(ModelState);
            var validRes = validator.Validate(cliente);
            if (!validRes.IsValid)
            {
                return BadRequest(validRes.Errors.FirstOrDefault());
            }
            ResponseModel apiResponseViewModel = new ResponseModel();
            
            try
            {
                Cliente clienteAtualizar = null;
                if (cliente.ClienteID != 0)
                    clienteAtualizar = _service.GetById(cliente.ClienteID);

                if (clienteAtualizar == null)
                {
                    return await InserirCliente(cliente);

                } else {
                    clienteAtualizar.NomeCompleto = cliente.NomeCompleto;
                    clienteAtualizar.DataNascimento = cliente.DataNascimento;
                    clienteAtualizar.DocRgNumero = cliente.DocRgNumero;
                    clienteAtualizar.DocRgOrgaoEmissor = cliente.DocRgOrgaoEmissor;
                    clienteAtualizar.DocRgUF = cliente.DocRgUF;
                    clienteAtualizar.DocCPF = cliente.DocCPF;
                    clienteAtualizar.Endereco = cliente.Endereco;
                    clienteAtualizar.Ativo = cliente.Ativo;

                    _service.Update(cliente);
                    await _service.SaveChangesAsync();
                    apiResponseViewModel.Sucesso = true;
                    apiResponseViewModel.Mensagem = "Cliente atualizado com sucesso.";
                    return Ok(apiResponseViewModel);
                }

                
            }
            catch(Exception ex) {
                apiResponseViewModel.Sucesso = false;
                apiResponseViewModel.Mensagem = ex.ToString();
                return BadRequest(apiResponseViewModel);
            }

        }

        [HttpPost("[action]"), MapToApiVersion("1.0")]
        public async Task<IActionResult> InserirCliente([FromBody] Cliente cliente)
        {
            _logger.LogInformation("InserirCliente: " + cliente);
            ResponseModel apiResponseViewModel = new ResponseModel();
            if (cliente == null) return BadRequest(ModelState);
            var validRes = validator.Validate(cliente);
            if (!validRes.IsValid)
            {
                return BadRequest(validRes.Errors.FirstOrDefault());
            }
            try
            {
                apiResponseViewModel = await _service.Add(cliente);
                if (apiResponseViewModel.Sucesso)
                {
                    await _service.SaveChangesAsync();
                    return Ok(apiResponseViewModel);
                }
                else
                {
                    return BadRequest(apiResponseViewModel);
                }

            }
            catch (Exception ex)
            {
                apiResponseViewModel.Sucesso = false;
                apiResponseViewModel.Mensagem = ex.ToString();
                return BadRequest(apiResponseViewModel);
            }
        }

        [HttpGet("[action]"), MapToApiVersion("1.0")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            _logger.LogInformation("DeleteById/" + id);
            ResponseModel apiResponseViewModel = new ResponseModel();
            try {
                Cliente cliente = new Cliente();
                cliente = _service.GetById(id);
                if (cliente == null)
                {
                    apiResponseViewModel.Sucesso = false;
                    apiResponseViewModel.Mensagem = "Cliente não encontrado";
                    return BadRequest(apiResponseViewModel);
                } else
                {
                    _service.Remove(cliente);
                    await _service.SaveChangesAsync();
                    apiResponseViewModel.Sucesso = true;
                    apiResponseViewModel.Mensagem = "Cliente removido com sucesso";
                    return Ok(apiResponseViewModel);
                }
            }

            catch(Exception ex)
            {
                apiResponseViewModel.Sucesso = false;
                apiResponseViewModel.Mensagem = ex.ToString();
                return BadRequest(apiResponseViewModel);
            }
            
        }
    }
}