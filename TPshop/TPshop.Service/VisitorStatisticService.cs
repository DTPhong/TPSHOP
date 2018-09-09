using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface IVisitorStatisticService
    {
        VisitorStatistic Add(VisitorStatistic visitorStatistic);

        VisitorStatistic Delete(int id);

        IEnumerable<VisitorStatistic> GetAll();

        VisitorStatistic GetById(int id);

        void Save();
    }

    public class VisitorStatisticService : IVisitorStatisticService
    {
        private IVisitorStatisticRepository _visitorStatisticRepository;
        private IUnitOfWork _unitOfWork;

        public VisitorStatisticService(IVisitorStatisticRepository visitorStatisticRepository, IUnitOfWork unitOfWork)
        {
            this._visitorStatisticRepository = visitorStatisticRepository;
            this._unitOfWork = unitOfWork;
        }

        public VisitorStatistic Add(VisitorStatistic visitorStatistic)
        {
            return _visitorStatisticRepository.Add(visitorStatistic);
        }

        public VisitorStatistic Delete(int id)
        {
            return _visitorStatisticRepository.Delete(id);
        }

        public IEnumerable<VisitorStatistic> GetAll()
        {
            return _visitorStatisticRepository.GetAll();
        }

        public VisitorStatistic GetById(int id)
        {
            return _visitorStatisticRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}