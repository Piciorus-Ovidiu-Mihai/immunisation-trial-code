using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyClassLibrary.Models
{
    [Table("SurveyPossibleAnswers")]
    public class SurveyPossibleAnswer
    {
        public int SurveyPossibleAnswerId { get; set; }
        public string SurveyPossibleAnswerText { get; set; }
        public int SurveyQuestionId { get; set; }
    }
}
