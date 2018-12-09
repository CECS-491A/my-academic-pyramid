using DataAccessLayer.Repository;


namespace DataAccessLayer
{
    public class UnitOfWork 
    {
        private myAPEntities1 _context = new myAPEntities1();
        private Repository<User> _userRepository;
        private Repository<Claim> _claimRepository;


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



        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
