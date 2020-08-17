using SurveyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class UserSurveyResponseViewModel
    {
        public int UserSurveyResponseId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SurveyId { get; set; }
        public SurveyViewModel Survey { get; set; }
        public List<UserQuestionAnswerViewModel> UserQuestionAnswersList { get; set; }
        [Display(Name="Completed on")]
        public DateTime CompletedOnDate { get; set; }
    }
}
