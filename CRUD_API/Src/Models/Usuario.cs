using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CRUD_API.Src.Models.Enum;

namespace CRUD_API.Src.Models
{
    /// <summary>
    /// <para> Resumo: Classe responsavel por representar tb_usuarios no banco.</para>
    /// </summary>
    [Table("tb_usuarios")]
    public class Usuario
    {
        #region Atributos
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Curso { get; set; }

        public EstadoCivil EstadoCivil { get; set; }
        #endregion

    }
}