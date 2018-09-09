using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);
        void Save();
    }
    public class FeedbackService : IFeedbackService
    {
        IFeedbackRepository _feedbackRepository;
        IUnitOfWork _unitOfWork;
        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
