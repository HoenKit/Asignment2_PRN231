using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class eBookStoreContext : DbContext
    {
        public eBookStoreContext(DbContextOptions<eBookStoreContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("MyConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.author_id);
            });
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.pub_id);
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.role_id);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.user_id); 

                entity.Property(u => u.email_address)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.password)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.first_name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.middle_name)
                      .HasMaxLength(50);

                entity.Property(u => u.last_name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.hire_date)
                      .IsRequired()
                      .HasColumnType("datetime");

                entity.HasIndex(u => u.email_address).IsUnique(); 
               
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.role_id)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(u => u.Publisher)
                      .WithMany(p => p.Users)
                      .HasForeignKey(u => u.pub_id)
                      .OnDelete(DeleteBehavior.Restrict); 
            });
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.book_id); 

                entity.Property(b => b.title)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(b => b.type)
                      .HasMaxLength(50);

                entity.Property(b => b.price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(b => b.advance)
                      .HasMaxLength(50);

                entity.Property(b => b.royalty)
                      .HasMaxLength(50);

                entity.Property(b => b.ytd_sales)
                      .HasColumnType("decimal(18,2)");

                entity.Property(b => b.notes)
                      .HasMaxLength(1000);

                entity.Property(b => b.published_date)
                      .IsRequired()
                      .HasColumnType("datetime");

                entity.HasOne(b => b.Publisher)
                      .WithMany(p => p.Books)
                      .HasForeignKey(b => b.pub_id)
                      .OnDelete(DeleteBehavior.Cascade); 
            });
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(ba => new { ba.book_id, ba.author_id });

                entity.HasOne(ba => ba.Book)
                      .WithMany(b => b.BookAuthors)
                      .HasForeignKey(ba => ba.book_id)
                      .OnDelete(DeleteBehavior.Cascade); 

                entity.HasOne(ba => ba.Author)
                      .WithMany(a => a.BookAuthors)
                      .HasForeignKey(ba => ba.author_id)
                      .OnDelete(DeleteBehavior.Cascade); 

                entity.Property(ba => ba.author_order)
                      .HasMaxLength(10);

                entity.Property(ba => ba.royalty_percentage)
                      .HasColumnType("decimal(5,2)");
            });
        }
    }
}
