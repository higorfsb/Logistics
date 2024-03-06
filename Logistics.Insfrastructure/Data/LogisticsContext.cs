using Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Insfrastructure.Data
{
    public partial class LogisticsContext : DbContext
    {
        
        public LogisticsContext(DbContextOptions<LogisticsContext> options)
            : base(options)
        {
        }
        
        public DbSet<Ocorrencia> Ocorrencia { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
