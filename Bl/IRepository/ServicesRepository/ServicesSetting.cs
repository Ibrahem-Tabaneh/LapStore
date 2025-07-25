using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesSetting : IServicesSetting
    {
        private readonly LapStoreContext ctx;

        public ServicesSetting(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }

        public Setting GetSetting()
        {
            try
            {
                var setting = ctx.Settings.FirstOrDefault();
                return setting;
            }catch (Exception ex)
            {
                return new Setting();
            }
        }


        public Setting GetById(int id)
        {
            try
            {
                var Setting = ctx.Settings.FirstOrDefault(x=>x.Id==id);
                return Setting;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Setting();
            }
        }

        public bool Save(Setting Setting)
        {
            try
            {
                if(Setting.Id==null||Setting.Id==0)
                {
                    ctx.Settings.Add(Setting);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldSetting = GetById(Setting.Id);
                    if(oldSetting != null)
                    {
                        oldSetting.Address = Setting.Address;
                        oldSetting.ContactNumber = Setting.ContactNumber;
                        oldSetting.InstgramLink = Setting.InstgramLink;
                        oldSetting.Email = Setting.Email;
                        oldSetting.Logo = Setting.Logo;
                        oldSetting.TwitterLink = Setting.TwitterLink;
                        oldSetting.WebsiteDescription = Setting.WebsiteDescription;
                        oldSetting.WebsiteName = Setting.WebsiteName;
                        ctx.Settings.Update(oldSetting);
                        ctx.SaveChanges() ;
                    }
                    
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
