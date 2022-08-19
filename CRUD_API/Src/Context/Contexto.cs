using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_API.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Src.Context
{
    /// <summary>
    /// <para> Class contexto, responsável por carregar contexto e definir DbSets</para>
    /// </summary>
    public class CrudContexto : DbContext
    {
        #region Atributos
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        #region Construtor
        public CrudContexto(DbContextOptions<CrudContexto> opt) : base(opt){
        }
        #endregion

    }
}