using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOAP.Models;
using ServicoCorreios;

namespace SOAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorreiosController : ControllerBase
    {
        [HttpGet("{cep}")]
        public async Task<IActionResult> GetDadosCep(string cep)
        {
            try
            {
                var cliente = new AtendeClienteClient();
                var consulta = await cliente.consultaCEPAsync(cep);

                if ((consulta == null) || (consulta.@return == null))
                    return NotFound("Retorno inexistente ou CEP não encontrado");

                Endereco endereco = new Endereco
                {
                    Descricao = consulta.@return.end,
                    Complemento = consulta.@return.complemento2,
                    Bairro = consulta.@return.bairro,
                    Cidade = consulta.@return.cidade,
                    UF = consulta.@return.uf
                };

                return Ok(endereco);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
