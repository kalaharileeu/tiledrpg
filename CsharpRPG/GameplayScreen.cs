using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        map map1;
        Loader loadedparams;
        public static int scrollspeed = 1;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Loader> paramloader = new XmlManager<Loader>();
            XmlManager<Player> playerLoader = new XmlManager<Player>();//Player is the class name sent to Xml manager
            XmlManager<map> mapLoader = new XmlManager<map>();
            loadedparams = paramloader.Load("Content/Gameplay/Explosion/parameters.xml");
            player = playerLoader.Load("Content/Gameplay/Player.xml");//player loader is the xml manager instance
            map1 = mapLoader.Load("Content/Gameplay/Map/newscene.xml");

            player.LoadContent();
            map1.LoadContent();
            LoadMapAnimatedGraphics();
        }

        void LoadMapAnimatedGraphics()
        {
            foreach (Loader.parameter v in loadedparams.Loadedparamaters)
            {
                try
                {
                    map1.Parameterdictionary.Add(v.type, v);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("An element with Key = \"v.type\" already exists.");
                }
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map1.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            player.Update(gameTime);//update player before map because to map update stuff dependent on player
            map1.Update(gameTime, player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map1.Draw(spriteBatch, "Underlay");//Draw map befor the player so that player is ontop
            map1.Draw(spriteBatch, "Overlay");
            if(!player.Dying)
                player.Draw(spriteBatch);
        }

    }
}
