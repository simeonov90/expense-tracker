using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Expense>()
                .HasOne(e => e.User)
                .WithMany(e => e.Expenses)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Income>()
                .HasOne(i => i.User)
                .WithMany(i => i.Incomes)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Expenses)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Incomes)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
