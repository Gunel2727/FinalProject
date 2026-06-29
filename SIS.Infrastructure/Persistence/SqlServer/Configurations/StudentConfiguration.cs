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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            
            builder.HasIndex(s => s.Email)
                .IsUnique();

            builder.Property(s => s.Phone)
                .HasMaxLength(20);

           
            builder.HasOne(s => s.Programme)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ProgrammeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
