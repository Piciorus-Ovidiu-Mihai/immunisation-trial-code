using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class UserQuestionAnswerViewModel
    {
        public int UserQuestionAnswerId { get; set; }
        public int SurveyQuestionId { get; set; }
        public SurveyQuestionViewModel SurveyQuestion { get; set; }
        public int UserSurveyResponseId { get; set; }
        public List<UserAnswersViewModel> UserQuestionResponse { get; set; }
    }
}
