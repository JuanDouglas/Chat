﻿using Chat.Server.Dal.SqlServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Server.Dal.SqlServer
{
    public partial class SqlServerContext : DbContext
    {
        public string ConnectionString { get; set; }
        public SqlServerContext()
        {
            ConnectionString ??= "Server=localhost\\MSSQLSERVER01;Database=ChatServer;Trusted_Connection=True;";
        }
        public SqlServerContext(string connectionString) : base()
        {
            ConnectionString = connectionString;
        }
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MessageContent> MessagesContent { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
