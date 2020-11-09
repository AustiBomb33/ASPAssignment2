using System;
using System.Collections.Generic;
using System.Text;
using ASPAssignment2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Author> Authors;
        public DbSet<Article> Articles;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //define relationship
            builder.Entity<Article>()
                .HasOne(ar => ar.Author)
                .WithMany(au => au.Articles)
                .HasForeignKey(ar => ar.AuthorId)
                .HasConstraintName("FK_Article_Author");

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ASPAssignment2.Models.Article> Article { get; set; }

        public DbSet<ASPAssignment2.Models.Author> Author { get; set; }
    }
}
