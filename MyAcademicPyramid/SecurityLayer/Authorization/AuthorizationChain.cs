using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer.Authorization
{
    public class AuthorizationChain
    {
        private IChecker root;
        private IChecker lastChecker;

        /// <summary>
        /// Sets a checker at end of chain.
        /// </summary>
        /// <param name="checker"></param>
        public void SetCheckerAtEnd(IChecker checker)
        {
            if (root == null)
            {
                root = checker;
                lastChecker = checker;
            }
            else
            {
                lastChecker.SetNextChecker(checker);
                lastChecker = checker;
            }
            
        }

        public bool ProcessUser(User user)
        {
            if (root == null)
            {
                throw new NullReferenceException("Root hasn't been set.");
            }
            return root.Process(user);
        }
    }
}
