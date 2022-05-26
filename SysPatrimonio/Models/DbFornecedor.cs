using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysPatrimonio.Models
{
    [Table("fornecedor", Schema = "public")]

    public class DbFornecedor
    {
        [Key]
        public int id { get; set; }
        public string nomefornecedor { get; set; }
        public string endereco { get; set; }
        public string fone { get; set; }
    }
}
