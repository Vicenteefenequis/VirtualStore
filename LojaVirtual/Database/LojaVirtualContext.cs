﻿using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Database
{
    public class LojaVirtualContext : DbContext
    {
        
         /*
          * EF CORE - ORM
          * ORM -> Biblioteca mapear Objetos para Banco de Dados Relacionais
          * 
          */
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options): base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        

    }
}
