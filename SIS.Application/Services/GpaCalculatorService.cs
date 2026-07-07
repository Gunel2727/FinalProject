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

            if (list.Count == 0) return 0.0;

            var points = list.Select(s => GetGpaPoint(s));
            return Math.Round(points.Average(), 2);
        }

        public string GetLetterGrade(double score)
        {
            switch (score)
            {
                case >= 90:
                    return "A";

                case >= 80:
                    return "B";

                case >= 70:
                    return "C";

                case >= 60:
                    return "D";

                default:
                    return "F";
            }
        }

        public static double GetGpaPoint(double score)
        {
            switch (score)
            {
                case >= 90:
                    return 4.0;

                case >= 80:
                    return 3.0;

                case >= 70:
                    return 2.0;

                case >= 60:
                    return 1.0;

                default:
                    return 0.0;
            }
        }


    }
}
