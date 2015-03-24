using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfServer
{
    public interface IStreamableElementFactory
    {
        IStreamableElement Create(string key);   
    }
}
