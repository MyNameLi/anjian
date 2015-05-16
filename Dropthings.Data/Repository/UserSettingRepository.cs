using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OmarALZabir.AspectF;
using Dropthings.Util;

namespace Dropthings.Data.Repository
{
    public class UserSettingRepository : Dropthings.Data.Repository.IUserSettingRepository 
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly UserSettingEntity.UserSettingDAO _userSettingDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public UserSettingRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._userSettingDAO = new UserSettingEntity.UserSettingDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public UserSettingEntity GetUserSettingByUserGuid(int userid)
        {
            return AspectF.Define
                .Cache<UserSettingEntity>(_cacheResolver, CacheKeys.UserKeys.UserSettingByUserID(userid))
                .Return<UserSettingEntity>(() => _userSettingDAO.FindById(userid));

            //return AspectF.Define
            //    .Cache<UserSetting>(_cacheResolver, CacheKeys.UserKeys.UserSettingByUserGuid(userGuid))
            //    .Return<UserSetting>(() => _database.Query(
            //                CompiledQueries.UserQueries.GetUserSettingByUserGuid, userGuid)
            //                .FirstOrDefault());
        }

        public void Delete(UserSettingEntity userSetting)
        {
            _userSettingDAO.Delete(userSetting);
            RemoveUserSettingCacheForUser(userSetting);
            //_database.Delete<UserSetting>( userSetting);
        }

        public UserSettingEntity Insert(UserSettingEntity setting)
        {
            //var user = setting.AspNetUser;
            //var Tab = setting.CurrentTab;
            
            //setting.AspNetUser = null;
            //setting.CurrentTab = null;

            //_database.Insert<AspNetUser, Tab, UserSetting>(user, Tab,
            //    (u, s) => s.AspNetUser = u,
            //    (p, s) => s.CurrentTab = p,
            //    setting);
            
            //setting.AspNetUser = user;
            //setting.CurrentTab = Tab;
            _userSettingDAO.Add(setting);
            return setting;
        }

        public void Update(UserSettingEntity userSetting)
        {
            _userSettingDAO.Update(userSetting);
            RemoveUserSettingCacheForUser(userSetting);
            //_database.Update<UserSetting>(userSetting);
        }

        private void RemoveUserSettingCacheForUser(UserSettingEntity userSetting)
        {
            _cacheResolver.Remove(CacheKeys.UserKeys.UserSettingByUserID(userSetting.USERID.Value));
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion
    }
}
