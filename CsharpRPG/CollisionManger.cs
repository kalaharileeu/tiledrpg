using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace CsharpRPG
{
    public class CollisionManager
    {
        public void checkEnemyPlayerBulletCollision(List<Enemy> elist)
        {
            foreach (Enemy e in elist)
            {
                if (e.type != "ScrollingBackground")
                {

                    foreach (PlayerBullet pb in BulletHandler.Listplayerbullets)
                    {
                        if (Collision.RectRect(e.GetCurrentRect(), pb.GetCurrentRect()))
                        {
                            e.dead(e.GetCurrentRect());
                        }
                    }
                }
            }
        }
    }
}
