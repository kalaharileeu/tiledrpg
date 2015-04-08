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
        //public SoundEffect love;

        public SoundManager()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            phaser = content.Load<SoundEffect>("Sound/phaser");
            electro = content.Load<SoundEffect>("Sound/DST_ElectroRock");
            //love = content.Load<SoundEffect>("Sound/love");
            //phaser.Play();//Needed to install openAL here
            electro.Play();
/*
            try
            {
                // Play the music
                //MediaPlayer.Play(love);

                // Loop the currently playing song
                MediaPlayer.IsRepeating = true;
            }
            catch { }
*/
        }
    }
}
