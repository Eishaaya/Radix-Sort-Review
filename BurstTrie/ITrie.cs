using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrie
{
    public interface ITrie
    {
        void Insert(string value);
        bool Remove(string value);
        List<string> GetAll();
    }
}
