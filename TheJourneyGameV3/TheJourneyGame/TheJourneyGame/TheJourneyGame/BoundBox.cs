using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheJourneyGame
{
    class BoundBox
    {
        /// <summary>
        /// Met deze classe maken we een bounding box aan voor elke object/tile, Deze hebben een top/left/right/bottom
        /// </summary>
        #region Variables

        private float top, bottom, left, right;

        #endregion

        #region Properties
        // we willen alleen opvragen en niet setten
        public float Top { get { return top; }  }
        public float Bottom {  get { return bottom; } }
        public float Left { get { return left; } }
        public float Right { get { return right; } }

        #endregion

        #region Constructor

        public BoundBox(float x, float y, float width, float height)
        {
            left = x;
            right = x + width;
            top = y;
            bottom = y + height;
        }

        #endregion 

        #region Methods

        // als ze met in aanraking komen
        public bool Intersects(BoundBox b)
        {
            if (right <= b.Left || left >= b.Right || top >= b.Bottom || bottom <= b.Top)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
