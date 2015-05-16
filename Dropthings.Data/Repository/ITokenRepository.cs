using System;
namespace Dropthings.Data.Repository
{
    public interface ITokenRepository : IDisposable
    {
        void Delete(Dropthings.Data.TokenEntity page);
        Dropthings.Data.TokenEntity GetTokenByUniqueId(int tokenId);
        Dropthings.Data.TokenEntity Insert(Dropthings.Data.TokenEntity token);
        void Update(Dropthings.Data.TokenEntity token);
    }
}
