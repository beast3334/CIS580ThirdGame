using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using RunnerLibrary;
namespace MonoGameWindowsStarter
{
    enum State
    { 
        STATE_MOVING,
        STATE_JUMPING,
        STATE_FALLING
    }
    class Player
    {
        BoundingRectangle bounds;
        Game1 game;
        Texture2D texture;
        SoundEffect jumpEffect;
        ContentManager content;
        State state = State.STATE_MOVING;
        TimeSpan jumpTimer;
        const int JUMP_TIME = 600;
        public BoundingRectangle Bounds
        { get { return bounds; } }
        public Rectangle RectBounds
        {
            get { return bounds; }
        }

        public Player(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
        }
        public void LoadContent()
        {
            bounds.Width = 25;
            bounds.Height = 25;
            bounds.X = 25;
            bounds.Y = 375;
            texture = content.Load<Texture2D>("Player");
            jumpEffect = content.Load<SoundEffect>("Jump");
        }
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            switch (state)
            {
                case State.STATE_MOVING:
                    if(keyboardState.IsKeyDown(Keys.Space))
                    {
                        jumpTimer = new TimeSpan(0);
                        state = State.STATE_JUMPING;
                        jumpEffect.Play();
                    }
                    break;
                case State.STATE_JUMPING:
                    jumpTimer += gameTime.ElapsedGameTime;
                    bounds.Y -= (300 / (float)jumpTimer.TotalMilliseconds);
                    if (jumpTimer.TotalMilliseconds >= JUMP_TIME) state = State.STATE_FALLING;
                    break;
                case State.STATE_FALLING:
                    bounds.Y += game.speed + 2;
                    if(bounds.Y >= 375)
                    {
                        bounds.Y = 375;
                        state = State.STATE_MOVING;
                    }
                    break;
                default:
                    break;
            }
            bounds.X += game.speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
        

    }
}
