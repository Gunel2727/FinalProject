using SIS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class GpaCalculatorService : IGpaCalculatorService
    {
        public double Calculate(IList<double> scores)
        {
            var list = scores.ToList();

            
            if (!list.Any()) return 0.0;

            
            var points = list.Select(GetGpaPoint);
            return Math.Round(points.Average(), 2);
        }

        public string GetLetterGrade(double score) => score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };

        private static double GetGpaPoint(double score) => score switch
        {
            >= 90 => 4.0,
            >= 80 => 3.0,
            >= 70 => 2.0,
            >= 60 => 1.0,
            _ => 0.0
        };


    }
}
