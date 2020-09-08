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
// []██[][]
// []██[][]
// []██[][]
// []██[][]

// red   



// ████
// ████

// yellow   



// []██[]
// ██████
// [][][]

// magenta



// [][]██
// ██████
// [][][]

// orange



// ██[][]
// ██████
// [][][]

// blue



// []████
// ████[]
// [][][]

// green



// ████[]
// []████
// [][][]

// cyan

//tetromino switch as blocks instead of blockstate

//Blocks = new Block[4, 4]
//{
//    {Block.Empty           , new Block(1, 0, Color), Block.Empty           , Block.Empty           },
//    {Block.Empty           , new Block(1, 1, Color), Block.Empty           , Block.Empty           },
//    {Block.Empty           , new Block(1, 2, Color), Block.Empty           , Block.Empty           },
//    {Block.Empty           , new Block(1, 3, Color), Block.Empty           , Block.Empty           }
//};
//Blocks = new Block[2, 2]
//{
//    {new Block(0, 0, Color), new Block(1, 0, Color)},
//    {new Block(0, 1, Color), new Block(1, 1, Color)}
//};
//Blocks = new Block[3, 3]
//{
//    {Block.Empty           , new Block(1, 0, Color), Block.Empty            },
//    {new Block(0, 1, Color), new Block(1, 1, Color), new Block(2, 1, Color) },
//    {Block.Empty           , Block.Empty           , Block.Empty            }
//};
//Blocks = new Block[3, 3]
//{
//    {Block.Empty           , Block.Empty           , new Block(2, 0, Color)},
//    {new Block(0, 1, Color), new Block(1, 1, Color), new Block(2, 1, Color) },
//    {Block.Empty           , Block.Empty           , Block.Empty            }
//};
//Blocks = new Block[3, 3]
//{
//    {new Block(0, 0, Color), Block.Empty           , Block.Empty            },
//    {new Block(0, 1, Color), new Block(1, 1, Color), new Block(2, 1, Color) },
//    {Block.Empty           , Block.Empty           , Block.Empty            }
//};
//Blocks = new Block[3, 3]
//{
//    {Block.Empty           , new Block(1, 0, Color), new Block(2, 0, Color)},
//    {new Block(0, 1, Color), new Block(1, 1, Color), Block.Empty            },
//    {Block.Empty           , Block.Empty           , Block.Empty            }
//};
//Blocks = new Block[3, 3]
//{
//    {new Block(0, 0, Color), new Block(1, 0, Color), Block.Empty            },
//    {Block.Empty           , new Block(1, 1, Color), new Block(2, 1, Color) },
//    {Block.Empty           , Block.Empty           , Block.Empty            }
//};