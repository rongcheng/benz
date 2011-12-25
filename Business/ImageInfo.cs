using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    public class ImageInfo:ResourceEntity
    {
        private int _width;
        private int _height;
        private string _hvsp;


        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public string Hvsp
        {
            get { return _hvsp; }
            set { _hvsp = value; }
        }


    }
}
