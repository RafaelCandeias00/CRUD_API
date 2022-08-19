using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_API.Src.Models;

namespace CRUD_API.Src.Repository
{
    /// <summary>
    /// <para> Resumo: Responsável por representar ações de CRUD de usuario.</para>
    /// </summary>
    public interface IUsuario
    {
        Task<List<Usuario>> ListarTodosUsuarios();
        Task<Usuario> PegarUsuarioPorId (int id);
        Task CriarUsuario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Task DeletarUsuario(int id);
    }
}