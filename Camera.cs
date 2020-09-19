using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    class Camera
    {
        public Matrix Transform { get; set; }

        Viewport Viewport;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
        }

        public void Update(Viewport viewport)
        {
            Viewport = viewport;
            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-(Settings.GridSize*6) + Viewport.Width / 2, 0, 0));
        }
    }
}
