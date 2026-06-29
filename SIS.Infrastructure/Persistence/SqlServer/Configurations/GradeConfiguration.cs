using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Configurations
{
    public class GradeConfiguration:IEntityTypeConfiguration<Grade>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);

            
            builder.HasIndex(g => new { g.StudentId, g.CourseId })
                .IsUnique();

            builder.Property(g => g.Score)
                .IsRequired();

            
            builder.Property(g => g.Letter)
                .HasConversion<string>();

            builder.HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Course)
                .WithMany()
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    {
    }
}
