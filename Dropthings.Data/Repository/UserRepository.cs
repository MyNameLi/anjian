namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;
    using Dropthings.Util;
    using System.Linq.Expressions;
    
    using System.Data;
    using System.Data.SqlClient;

    public class UserRepository : Dropthings.Data.Repository.IUserRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly UsersEntity.UsersDAO _userDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public UserRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._userDAO = new UsersEntity.UsersDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public UsersEntity GetUser(string userName, string passWord)
        {
            SqlParameter[] parameters = { new SqlParameter("@USERNAME",SqlDbType.NVarChar) ,
                new SqlParameter("@PASSWORD", SqlDbType.NVarChar)  
            };
            parameters[0].Value = userName;
            parameters[1].Value = passWord;

            IList<UsersEntity> list = _userDAO.Find("ACCID=@USERNAME AND PASSWORD=@PASSWORD", parameters);
            if (list.Count > 0)
            {
                return list[0];
            }
            else {
                return null;
            }
        }

        public List<RolesEntity> GetRolesOfUser(int userID)
        {
            return AspectF.Define
                .Cache<List<RolesEntity>>(_cacheResolver, CacheKeys.UserKeys.RolesOfUser(userID))
                .Return<List<RolesEntity>>(() =>
                    _userDAO.GetRolesOfUser(userID));
        }
        public int CreateUser(string registeredUserName, string password, string email)
        {
            _userDAO.Add(new UsersEntity
            {
                USERID = 0,
                PASSWORD = password,
                EMAIL = email,
                CREATEDATE = DateTime.Now,
                MOBILE = "",
                USERNAME = registeredUserName,
                LASTLOGINIP = "",
                LASTLOGINDATE = DateTime.Now
            });
            return this.GetUserIDFromUserName(registeredUserName);
        }

        public IList<UsersEntity> GetUsersList(string idlist, bool tag) {
            return _userDAO.GetUsersList(idlist, tag);
        }

        public int CreateUser(UsersEntity entity)
        {
            _userDAO.Add(entity);
            return this.GetUserIDFromUserName(entity.USERNAME);
        }

        public UsersEntity GetUserByUserID(int userID)
        {
            return AspectF.Define
                .Cache<UsersEntity>(_cacheResolver, CacheKeys.UserKeys.UserFromUserID(userID))
                .Return<UsersEntity>(() =>
                    _userDAO.FindById(userID));
        }

        public void Update(UsersEntity member)
        {
            _userDAO.Update(member);
        }               

        public void DeleteUser(string userName)
        {
            UsersEntity entity = this.GetUserFromUserName(userName);
            _userDAO.Delete(entity);
        }

        public bool DeleteUser(int userid) {
            return _userDAO.Delete(userid);
        }

        public UsersEntity GetUserFromUserName(string userName)
        {
            SqlParameter[] parameters = { new SqlParameter("@USERNAME",SqlDbType.NVarChar) };
            parameters[0].Value = userName;

            return AspectF.Define
                .Cache<UsersEntity>(_cacheResolver, CacheKeys.UserKeys.UserFromUserName(userName))
                .Return<UsersEntity>(() =>
                    _userDAO.Find("USERNAME=@USERNAME", parameters).FirstOrDefault());
        }

        public int GetUserIDFromUserName(string userName)
        {
            UsersEntity entity = GetUserFromUserName(userName);
            if (null != entity)
            {
                return entity.USERID.Value;
            }
            return 0;
            //return AspectF.Define
            //    .Cache<Guid>(_cacheResolver, CacheKeys.UserKeys.UserGuidFromUserName(userName))
            //    .Return<Guid>(() =>
            //        _database.Query(CompiledQueries.UserQueries.GetUserGuidFromUserName, userName)
            //        .FirstOrDefault());
        }


        public DataTable GetUsersDTFromStrSql(string strSql) {
            return _userDAO.GetUserInnerRoleDataTable(strSql);
        }

        public DataTable GetUserPagerDataTable(string where, string orderBy, int pageSize, int pageNumber)
        {
            return _userDAO.GetPager(where, orderBy, pageSize, pageNumber);
        }

        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            //_database.Dispose();
        }

        #endregion

    }
}