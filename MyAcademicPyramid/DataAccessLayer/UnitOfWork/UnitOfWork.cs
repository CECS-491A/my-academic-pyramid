
using DataAccessLayer.Models;
using DataAccessLayer.Repository;


namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _context = new DatabaseContext();
        private Repository<User> _userRepository;
        private Repository<Claim> _claimRepository;
        private Repository<PasswordQA> _passwordQA;


        public Repository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new Repository<User>(_context);
                }
                return _userRepository;
            }
        }

        public Repository<Claim> ClaimRepository
        {
            get
            {
                if (_claimRepository == null)
                {
                    _claimRepository = new Repository<Claim>(_context);
                }
                return _claimRepository;
            }
        }

        public Repository<PasswordQA> PasswordQA
        {
            get
            {
                if (_passwordQA == null)
                {
                    _passwordQA = new Repository<PasswordQA>(_context);
                }
                return _passwordQA;
            }
        }



        public void Commit()
        {
            _context.SaveChanges();
        }


    }
}
