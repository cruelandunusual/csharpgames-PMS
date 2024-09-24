using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Painter
{
    class Painter2 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Cannon cannon;
        SpriteObject spriteObject;

        MouseState currentMouseState, previousMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;

        public Painter2()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("spr_background");

            cannon = new Cannon(Content);
        }

        public void HandleInput()
        {
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R)) // if R pressed and not pressed in previous game loop
            {
                cannon.Color = Color.Red;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
            {
                cannon.Color = Color.Green;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B))
            {
                cannon.Color = Color.Blue;
            }

            double opposite = currentMouseState.Y - cannon.Position.Y;
            double adjacent = currentMouseState.X - cannon.Position.X;
            cannon.Angle = (float)Math.Atan2(opposite, adjacent);
        }

        protected override void Update(GameTime gameTime)
        {
            QuitIfEscape();
            HandleInput();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            cannon.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        private void QuitIfEscape()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }
    }
}