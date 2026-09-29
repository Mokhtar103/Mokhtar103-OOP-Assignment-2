using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.LoanDeskRefactoring
{
    public class LoanRiskEvaluator
    {
        public decimal RiskScore(LoanApplication application)
        {
            decimal score = 100m;

            score -= Math.Max(0, 700 - application.CreditScore) * 0.15m;

            if (application.EmploymentMonths < 6)
                score -= 20m;

            if (application.RequestedAmount > 50_000m && !application.HasCollateral)
            {
                score -= 25m;
            }

            if (application.RequestedAmount > 150_000m)
                score -= 10m;

            return Math.Clamp(score, 0m, 100m);
        }

        public bool IsEligible(LoanApplication application)
        {
            return RiskScore(application) >= 55m &&
                   application.CreditScore >= 580;
        }
    }
}
