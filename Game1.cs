using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_4___Bomb
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        MouseState  mouseState;

        Rectangle window;

        Texture2D bombTexture;

        Rectangle resetRect;

        Rectangle bombRect;

        SpriteFont bombText;

        SoundEffect explosion;

        float seconds;

        bool exploded;


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
            bombRect = new Rectangle(50, 50, 700, 400);
            resetRect = new Rectangle(254, 132, 12, 18);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            bombText = Content.Load<SpriteFont>("BombFont");
            explosion = Content.Load<SoundEffect>("explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            this.Window.Title = mouseState.Position.ToString();

            if (mouseState.LeftButton == ButtonState.Pressed && resetRect.Contains(mouseState.Position))
                seconds = 0f;
            


            // TODO: Add your update logic here
           
            if (!exploded)
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 10) 
            {
                seconds = 0;
                explosion.Play();
                exploded = true;
            }

            //h

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(bombText, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
