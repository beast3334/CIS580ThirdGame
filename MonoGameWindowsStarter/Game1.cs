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
using System.Diagnostics;
using RunnerLibrary;
using RunnerContentExtension;
namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Ground ground;
        Vector2 offset;
        float prevOffset;
        TimeSpan enemySpawnTimer;
        List<Enemy> enemyList;
        List<ScoreBox> scoreBoxList;
        SoundEffect hitEffect;
        SpriteFont font;
        Random random;
        Tilemap tilemap;
        int randomSpawnRate = 1;
        Color currentColor = new Color();
        Color finalColor = new Color();
        Color oldColor = new Color();
        bool gameOver = false;
        float soundEffectTimer;
        float scoreTimer;
        float difficultyTimer;
        int score = 0;
        int lives = 3;
        public int speed = 3;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(this, Content);
            enemyList = new List<Enemy>();
            scoreBoxList = new List<ScoreBox>();
            random = new Random();
            currentColor = Color.CornflowerBlue;
            finalColor = Color.FromNonPremultiplied(random.Next(256), random.Next(256), random.Next(256), 255);
            oldColor = Color.Black;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            ground = new Ground(this, Content, -200);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hitEffect = Content.Load<SoundEffect>("Hit_Hurt");
            font = Content.Load<SpriteFont>("font");
            player.LoadContent();
            tilemap = Content.Load<Tilemap>("level1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {

                soundEffectTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                scoreTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                difficultyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                enemySpawnTimer += gameTime.ElapsedGameTime;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here
                player.Update(gameTime);
                ground.Update(gameTime);
                if (offset.X - 500 <= prevOffset)
                {
                    ground = new Ground(this, Content, (int)offset.X);
                }

                if (enemySpawnTimer.TotalSeconds >= randomSpawnRate)
                {

                    enemyList.Add(new Enemy(this, Content, (int)player.Bounds.X + 1000));
                    scoreBoxList.Add(new ScoreBox(this, Content, (int)player.Bounds.X + 1000));
                    enemySpawnTimer = new TimeSpan(0);
                    randomSpawnRate = random.Next(1, 5);
                }
                prevOffset = offset.X;

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (player.Bounds.X - 800 >= enemyList[i].Bounds.X)
                    {
                        enemyList.RemoveAt(i);
                        scoreBoxList.RemoveAt(i);
                        i--;
                    }
                }

                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.RectBounds.Intersects(player.RectBounds))
                    {
                        if (soundEffectTimer > 1)
                        {
                            hitEffect.Play();
                            soundEffectTimer = 0;
                            lives--;
                        }
                    }
                }
                foreach(ScoreBox box in scoreBoxList)
                {
                    if(box.RectBounds.Intersects(player.RectBounds))
                    {
                        if(scoreTimer >= 0.5)
                        {
                            score += 10;
                            scoreTimer = 0;
                        }
                        
                    }
                }
                if(difficultyTimer >= 15)
                {
                    speed += 1;
                    difficultyTimer = 0;
                }
                if (lives == 0)
                {
                    gameOver = true;
                }
            }
            else
            {
                var keyboardState = Keyboard.GetState();

                if(keyboardState.IsKeyDown(Keys.Space))
                {
                    score = 0;
                    speed = 3;
                    lives = 3;
                    gameOver = false;
                    enemyList.Clear();
                    scoreBoxList.Clear();
                }
            }
            base.Update(gameTime);
        }
        public void GetBackground(Color finalColor)
        {
            oldColor = currentColor;
            currentColor = Color.Lerp(currentColor, finalColor, 0.01f);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            offset = new Vector2(200, 300) - new Vector2(player.Bounds.X, player.Bounds.Y);
            var t = Matrix.CreateTranslation(offset.X, offset.Y, 0);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, t);
            tilemap.Draw(spriteBatch);
            if (!gameOver)
            {
                if (currentColor != oldColor)
                {
                    GetBackground(finalColor);
                }
                else
                {
                    finalColor = Color.FromNonPremultiplied(random.Next(100, 256), random.Next(100, 256), random.Next(100, 256), 255);
                    oldColor = Color.Black;
                }
                GraphicsDevice.Clear(currentColor);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(player.Bounds.X - 200, player.Bounds.Y - 300), Color.White);
                spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(player.Bounds.X - 200, player.Bounds.Y - 280), Color.White);
                spriteBatch.DrawString(font, "Difficulty: " + (speed - 2), new Vector2(player.Bounds.X - 200, player.Bounds.Y - 260), Color.White);
                player.Draw(spriteBatch);
                ground.Draw(spriteBatch);
                foreach (Enemy enemy in enemyList)
                {
                    enemy.Draw(spriteBatch);
                }
                
            }
            else
            {
                spriteBatch.DrawString(font, "Game Over, Press Space to Play Again!", new Vector2(player.Bounds.X + 75, player.Bounds.Y - 50), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
