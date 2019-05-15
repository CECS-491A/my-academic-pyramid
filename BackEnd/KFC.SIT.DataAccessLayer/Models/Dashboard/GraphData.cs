using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dashboard
{
    public class GraphData<T>
    {
        public ICollection<string> labels;
        public ICollection<T> data;

        public GraphData(ICollection<string> labels, ICollection<T> data)
        {
            this.labels = labels;
            this.data = data;
        }
    }
}
