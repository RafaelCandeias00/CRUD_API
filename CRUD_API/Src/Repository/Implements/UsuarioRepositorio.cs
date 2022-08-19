using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRUD_API.Src.Context;
using CRUD_API.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Src.Repository.Implements
{
    /// <summary>
    /// <para> Resumo: Classe responsável por implementar IUsuario</para>
    /// </summary>
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos
        private readonly CrudContexto _contexto;
        #endregion

        #region Construtor
        public UsuarioRepositorio(CrudContexto contexto){
            _contexto = contexto;
        }
        #endregion

        #region Método
        /// <summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// </summary>
        public async Task<List<Usuario>> ListarTodosUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para pegar um usuario pelo id</para>
        /// </summary>
        public async Task<Usuario> PegarUsuarioPorId(int id)
        {
            if(!ExisteId(id)) throw new Exception("Id do usuário não foi encontrado!");

            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            bool ExisteId(int id)
            {
                var aux = _contexto.Usuarios.FirstAsync(u => u.Id == id);
                return aux != null;
            }
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para criar usuario</para>
        /// </summary>
        public async Task CriarUsuario(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(
                new Usuario
                {
                    Nome = usuario.Nome,
                    DataNascimento = usuario.DataNascimento,
                    Curso = usuario.Curso,
                    EstadoCivil = usuario.EstadoCivil
                });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para atualizar usuario</para>
        /// </summary>
        public async Task AtualizarUsuario(Usuario usuario)
        {
            var aux = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            aux.Nome = usuario.Nome;
            aux.DataNascimento = usuario.DataNascimento;
            aux.Curso = usuario.Curso;
            aux.EstadoCivil = usuario.EstadoCivil;

            _contexto.Usuarios.Update(aux);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para> Resumo: Método assincrono para deletar usuario</para>
        /// </summary>
        public async Task DeletarUsuario(int id)
        {
            var aux = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            _contexto.Usuarios.Remove(aux);
            await _contexto.SaveChangesAsync();
        }
        #endregion
    }
}