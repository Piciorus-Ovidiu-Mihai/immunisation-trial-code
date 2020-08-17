using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class SurveyQuestionViewModel
    {
        [Key]
        public int SurveyQuestionId { get; set; }
        [Display(Name ="Question Type")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Text")]
        public string SurveyQuestionText { get; set; }
        [Display(Name = "Mandatory")]
        public Boolean IsMandatory { get; set; }
        public int SurveyId { get; set; }
        [Display(Name = "Answers")]
        public List<SurveyPossibleAnswerViewModel> SurveyPossibleAnswersList { get; set; }
        public string SelectedAnswer { get; set; }
    }
}
