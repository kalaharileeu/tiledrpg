using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    class PlayerBullet : Enemy
    {
        public bool isDead = false;

        public PlayerBullet(int player_x, int player_y)
        {
            type = "bullet";
            x = player_x;
            y = player_y;
            //image = new Image();
            LoadContent();
            //numFrames = 1;
        }

        public override void LoadContent()
        {
            image.Position.X = x;
            image.Position.Y = y;
            image.height = 11;
            image.width = 11;
            image.source = "Gameplay/Bullet/bullet1";
            image.LoadContent();
        }

        public void UnLoadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            image.Position.X += 3;
            if (image.Position.X > 600)
            {
                isDead = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
