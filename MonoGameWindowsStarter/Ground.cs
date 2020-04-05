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
    class Ground: ISprite
    {
        public BoundingRectangle bounds;
        Game1 game;
        Texture2D texture;
        ContentManager content;

        public Ground(Game1 game, ContentManager content, int x)
        {
            this.game = game;
            this.content = content;
            bounds.X = -500;
            LoadContent();
        }
        public void LoadContent()
        {
            bounds.Width = 100000;
            bounds.Height = 25;
            bounds.Y = 425;
            texture = content.Load<Texture2D>("ground");
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }


    }
}
