using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaYazılım
{
    public interface IProcess
    {
        public List<Response> DoEverything(Base data);
    }
}
