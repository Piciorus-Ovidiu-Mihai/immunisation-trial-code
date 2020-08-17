using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class SurveyCreateViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public SurveyViewModel SurveyViewModel { get; set; }
        public SurveyQuestionViewModel SurveyQuestionViewModel { get; set; }
        public SurveyPossibleAnswerViewModel SurveyPossibleAnswerViewModel { get; set; }
    }
}
