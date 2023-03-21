using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDataAccess.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasIndex(x => x.ISBN).IsUnique();
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(13);
            builder.HasIndex(x => x.Title).IsUnique();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(3000);
            builder.Property(x => x.ReleaseDate).IsRequired();
            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.Stock).IsRequired();

            builder.HasOne(x => x.Genre).WithMany(x => x.Books).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
