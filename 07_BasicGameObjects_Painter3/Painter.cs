using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//using System.Drawing;

namespace Painter
{
    class Painter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Cannon cannon;
        SpriteObject spriteObject;

        MouseState currentMouseState, previousMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;

        public Painter()
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
            /*
             * colour sequence = Blue-->Red-->Green (reversed if pressing left)
             */
            if (currentKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
            {
                if (cannon.Color == Color.Blue)
                {
                    cannon.Color = Color.Red;
                }
                else if (cannon.Color == Color.Red)
                {
                    cannon.Color = Color.Green;
                }
                else
                {
                    cannon.Color = Color.Blue;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
            {
                if (cannon.Color == Color.Blue)
                {
                    cannon.Color = Color.Green;
                }
                else if (cannon.Color == Color.Green)
                {
                    cannon.Color = Color.Red;
                }
                else
                {
                    cannon.Color = Color.Blue;
                }
            }

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