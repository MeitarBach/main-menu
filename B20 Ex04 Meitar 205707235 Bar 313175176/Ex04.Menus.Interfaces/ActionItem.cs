using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class ActionItem : MenuItem
    {
        public ActionItem(MenuItem i_Parent, string i_Title) : base(i_Parent, i_Title)
        {
        }

    }
}
