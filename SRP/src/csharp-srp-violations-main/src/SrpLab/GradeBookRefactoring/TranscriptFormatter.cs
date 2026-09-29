using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.GradeBookRefactoring
{
    public class TranscriptFormatter
    {
       public string Create(
       string studentId,
       string fullName,
       decimal average,
       string letter,
       bool honorRoll)
        {
            return $"TRANSCRIPT\n" +
                   $"Student: {fullName} ({studentId})\n" +
                   $"Average: {average}\n" +
                   $"Letter: {letter}\n" +
                   $"Honor: {honorRoll}\n";
        }
    }
}
