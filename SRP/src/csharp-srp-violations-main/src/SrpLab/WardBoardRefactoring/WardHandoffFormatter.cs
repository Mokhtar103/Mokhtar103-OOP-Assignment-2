using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WardBoardRefactoring
{
    public class WardHandoffFormatter
    {
        public string Build(WardBedBoard board, int bed)
        {
            if (!board.TryGetPatient(bed, out var patient))
                return $"Bed {bed}: empty";

            var acuity = board.GetAcuity(bed);

            var tone = acuity >= 8
                ? "ESCALATE"
                : acuity >= 4
                    ? "WATCH"
                    : "STABLE";

            return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] " +
                   $"Bed {bed} · {patient} · " +
                   $"acuity={acuity} · {tone}";
        }
    }
}
