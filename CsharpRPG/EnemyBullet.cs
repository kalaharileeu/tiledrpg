using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    class EnemyBullet : Enemy
    {
        public bool isDead = false;

        public EnemyBullet(int enemy_x, int enemy_y)
        {
            type = "enemybullet";
            x = enemy_x;
            y = enemy_y;
            LoadContent();
        }

        public override void LoadContent()
        {
            image.Position.X = x;
            image.Position.Y = y;
            image.height = 11;
            image.width = 11;
            image.source = "Gameplay/Enemy/bullet2";
            image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            image.Position.X -= 2;
            if (image.Position.X < 10)
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
