using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_API.Src.Models;
using CRUD_API.Src.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Src.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributo
        private readonly IUsuario _repositorio;
        #endregion

        #region Construtor
        public UsuarioControlador(IUsuario repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Método
        /// <summary>
        /// Pegar todos os usuários
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Lista de postagens </response>
        /// <response code="204"> Lista vazia </response>
        [HttpGet]
        public async Task<ActionResult> ListarTodosUsuarios()
        {
            var lista = await _repositorio.ListarTodosUsuarios();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Retorna o usuário </response>
        /// <response code="404"> Usuário não existente </response>
        [HttpGet("id/{id}")]
        public async Task<ActionResult> PegarUsuarioPorId([FromRoute] int id)
        {
            try
            {
                return Ok(await _repositorio.PegarUsuarioPorId(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        /// <param name="usuario">Contrutor para criar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/cadastrar 
        ///     { 
        ///         "nome": "Nome Do Usuario", 
        ///         "DataNascimento": "2022-08-19T11:07:37.470Z", 
        ///         "Curso": "Nome do Curso", 
        ///         "EstadoCivil": "SOLTEIRO"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response>
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            await _repositorio.CriarUsuario(usuario);
            return Created($"api/Usuarios", usuario);
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary> 
        /// <param name="usuario">Contrutor para atualizar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Usuarios/cadastrar 
        ///     { 
        ///         "id": 0,
        ///         "nome": "Nome Do Usuario", 
        ///         "DataNascimento": "2022-08-19T11:07:37.470Z", 
        ///         "Curso": "Nome do Curso", 
        ///         "EstadoCivil": "SOLTEIRO"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Retorna usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                await _repositorio.AtualizarUsuario(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuário deletado</response>
        /// <response code="404">Id do usuário não existe</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarUsuario([FromRoute] int id)
        {
            try
            {
                await _repositorio.DeletarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion

    }
}