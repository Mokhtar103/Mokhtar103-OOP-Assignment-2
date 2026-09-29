using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.GradeBookRefactoring
{
    public class HonorRollPolicy
    {
        public bool MeetsHonorRoll(decimal average, string letter)
        {
            return average >= 85 &&
                   letter is "A" or "B";
        }
    }
}
