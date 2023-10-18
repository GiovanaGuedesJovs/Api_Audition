

using RickAndMortyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseStore.Data.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<Logs>
    {
        public void Configure(EntityTypeBuilder<Logs> builder){

            builder.HasKey(p => p.Id);
            builder.Property(c => c.DateNow).HasColumnType("DATETIME");
            builder.Property(c => c.FilePath).HasColumnType("varchar(max)");

            builder.ToTable("Log");
        }
    }
}