using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyClassLibrary.Models
{
    public class UserAnswers
    {
        public int UserAnswersId { get; set; }
        public int SurveyPossibleAnswerId { get; set; }
        public SurveyPossibleAnswer SurveyPossibleAnswer { get; set; }
        public int UserQuestionAnswerId { get; set; }
        public string UserInput { get; set; }

    }
}
