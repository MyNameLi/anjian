using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dropthings.Data
{
    public partial class PageEntity
    {
        #region Properties

        public string UserTabName
        {
            get
            {
                return this.TITLE.Replace(' ', '_');
            }
        }

        public string LockedTabName
        {
            get
            {
                return this.TITLE.Replace(' ', '_') + "_Locked";
            }
        }

        #endregion Properties

        #region Methods

        public static int[] GetColumnWidths(int layoutType)
        {
            int[] columnWidths;
            if (layoutType == 1)
                columnWidths = new int[] { 32, 32, 33 };
            else if (layoutType == 2)
                columnWidths = new int[] { 24, 75 };
            else if (layoutType == 3)
                columnWidths = new int[] { 75, 24 };
            else if (layoutType == 4)
                columnWidths = new int[] { 100 };
            else //if (layoutType == 5)
                columnWidths = new int[] { 49, 50 };

            return columnWidths;
        }


        #endregion Methods
    }
}
