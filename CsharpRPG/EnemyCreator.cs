using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace CsharpRPG
{
    public class objectgroup
    {
        [XmlElement("object")]
        public List<Enemy> enemies;

        objectgroup()
        {
           enemies = new List<Enemy>();
        }

        public void removeenemy(int myindex)
        {
            if (enemies[myindex] != null)
                enemies.RemoveAt(myindex);
        }

        public List<Enemy> Enemies
        { 
            get
            {
                return enemies;
            }
        }

    }
}
