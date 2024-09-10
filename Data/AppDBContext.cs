﻿using Microsoft.EntityFrameworkCore;
using ST10150702_PROG6212_POE.Models;
namespace ST10150702_PROG6212_POE.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 
        }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}