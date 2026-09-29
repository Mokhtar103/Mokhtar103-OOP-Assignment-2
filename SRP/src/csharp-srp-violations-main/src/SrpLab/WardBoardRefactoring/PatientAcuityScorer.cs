using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WardBoardRefactoring
{
    public class PatientAcuityScorer
    {
        public int Score(int heartRate, int spo2)
        {
            var score = 0;

            if (heartRate > 120 || heartRate < 45)
                score += 4;
            else if (heartRate > 100)
                score += 2;

            if (spo2 < 90)
                score += 5;
            else if (spo2 < 94)
                score += 2;

            return Math.Min(score, 10);
        }
    }
}
