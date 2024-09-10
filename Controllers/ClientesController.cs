using ClientesTrinity.DTOs;
using ClientesTrinity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ClientesTrinity.Models.Clientes;

namespace ClientesTrinity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _clienteService.GetAllAsync());
        }

        [HttpGet]
        [Route("GetId{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound(); // Retorna 404 se o cliente não for encontrado
            }
            return Ok(cliente);
        }

        //método GET para CPF ou CNPJ.
        [HttpGet]
        [Route("GetCpfCnpj{cpfCnpj}")]
        public async Task<ActionResult<ClienteDTO>> GetClienteByCpfCnpj(string cpfCnpj)
        {
            var cliente = await _clienteService.GetByCpfCnpjAsync(cpfCnpj);

            if (cliente == null)
            {
                return NotFound(); // Retorna 404 se o cliente não for encontrado
            }

            var clienteDto = new ClienteDTO
            {
                RazaoSocial = cliente.RazaoSocial,
                NomeFantasia = cliente.NomeFantasia,
                CpfCnpj = cliente.CpfCnpj
            };

            return Ok(clienteDto); // Retorna o cliente encontrado
        }


        [HttpPost]
        public async Task<ActionResult> CreateCliente(ClienteDTO clienteDto)
        {
            //verifica se está faltando dados
            if (clienteDto.NomeFantasia == null || clienteDto.NomeFantasia == "" || clienteDto.CpfCnpj == null || clienteDto.CpfCnpj == "string" || clienteDto.CpfCnpj == "" || clienteDto.RazaoSocial == null || clienteDto.RazaoSocial == "")
            {
                return BadRequest("Falta dados, verifique se todos os dados foi inseridos!");
            }
            var cliente = new Cliente
            {
                RazaoSocial = clienteDto.RazaoSocial,
                NomeFantasia = clienteDto.NomeFantasia,
                CpfCnpj = clienteDto.CpfCnpj
            };
            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente); // Retorna o cliente encontrado
        }

        [HttpPut]
        [Route("PutId{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteDTO clienteDto)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            //verifica se esta faltando dados para não haver inserções indevidas!
            else if (clienteDto.RazaoSocial == null || clienteDto.RazaoSocial == "" || clienteDto.RazaoSocial == "string")
            {
                return BadRequest("Não foi inserido a razão social, todos os dados são obrigatórios");
            }
            else if (clienteDto.CpfCnpj == null || clienteDto.CpfCnpj == "" || clienteDto.CpfCnpj == "string")
            {
                return BadRequest("Não foi inserido a CPF ou CNPJ, todos os dados são  obrigatórios");
            }
            else if (clienteDto.NomeFantasia == null || clienteDto.NomeFantasia == "" || clienteDto.NomeFantasia == "string")
            {
                return BadRequest("Não foi inserido o nome fantasia, todos os dados são  obrigatórios");
            }

            else
            cliente.RazaoSocial = clienteDto.RazaoSocial;
            cliente.NomeFantasia = clienteDto.NomeFantasia;
            cliente.CpfCnpj = clienteDto.CpfCnpj;

            await _clienteService.UpdateAsync(cliente);
            return NoContent();
        }

        //método de UPDATE pelo CPF ou CNPJ
        [HttpPut]
        [Route("Put{cpfCnpj}")]
        public async Task<IActionResult> UpdateCliente(string cpfCnpj, ClienteDTO clienteDto)
        {
            var cliente = await _clienteService.GetByCpfCnpjAsync(cpfCnpj);
            if (cliente == null)
            {
                return NotFound(); // Retorna 404 se o cliente não for encontrado
            }
            
            else if (clienteDto.RazaoSocial == null || clienteDto.RazaoSocial == "" || clienteDto.RazaoSocial == "string") 
            {
                return BadRequest("Não foi inserido a razão social, todos os dados são obrigatórios");
            }
            else if (clienteDto.CpfCnpj == null || clienteDto.CpfCnpj == "" || clienteDto.CpfCnpj == "string")
            {
                return BadRequest("Não foi inserido a CPF ou CNPJ, todos os dados são  obrigatórios");
            }
            else if (clienteDto.NomeFantasia == null || clienteDto.NomeFantasia == "" || clienteDto.NomeFantasia == "string")
            {
                return BadRequest("Não foi inserido o nome fantasia, todos os dados são  obrigatórios");
            }

            else
            cliente.RazaoSocial = clienteDto.RazaoSocial;
            cliente.NomeFantasia = clienteDto.NomeFantasia;
            cliente.CpfCnpj = clienteDto.CpfCnpj;

            await _clienteService.UpdateAsync(cliente); //Atualiza dados.
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteById{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }

        //método de deleção pelo CPF ou CNPJ
        [HttpDelete]
        [Route("DeleteByCpfCnpj{cpfCnpj}")]
        public async Task<IActionResult> DeleteCliente(string cpfCnpj)
        {
            var cliente = await _clienteService.GetByCpfCnpjAsync(cpfCnpj);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.DeleteAsync(cliente.Id);
            return NoContent();
        }
    }
}
