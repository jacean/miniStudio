using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace miniStudio
{
    [Serializable]
    class controlState
    {
        public string Name{ get;set;}
        public string Location { get; set; }
        public Rectangle Bounds{get;set;}
        public string Text { get; set; }

    }
}
