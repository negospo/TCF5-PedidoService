using API.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("Cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly Application.Interfaces.UseCases.IClienteUseCase _clienteUseCase;

        public ClienteController(Application.Interfaces.UseCases.IClienteUseCase clienteUseCase)
        {
            this._clienteUseCase = clienteUseCase;
        }


        /// <summary>
        /// Anonimiza os dados do cliente da base de dados
        /// </summary>
        [HttpDelete]
        [Route("RemoveAccount")]
        [ProducesResponseType(typeof(bool), 200)]
        [Authorize]
        /// <response code="404" >Cliente não encontrado</response>
        public ActionResult<bool> RemoveAccount()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")))
                { 
                    int id = Convert.ToInt32(this.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
                    return Ok(this._clienteUseCase.Delete(id));
                }
               
                return NotFound("Cliente não encontrado");
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        /// <summary>
        /// Exclui um cliente pelo seu id
        /// </summary>
        /// <response code="404" >Cliente não encontrado</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(this._clienteUseCase.Delete(id));
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        /// <summary>
        /// Retorna um cliente pelo seu id
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <response code="404" >Cliente não encontrado</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Application.DTOs.Output.Cliente), 200)]
        public ActionResult<Application.DTOs.Output.Cliente> Get(int id)
        {
            try
            {
                return Ok(this._clienteUseCase.Get(id));
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retorna um cliente pelo seu cpf
        /// </summary>
        /// <param name="cpf">Cpf do cliente</param>
        /// <response code="404" >Pedido não encontrado</response>
        [HttpGet]
        [Route("getbycpf")]
        [ProducesResponseType(typeof(Application.DTOs.Output.Cliente), 200)]
        public ActionResult<Application.DTOs.Output.Cliente> GetByCpf(string cpf)
        {
            try
            {
                return Ok(this._clienteUseCase.GetByCpf(cpf));
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.Output.Cliente>), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Cliente>> List()
        {
            return Ok(this._clienteUseCase.List());
        }


        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <response code="409" >Email ou CPF ja estão em uso</response>
        [HttpPost]
        [Route("create")]
        [CustonValidateModel]
        [ProducesResponseType(typeof(Validation.CustonValidationResultModel), 400)]
        [ProducesResponseType(typeof(Application.DTOs.Output.Cliente), 200)]
        public ActionResult<Application.DTOs.Output.Cliente> Create(Application.DTOs.Imput.ClienteInsert cliente)
        {
            try
            {
                var result = _clienteUseCase.Insert(cliente);
                return Ok(result);
            }
            catch (Application.CustomExceptions.ConflictException ex) 
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <response code="404" >Cliente não encontrado</response>
        /// <response code="409" >Email ou CPF ja estão em uso</response>
        [HttpPost]
        [Route("update")]
        [CustonValidateModel]
        [ProducesResponseType(typeof(Validation.CustonValidationResultModel), 400)]
        [ProducesResponseType(typeof(Application.DTOs.Output.Cliente), 200)]
        public ActionResult<bool> Update(Application.DTOs.Imput.ClienteUpdate cliente)
        {
            try
            {
                var result = _clienteUseCase.Update(cliente);
                return Ok(result);
            }
            catch (Application.CustomExceptions.ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
