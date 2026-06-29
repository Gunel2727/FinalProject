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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            
            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(c => c.Code)
                .IsUnique();

            
            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(c => c.AcademicTerm)
                .WithMany(a => a.Courses)
                .HasForeignKey(c => c.AcademicTermId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
