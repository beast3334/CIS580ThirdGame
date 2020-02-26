using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
namespace MonoGameWindowsStarter
{
    class ScoreBox
    {
        BoundingRectangle bounds;
        Game1 game;
        Texture2D texture;
        ContentManager content;
        public BoundingRectangle Bounds
        { get { return bounds; } }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }
        public ScoreBox(Game1 game, ContentManager content, int x)
        {
            this.game = game;
            this.content = content;
            bounds.X = x;
            LoadContent();
        }
        public void LoadContent()
        {
            bounds.Width = 25;
            bounds.Height = 25;
            bounds.Y = 350;
        }
        public void Update(GameTime gameTime)
        {
        }

    }
}
