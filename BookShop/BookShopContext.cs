using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookShop
{
    public partial class BookShopContext : DbContext
    {
        public BookShopContext()
        {
        }

        public BookShopContext(DbContextOptions<BookShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avail> Avails { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-KHJ53AF0\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avail>(entity =>
            {
                entity.HasKey(e => new { e.IdShop, e.IdBook })
                    .HasName("PK__Avail__074ED6D4EC4D5F7F");

                entity.ToTable("Avail");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.Avails)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_Book");

                entity.HasOne(d => d.IdShopNavigation)
                    .WithMany(p => p.Avails)
                    .HasForeignKey(d => d.IdShop)
                    .HasConstraintName("FK_Shop");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Director).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
