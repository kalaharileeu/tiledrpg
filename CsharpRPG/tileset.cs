using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace CsharpRPG
{
    public class tileset
    {
        [XmlAttribute]
        public int tilewidth;
        [XmlAttribute]
        public int tileheight;
        [XmlAttribute]
        public int spacing;
        [XmlAttribute]
        public int margin;
        public Image image;

        tileset()
        {
            image = new Image();
        }

        public Image getimage
        {
            get { return image; }
        }
    }
}
