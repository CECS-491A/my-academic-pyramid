using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        Repository<Claim> ClaimRepository { get; }
        Repository<PasswordQA> PasswordQA { get; }
        Repository<User> UserRepository { get; }

        void Commit();
    }
}