using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesSetting
    {
        public Setting GetById(int id);
        public Setting GetSetting();

        public bool Save(Setting Setting);  
    }
}
