using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        string state;

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void scroll(int x)
        {
            position.X += GameplayScreen.scrollspeed;
            //position.X += Enemy.scr;
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }

        public void UnloadContent()
        { }
        
        public void Update(GameTime gameTime, ref Player player)
        {


            if(state == "Solid")//Below is the collision handling
            {
                Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y,
                    sourceRect.Width, sourceRect.Height);
                Rectangle playerRect = new Rectangle((int)player.image.Position.X,
                    (int)player.image.Position.Y, player.image.SourceRect.Width, player.image.SourceRect.Height);

                if (playerRect.Intersects(tileRect))
                {
                    if (player.Velocity.X < 0)
                        player.image.Position.X = tileRect.Right;
                    else if (player.Velocity.X > 0)
                        player.image.Position.X = tileRect.Left - player.image.SourceRect.Width;
                    else if (player.Velocity.Y < 0)
                        player.image.Position.Y = tileRect.Bottom;
                    else
                        player.image.Position.Y = tileRect.Top - player.image.SourceRect.Height;

                    player.Velocity = Vector2.Zero;//This does not matter that much.
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
 

        }
    }

}
