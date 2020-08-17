using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyClassLibrary.Models
{
    [Table("SurveyQuestions")]
    public class SurveyQuestion
    {
        public int SurveyQuestionId { get; set; }
        public string Type { get; set; }
        public string SurveyQuestionText { get; set; }
        public Boolean IsMandatory { get; set; }
        public int SurveyId { get; set; }
        public List<SurveyPossibleAnswer> SurveyPossibleAnswersList { get; set; }

    }
}
