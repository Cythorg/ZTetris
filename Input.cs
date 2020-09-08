using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    class Input
    {
        public void Update(GameTime gameTime) //maybe not gametime?
        {
            Keyboard.GetState();
        }
    }
}
