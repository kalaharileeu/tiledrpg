using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace CsharpRPG
{
    class SoundManager
    {
        ContentManager content;
        public SoundEffect phaser;
        public SoundEffect electro;

        private static SoundManager instance;

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundManager();
                return instance;
            }
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            phaser = content.Load<SoundEffect>("Sound/phaser");
            electro = content.Load<SoundEffect>("Sound/DST_ElectroRock");
        }

        public void PlayPhaser()
        {
            phaser.Play();
        }

        public void PlayMusic()
        {
            electro.Play();
        }
    }
}
