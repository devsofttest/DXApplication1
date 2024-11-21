using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace DXApplication1.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class PictureItem : BaseObject, IPictureItem
    {
        [ImageEditor]
        public virtual byte[] Image { get; set; }
        public virtual string Text { get; set; }
    }
}
