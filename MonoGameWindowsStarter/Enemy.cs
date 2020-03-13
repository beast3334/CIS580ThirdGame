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
using RunnerLibrary;
namespace MonoGameWindowsStarter
{
    class Enemy
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
        public Enemy(Game1 game, ContentManager content, int x)
        {
            this.game = game;
            this.content = content;
            bounds.X = x;
            LoadContent();
        }
        public void LoadContent()
        {
            bounds.Width = 50;
            bounds.Height = 35;
            bounds.Y = 370;
            texture = content.Load<Texture2D>("spike");
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }


    }
}
