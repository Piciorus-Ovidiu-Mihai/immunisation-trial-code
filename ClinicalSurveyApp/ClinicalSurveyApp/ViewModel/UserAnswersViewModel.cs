using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class UserAnswersViewModel
    {
        public int UserAnswersId { get; set; }
        public int SurveyPossibleAnswerId { get; set; }
        public SurveyPossibleAnswerViewModel SurveyPossibleAnswer { get; set; }
        public int UserQuestionAnswerId { get; set; }
        public bool Checked { get; set; }
        public string SelectedAnswer { get; set; }
        public string UserInput { get; set; }
    }
}
