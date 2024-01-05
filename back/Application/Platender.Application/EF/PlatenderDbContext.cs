﻿using Platender.Application.EF.Configuration;
using Platender.Core.Models;
using Microsoft.EntityFrameworkCore;
using Platender.Application.EF.Settings;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Platender.Application.EF
{
    public class PlatenderDbContext : DbContext
    {
        private readonly SqlConnectionSettings _connectionSettings;
        public DbSet<User> users { get; set; }
        public DbSet<Plate> plates { get; set; }

        public PlatenderDbContext(DbContextOptions<PlatenderDbContext> options, SqlConnectionSettings connectionSettings) : base(options)
        {
            _connectionSettings = connectionSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionSettings.BuildConnectionStrings(), ServerVersion.Create(new Version(10, 4, 32), ServerType.MariaDb), x=> x.MigrationsAssembly("Platender.Infrastructure"));

            //optionsBuilder.UseMySql(_connectionSettings.BuildConnectionStrings(), x => x.MigrationsAssembly("Platender.Infrastructure"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlateConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}