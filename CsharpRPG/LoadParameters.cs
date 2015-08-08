using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace CsharpRPG
{
    public class Loader
    {
        public class parameter
        {
            public string type;
            public string imagesource;
            public string effects;
            public int life;//number of frames to animate till death of animation
            public int numframesX;
            public int numframesY;
            public int movespeed;

            public string Imagesource
            {
                get { return imagesource; }
            }

            string Type
            {
                get { return type; }
            }

            public string Effects
            {
                get { return effects; }
            }

            public int Movespeed
            {
                get { return movespeed; }
            }
        }

        [XmlElement("parameter")]
        public List<parameter> loadparameters;

        public List<parameter> Loadedparamaters
        {
            get { return loadparameters; }  
        }

        public Loader()
        {
            loadparameters = new List<parameter>();
        }
    }
}
