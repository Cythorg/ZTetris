using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    public interface IGameObject
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
