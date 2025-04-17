using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Data
{
    public class FuncionarioContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=VIKKI\\victo;Initial Catalog=GerenciadorApp;Data Source=localhost\\SQLEXPRESS");
        }
    }
}
