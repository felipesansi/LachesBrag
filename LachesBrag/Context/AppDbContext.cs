﻿using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Context
{
    public class AppDbContext : DbContext // colocar :  DbContext para colocar o contex para funcionar
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) // Construtor  do DbContext
        { 
        }
        // Criando Tabelas
      
        public DbSet<Categoria> Categorias { get; set; } // Categoria é os elementos dentro do Tabela Categorias
        public DbSet<Lanches> Lanches { get; set; } // Laches é os elementos dentro do Tabela lanches

    }
}