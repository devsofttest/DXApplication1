using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication1.Module.BusinessObjects
{
    public interface IPictureItem
    {
        byte[] Image { get; }
        string Text { get; }
    }
}
