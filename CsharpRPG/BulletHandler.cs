using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CsharpRPG
{
    class BulletHandler
    {
        static List<PlayerBullet> listplayerbullets;
        List<EnemyBullet> listenemybullets;

        public BulletHandler()
        {
            listplayerbullets = new List<PlayerBullet>();
            listenemybullets = new List<EnemyBullet>();
        }

        public static List<PlayerBullet> Listplayerbullets
        {
            get { return listplayerbullets; }
        }

        public void addPlayerBullet(int x, int y)
        {
            listplayerbullets.Add(new PlayerBullet(x, y));
        }

        public void addPlayerBullet(PlayerBullet pb)
        {
            listplayerbullets.Add(pb);
        }

        public void clearbullet()
        {

        }

        public void update(GameTime gametime)
        {
            int saveindex = 100;
            int index = 0;
            foreach (PlayerBullet pb in listplayerbullets)
            {
                pb.Update(gametime);
                if (pb.isDead == true)
                {
                    saveindex = index;
                }
                index++;
            }
            if(saveindex < 100)
                listplayerbullets.RemoveAt(saveindex);

        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach(PlayerBullet pb in listplayerbullets)
            {
                pb.Draw(spriteBatch);

            }

            foreach(EnemyBullet eb in listenemybullets)
            {
                eb.Draw(spriteBatch);
            }
        }


    }
}
