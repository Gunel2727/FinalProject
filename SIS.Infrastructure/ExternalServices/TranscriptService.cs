using QuestPDF.Fluent;
using QuestPDF.Helpers;
using SIS.Application.Common;
using SIS.Application.Interfaces;
using SIS.Application.Services;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.ExternalServices
{
    public class TranscriptService : ITranscriptService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGpaCalculatorService _gpaCalculator;

        public TranscriptService(IUnitOfWork uow, IGpaCalculatorService gpaCalculator)
        {
            _uow = uow;
            _gpaCalculator = gpaCalculator;
        }
        public async Task<byte[]> GenerateTranscriptAsync(int studentId)
        {
            var student = await _uow.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

            var grades = await _uow.Grades.GetByStudentIdAsync(studentId);
            var gradesList = grades.ToList();

            var gradesWithCredits = grades.Select(g => (g.Score, g.Course.Credits)).ToList();
            var gpa = _gpaCalculator.Calculate(gradesWithCredits);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                   
                    page.Header().Background(Colors.Blue.Darken2).Padding(20).Column(column =>
                    {
                        column.Item().Text("Student Information System")
                            .FontSize(20).Bold().FontColor(Colors.White);
                        column.Item().PaddingTop(3).Text("Akademik Transkript")
                            .FontSize(13).FontColor(Colors.Grey.Lighten3);
                    });

                    
                    page.Content().PaddingVertical(20).Column(column =>
                    {
                        
                        column.Item().Background(Colors.Grey.Lighten4)
                            .Padding(15).Column(info =>
                            {
                                info.Item().Row(row =>
                                {
                                    row.RelativeItem().Text(t =>
                                    {
                                        t.Span("Ad Soyad: ").SemiBold();
                                        t.Span($"{student.FirstName} {student.LastName}");
                                    });
                                    row.RelativeItem().Text(t =>
                                    {
                                        t.Span("Kurs İli: ").SemiBold();
                                        t.Span(student.AcademicYear.ToString());
                                    });
                                });

                                info.Item().PaddingTop(5).Row(row =>
                                {
                                    row.RelativeItem().Text(t =>
                                    {
                                        t.Span("Proqram: ").SemiBold();
                                        t.Span(student.Programme?.Name ?? "-");
                                    });
                                    row.RelativeItem().Text(t =>
                                    {
                                        t.Span("GPA: ").SemiBold();
                                        t.Span($"{gpa:F2} / 4.0").FontColor(Colors.Blue.Darken2).Bold();
                                    });
                                });
                            });

                        column.Item().PaddingTop(20);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Background(Colors.Blue.Lighten4).Padding(12).Column(c =>
                            {
                                c.Item().Text("Toplam Kredit").FontSize(9).FontColor(Colors.Grey.Darken1);
                                c.Item().Text(gradesList.Count.ToString()).FontSize(16).Bold();
                            });
                            row.Spacing(10);
                            row.RelativeItem().Background(Colors.Green.Lighten4).Padding(12).Column(c =>
                            {
                                c.Item().Text("Tamamlanmış Kurslar").FontSize(9).FontColor(Colors.Grey.Darken1);
                                c.Item().Text(gradesList.Count.ToString()).FontSize(16).Bold();
                            });
                            row.RelativeItem().Background(Colors.Orange.Lighten4).Padding(12).Column(c =>
                            {
                                c.Item().Text("Akademik Status").FontSize(9).FontColor(Colors.Grey.Darken1);
                                c.Item().Text(gpa >= 2.0 ? "Aktiv" : "Nəzarətdə").FontSize(16).Bold();
                            });
                        });

                        column.Item().PaddingTop(20);


                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Darken2).Padding(8)
                                    .Text("Kurs").FontColor(Colors.White).Bold();
                                header.Cell().Background(Colors.Blue.Darken2).Padding(8)
                                    .Text("Bal").FontColor(Colors.White).Bold();
                                header.Cell().Background(Colors.Blue.Darken2).Padding(8)
                                    .Text("Qiymət").FontColor(Colors.White).Bold();
                            });

                           
                            for (int i = 0; i < gradesList.Count; i++)
                            {
                                var grade = gradesList[i];
                                var bgColor = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;

                                table.Cell().Background(bgColor).Padding(8)
                                    .Text(grade.Course?.Name ?? "-");
                                table.Cell().Background(bgColor).Padding(8)
                                    .Text(grade.Score.ToString("F1"));
                                table.Cell().Background(bgColor).Padding(8)
                                    .Text(grade.Letter.ToString()).Bold();
                            }

                           
                            if (gradesList.Count == 0)
                            {
                                table.Cell().ColumnSpan(3).Padding(15).AlignCenter()
                                    .Text("Hələ qiymət qeydə alınmayıb")
                                    .FontColor(Colors.Grey.Darken1).Italic();
                            }
                        });
                    });

                    page.Footer().PaddingTop(15).Column(column =>
                    {
                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        column.Item().PaddingTop(5).AlignCenter().Text(x =>
                        {
                            x.Span("Yaradılma tarixi: ").FontColor(Colors.Grey.Darken1);
                            x.Span(DateTime.Now.ToString("dd.MM.yyyy")).FontColor(Colors.Grey.Darken1);
                        });
                    });
                });
            });
            return document.GeneratePdf();

        }


        
    }
                



                   
}
