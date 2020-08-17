using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{
    public class SurveyPossibleAnswerViewModel
    {
        [Key]
        public int SurveyPossibleAnswerId { get; set; }
        [Required]
        [Display(Name = "Text")]
        public string SurveyPossibleAnswerText { get; set; }
        public bool Checked { get; set; }
    }
}
