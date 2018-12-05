using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    // Common interface which allow repository access the Id attribute in every entity class
    public interface IEntity
    {
        int Id { get; }
    }
}
