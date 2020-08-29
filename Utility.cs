using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    enum DefaultColors { Red, Green, Blie, Cyan, Yellow, Magenta, Orange }
    public class Utility 
    {
        public static List<Color> DefaultColors = new List<Color>(){
            new Color(255,0,0), //red
            new Color(0,255,0), //green
            new Color(0,0,255), //blue
            new Color(0,255,255), //cyan
            new Color(255,255,0), //yellow
            new Color(255,0,255), //magenta
            new Color(255,102,0) //orange
        };
    }
}
