using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using System;

namespace SpriteDrawing
{

    class SpriteDrawing : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balloon;
        Vector2 balloonPos;
        Color background;
        float spriteX;
        float spriteY;
        Song mySound;



        public SpriteDrawing()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            balloonPos = Vector2.Zero;
            spriteX = 0f;
            spriteY = 0f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            balloon = Content.Load<Texture2D>("spr_lives");

            MediaPlayer.Play(Content.Load<Song>("snd_music"));
            //MediaPlayer.Play(Content.Load<Song>("snd_shoot_paint"));
            //mySound.Play();

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                spriteX -= 20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                spriteX += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                spriteY -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spriteY += 2;
            }

            /*
            //calculate background colour
            int backgroundComponent_RED = gameTime.TotalGameTime.Minutes * 2000;
            int backgroundComponent_GREEN = gameTime.TotalGameTime.Seconds * 300;
            int backgroundComponent_BLUE = gameTime.TotalGameTime.Milliseconds;
            background = new Color(
                backgroundComponent_RED,
                backgroundComponent_GREEN,
                backgroundComponent_BLUE);
            */
            float xPosition = (graphics.PreferredBackBufferHeight / 2) + (gameTime.TotalGameTime.Milliseconds / 2) + spriteX;
            //float yPosition = (graphics.PreferredBackBufferHeight / 2) - (gameTime.TotalGameTime.Milliseconds / 2) + spriteY;
            float yPosition = (graphics.PreferredBackBufferHeight / 2) + spriteY;
            balloonPos = new Vector2(xPosition, yPosition);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(balloon, balloonPos, Color.White);
            spriteBatch.End();
        }
    }
}