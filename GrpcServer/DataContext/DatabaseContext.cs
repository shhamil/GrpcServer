using System.Collections.Generic;
using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GrpcData> grpc_data { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
