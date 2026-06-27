using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IGpaCalculatorService
    {
        
        double Calculate(IList<double> scores);
        
        string GetLetterGrade(double score);
    }
}
