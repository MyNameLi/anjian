namespace Dropthings.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OmarALZabir.AspectF;

    public class TokenRepository : Dropthings.Data.Repository.ITokenRepository
    {
        #region Fields

        //private readonly IDatabase _database;
        private readonly TokenEntity.TokenDAO _tokenDAO;
        private readonly ICache _cacheResolver;

        #endregion Fields

        #region Constructors

        public TokenRepository(ICache cacheResolver)
        {
            //this._database = database;
            this._tokenDAO = new TokenEntity.TokenDAO();
            this._cacheResolver = cacheResolver;
        }

        #endregion Constructors

        #region Methods

        public void Delete(TokenEntity page)
        {
            this._tokenDAO.Delete(page.ID.Value);
            //_database.Delete<Token>(page);
        }

        public TokenEntity GetTokenByUniqueId(int tokenId)
        {
            return _tokenDAO.FindById(tokenId);
            //return _database.Query(
            //    CompiledQueries.MiscQueries.GetTokenByUniqueId, tokenId)
            //    .First();
        }

        public TokenEntity Insert(TokenEntity token)
        {
            //var user = token.AspNetUser;
            //token.AspNetUser = null;
            //_database.Insert<AspNetUser, Token>(user,
            //    (u, t) => t.AspNetUser = u,
            //    token);
            //token.AspNetUser = user;
            _tokenDAO.Add(token);
            return token;
        }

        public void Update(TokenEntity token)
        {
            _tokenDAO.Update(token);
            //_database.Update<Token>(token);
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