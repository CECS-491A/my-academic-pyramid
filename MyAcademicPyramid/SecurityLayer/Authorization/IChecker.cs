using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer.Authorization
{
    public interface IChecker
    {
        /// <summary>
        /// Validates the specific authorization of user that the
        /// checker is in charge of.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if the user passess the authorization check and the
        /// rest in the chain.
        /// Otherwise, false.</returns>
        bool Process(User user);
        void SetNextChecker(IChecker checher);
    }
}
