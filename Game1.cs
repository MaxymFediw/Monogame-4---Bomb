using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security;
using System.Threading.Channels;

namespace Monogame_4___Bomb
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        MouseState  mouseState;

        Rectangle window;

        Texture2D bombTexture, nukeTexture, pliersTexture, winTexture;

        Rectangle resetRect;

        Rectangle winRect;

        Rectangle pliersRect;

        Rectangle doneRect;

        Rectangle nukeRect;

        Rectangle bombRect;

        SpriteFont bombText;

        SoundEffect explosion, victory;
        SoundEffectInstance explosionIntsance;
        SoundEffectInstance victoryInstance;

        float seconds;

        bool exploded, safe;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            seconds = 0f;
            
            exploded = false;
            safe = false;
            bombRect = new Rectangle(50, 50, 700, 400);
            resetRect = new Rectangle(253, 132, 12, 18);
            nukeRect = new Rectangle(-400, -250, 1600, 1000);
            pliersRect = new Rectangle(0, 0, 65, 56);
            doneRect = new Rectangle (724, 179, 12, 18);
            winRect = new Rectangle(0, 0, 800, 500);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            bombText = Content.Load<SpriteFont>("BombFont");
            explosion = Content.Load<SoundEffect>("explosion");
            victory = Content.Load<SoundEffect>("victorySound");
            explosionIntsance = explosion.CreateInstance();
            victoryInstance = victory.CreateInstance();
            nukeTexture = Content.Load<Texture2D>("nuke");
            pliersTexture = Content.Load<Texture2D>("pliers");
            winTexture = Content.Load<Texture2D>("youWin");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            this.Window.Title = mouseState.Position.ToString();

            if (mouseState.LeftButton == ButtonState.Pressed && resetRect.Contains(mouseState.Position))
                seconds = 0f;
            
            pliersRect.Location = mouseState.Position;


            // TODO: Add your update logic here
           
            if (!exploded)
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 15 && !safe) 
            {
                seconds = 0;
                explosionIntsance.Play();
                exploded = true;
                
            }
           
            if (!exploded)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && doneRect.Contains(mouseState.Position))
                {
                    victoryInstance.Play();
                    safe = true;
                    
                }
            }//Exiting means you won here.

                    //h

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(bombText, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            _spriteBatch.DrawString(bombText, ("Cut somewhere on"), new Vector2(40, 20), Color.Lime);
            _spriteBatch.DrawString(bombText, ("the red Wire"), new Vector2(100, 408), Color.Lime);

            if (exploded && !safe)
            {
                _spriteBatch.Draw(nukeTexture, nukeRect, Color.White);

                if (explosionIntsance.State == SoundState.Stopped) 
                {
                    Exit();
                }

            }

            if (safe && !exploded) 
            {
               _spriteBatch.Draw(winTexture, winRect, Color.White);
               
                if (victoryInstance.State == SoundState.Stopped)
                {
                    Exit();
                }
            }

            

            _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
