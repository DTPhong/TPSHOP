using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface ISettingService
    {
        Setting Add(Setting Setting);

        Setting Delete(int id);

        void Update(Setting Setting);

        Setting GetById(int id);

        void Save();
    }

    public class SettingService : ISettingService
    {
        private ISettingRepository _settingRepository;
        private IUnitOfWork _unitOfWork;

        public SettingService(ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            this._settingRepository = settingRepository;
            this._unitOfWork = unitOfWork;
        }

        public Setting Add(Setting Setting)
        {
            return _settingRepository.Add(Setting);
        }

        public Setting Delete(int id)
        {
            return _settingRepository.Delete(id);
        }

        public Setting GetById(int id)
        {
            return _settingRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Setting Setting)
        {
            _settingRepository.Update(Setting);
        }
    }
}