using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Test
{
    public class Program : IChoosableObserver
    {
        
        public static void Main()
        {
            //MainMenu currentMenuItem = new MainMenu();   
            //currentMenuItem.Show();

            new Program().test2();
        }

        public void UserChoseOption(MenuItem i_MenuItem, int i_ChosenOption)
        {
            i_MenuItem.GetItemFromList(i_ChosenOption).Show();
        }


        public static void test1()
        {
            MenuItem item = new MenuItem(null);
            item.Title = "Main Menu";

            MenuItem item2 = new MenuItem(item);
            MenuItem item3 = new MenuItem(item);
            item2.Title = "Sub Item 1";
            item3.Title = "Sub Item 2";

            item.AddSubItem(item2);
            item.AddSubItem(item3);

            item.Show();
        }
        
        private void test2()
        {
            MainMenu item = new MainMenu();
            item.Title = "Main Menu";

            MenuItem item2 = new MenuItem(item);
            MenuItem item3 = new MenuItem(item);
            item2.Title = "Sub Item 1";
            item3.Title = "Sub Item 2";

            item.AddSubItem(item2);
            item.AddSubItem(item3);

            item.AttachObserver(this);

            item.Show();
        }
    }
}
