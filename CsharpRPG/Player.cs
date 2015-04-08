using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CsharpRPG
{
    public class Player
    {
        public Image image;
        public Vector2 Velocity;
        public float MoveSpeed;

        BulletHandler bulletHandler;

        public Player()
        {
            Velocity = Vector2.Zero;
            bulletHandler = new BulletHandler();
        }

        public void LoadContent()
        {
            image.LoadContent();
        }

        public void UnloadContent()
        {
            image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            image.IsActive = true;
            if (Velocity.X == 0)//If you do not want to do diagonal movement
            {
                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else if (InputManager.Instance.KeyDown(Keys.Up))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 3;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else
                    Velocity.Y = 0;
            }

            if (Velocity.Y == 0)//If you do not want to do diagonal movement
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 2;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 1;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else
                    Velocity.X = 0;
            }

            if (InputManager.Instance.KeyPressed(Keys.Space))
                bulletHandler.addPlayerBullet((int)image.Position.X, (int)image.Position.Y);


            if (Velocity.X == 0 && Velocity.Y == 0)//if the player is not moving
                image.IsActive = false;

            bulletHandler.update(gameTime);
            image.Update(gameTime);
            image.Position += Velocity;
            //image.Position.X = 620;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            bulletHandler.draw(spriteBatch);
            image.Draw(spriteBatch);
        }
 
    }
}
