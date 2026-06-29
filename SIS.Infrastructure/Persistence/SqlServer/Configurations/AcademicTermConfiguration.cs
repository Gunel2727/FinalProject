using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Configurations
{
    public class AcademicTermConfiguration : IEntityTypeConfiguration<AcademicTerm>
    {
        public void Configure(EntityTypeBuilder<AcademicTerm> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.StartDate)
                .IsRequired();

            builder.Property(a => a.EndDate)
                .IsRequired();
        }
    }
}
