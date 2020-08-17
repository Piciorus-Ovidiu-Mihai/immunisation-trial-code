using SurveyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Survey> SurveyRepository { get; }

        IGenericRepository<SurveyQuestion> SurveyQuestionRepository { get; }

        IGenericRepository<SurveyPossibleAnswer> SurveyPossibleAnswerRepository { get; }

        IGenericRepository<UserQuestionAnswer> UserQuestionAnswerRepository { get; }

        IGenericRepository<UserSurveyResponse> UserSurveyResponseRepository { get; }

        IGenericRepository<UserAnswers> UserAnswersRepository { get; }

        void Save();

        new void Dispose();
    }
}
