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
        public double Calculate(IList<(double Score, int Credits)> gradesWithCredits)
        {
            if (gradesWithCredits.Count == 0) return 0;

            double totalWeightedPoints = 0;
            int totalCredits = 0;

            foreach (var (score, credits) in gradesWithCredits)
            {
                double gradePoint = ScoreToGradePoint(score); // 90→4.0, 80→3.0 və s.
                totalWeightedPoints += gradePoint * credits;
                totalCredits += credits;
            }

            return totalCredits == 0 ? 0 : Math.Round(totalWeightedPoints / totalCredits, 2);
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

        private double ScoreToGradePoint(double score)
        {
            if (score >= 90) return 4.0;
            if (score >= 80) return 3.0;
            if (score >= 70) return 2.0;
            if (score >= 60) return 1.0;
            return 0.0;
        }


    }
}
