using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.GradeBookRefactoring
{
    public class GradeCsvExporter
    {
       public string Export(
       StudentGradeRecords records,
       GradePolicy gradePolicy,
       HonorRollPolicy honorRollPolicy)
        {
            var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

            foreach (var id in records.StudentIds())
            {
                var average = records.Average(id);
                var letter = gradePolicy.Letter(average);
                var honor = honorRollPolicy.MeetsHonorRoll(
                    average,
                    letter);

                rows.Add(
                    $"{id},{average},{letter},{(honor ? 1 : 0)}");
            }

            return string.Join('\n', rows);
        }
    }
}
