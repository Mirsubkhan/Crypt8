using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    class CompareHistoryConfiguration : IEntityTypeConfiguration<CompareHistory>
    {
        public void Configure(EntityTypeBuilder<CompareHistory> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId);

            builder
                .HasOne<Coin>()
                .WithMany()
                .HasForeignKey(u => u.ACoinId);

            builder
                .HasOne<Coin>()
                .WithMany()
                .HasForeignKey(u => u.BCoinId);
        }
    }
}
