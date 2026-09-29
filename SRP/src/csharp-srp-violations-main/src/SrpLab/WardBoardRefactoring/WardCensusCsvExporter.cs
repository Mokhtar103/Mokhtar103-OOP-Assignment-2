using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WardBoardRefactoring
{
    public class WardCensusCsvExporter
    {
        public string Export(WardBedBoard board)
        {
            var lines = new List<string> { "bed,patient,acuity" };

            foreach (var bed in board.Patients.Keys.OrderBy(x => x))
            {
                lines.Add(
                    $"{bed},{board.Patients[bed]},{board.AcuityScores[bed]}"
                );
            }

            return string.Join('\n', lines);
        }
    }
}
