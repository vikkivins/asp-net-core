﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }
}
