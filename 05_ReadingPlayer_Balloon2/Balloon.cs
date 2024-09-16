﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Balloon
{

    class Balloon : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balloon, background;
        Vector2 balloonOrigin, balloonPosition;

        public Balloon()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            balloon = Content.Load<Texture2D>("spr_lives");
            background = Content.Load<Texture2D>("spr_background");
            balloonOrigin = new Vector2(balloon.Width / 2, balloon.Height); //sets origin bottom centre of sprite
        }

        protected override void Update(GameTime gameTime)
        {
            QuitIfEscape();
            MouseState currentMouseState = Mouse.GetState();
            balloonPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(balloon, balloonPosition, null, Color.White, 0.0f, balloonOrigin, 1.0f, SpriteEffects.None, 0);
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