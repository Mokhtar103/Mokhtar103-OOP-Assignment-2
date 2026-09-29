using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.WardBoardRefactoring
{
    public class WardBedBoard
    {
        private readonly Dictionary<int, string> _bedPatient = new();
        private readonly Dictionary<int, int> _vitalsScore = new();

        public void AssignBed(
            int bed,
            string patientId,
            int heartRate,
            int spo2,
            PatientAcuityScorer acuityScorer,
            PagerAlertLog pagerLog)
        {
            if (bed <= 0)
                throw new ArgumentOutOfRangeException(nameof(bed));

            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("patient required");

            _bedPatient[bed] = patientId.Trim().ToUpperInvariant();

            var acuity = acuityScorer.Score(heartRate, spo2);
            _vitalsScore[bed] = acuity;

            if (acuity >= 8)
            {
                pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
            }
        }

        public bool TryGetPatient(
            int bed,
            out string patient)
        {
            return _bedPatient.TryGetValue(bed, out patient!);
        }

        public int GetAcuity(int bed)
        {
            return _vitalsScore[bed];
        }

        public IReadOnlyDictionary<int, string> Patients => _bedPatient;

        public IReadOnlyDictionary<int, int> AcuityScores => _vitalsScore;
    }
}
