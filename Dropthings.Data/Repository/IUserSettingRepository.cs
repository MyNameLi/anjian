using System;
namespace Dropthings.Data.Repository
{
    public interface IUserSettingRepository : IDisposable
    {
        void Delete(Dropthings.Data.UserSettingEntity userSetting);
        Dropthings.Data.UserSettingEntity GetUserSettingByUserGuid(int userid);
        Dropthings.Data.UserSettingEntity Insert(Dropthings.Data.UserSettingEntity setting);
        void Update(Dropthings.Data.UserSettingEntity userSetting);
    }
}
