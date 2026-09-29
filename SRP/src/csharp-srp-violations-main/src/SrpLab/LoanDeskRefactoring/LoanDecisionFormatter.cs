using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab.LoanDeskRefactoring
{
    public class LoanDecisionFormatter
    {
        private readonly LoanRiskEvaluator _riskEvaluator;
        private readonly LoanDocumentRequirements _documentRequirements;

        public LoanDecisionFormatter(
            LoanRiskEvaluator riskEvaluator,
            LoanDocumentRequirements documentRequirements)
        {
            _riskEvaluator = riskEvaluator;
            _documentRequirements = documentRequirements;
        }

        public string DecisionLetter(
            LoanApplication application,
            string applicantName)
        {
            var risk = _riskEvaluator.RiskScore(application);
            var eligible = _riskEvaluator.IsEligible(application);

            if (eligible)
            {
                var documents =
                    _documentRequirements.GetRequiredDocuments(application);

                return $"Dear {applicantName},\n" +
                       $"Your request for {application.RequestedAmount:C} " +
                       $"is pre-approved (risk {risk:0}).\n" +
                       $"Please upload: {string.Join("; ", documents)}.\n";
            }

            return $"Dear {applicantName},\n" +
                   $"We are unable to approve " +
                   $"{application.RequestedAmount:C} at this time.\n" +
                   $"Reference risk={risk:0}. " +
                   $"You may reapply after improving documentation.\n";
        }

        public string UnderwriterCsvRow(
            LoanApplication application,
            string applicationId)
        {
            var risk = _riskEvaluator.RiskScore(application);
            var eligible = _riskEvaluator.IsEligible(application);

            return $"{applicationId}," +
                   $"{application.CreditScore}," +
                   $"{application.EmploymentMonths}," +
                   $"{(application.HasCollateral ? 1 : 0)}," +
                   $"{risk:0.00}," +
                   $"{(eligible ? "Y" : "N")}";
        }
    }
}
