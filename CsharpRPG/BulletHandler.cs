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

        private static BulletHandler instance;

        public static BulletHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new BulletHandler();
                return instance;
            }
        }

        public BulletHandler()
        {
            listplayerbullets = new List<PlayerBullet>();
            listenemybullets = new List<EnemyBullet>();
        }

        public static List<PlayerBullet> Listplayerbullets
        {
            get { return listplayerbullets; }
        }

        public void addEnemyBullet(int x, int y)
        {
            listenemybullets.Add(new EnemyBullet(x, y));
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
            listplayerbullets.RemoveAll(PlayerBullet => PlayerBullet.isDead == true);
            listenemybullets.RemoveAll(EnemyBullet => EnemyBullet.isDead == true);

            foreach (PlayerBullet pb in listplayerbullets)
                pb.Update(gametime);

            foreach (EnemyBullet eb in listenemybullets)
                eb.Update(gametime);
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
