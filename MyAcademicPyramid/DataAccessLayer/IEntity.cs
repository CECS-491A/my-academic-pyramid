using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Interface which allow repository class access the commn Id attribute in every entity class
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; }
    }
}
