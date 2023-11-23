using Microsoft.EntityFrameworkCore;

namespace Dez.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) :
            base(options)
        { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Farmaceutico> Farmaceuticos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
