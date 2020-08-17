using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Survey> surveyRepository;
        private IGenericRepository<SurveyQuestion> surveyQuestionRepository;
        private IGenericRepository<SurveyPossibleAnswer> surveyPossibleAnswerRepository;
        private IGenericRepository<UserQuestionAnswer> userQuestionAnswerRepository;
        private IGenericRepository<UserSurveyResponse> userSurveyResponseRepository;
        private IGenericRepository<UserAnswers> userAnswerRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Survey> SurveyRepository
        {
            get
            {

                if (this.surveyRepository == null)
                {
                    this.surveyRepository = new GenericRepository<Survey>(_context);
                }
                return surveyRepository;
            }
        }

        public IGenericRepository<SurveyQuestion> SurveyQuestionRepository
        {
            get
            {

                if (this.surveyQuestionRepository == null)
                {
                    this.surveyQuestionRepository = new GenericRepository<SurveyQuestion>(_context);
                }
                return surveyQuestionRepository;
            }
        }

        public IGenericRepository<SurveyPossibleAnswer> SurveyPossibleAnswerRepository
        {
            get
            {

                if (this.surveyPossibleAnswerRepository == null)
                {
                    this.surveyPossibleAnswerRepository = new GenericRepository<SurveyPossibleAnswer>(_context);
                }
                return surveyPossibleAnswerRepository;
            }
        }

        public IGenericRepository<UserQuestionAnswer> UserQuestionAnswerRepository
        {
            get
            {

                if (this.userQuestionAnswerRepository == null)
                {
                    this.userQuestionAnswerRepository = new GenericRepository<UserQuestionAnswer>(_context);
                }
                return userQuestionAnswerRepository;
            }
        }

        public IGenericRepository<UserSurveyResponse> UserSurveyResponseRepository
        {
            get
            {

                if (this.userSurveyResponseRepository == null)
                {
                    this.userSurveyResponseRepository = new GenericRepository<UserSurveyResponse>(_context);
                }
                return userSurveyResponseRepository;
            }
        }

        public IGenericRepository<UserAnswers> UserAnswersRepository
        {
            get
            {

                if (this.userAnswerRepository == null)
                {
                    this.userAnswerRepository = new GenericRepository<UserAnswers>(_context);
                }
                return userAnswerRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
