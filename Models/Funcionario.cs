using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }

        public string Foto { get; set; } = string.Empty;

        public Departamento Departamento { get; set; }

        public int DepartamentoId { get; set; }
    }
}
