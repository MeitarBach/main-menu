using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        private readonly List<MenuItem> r_SubMenuItems;
        private string m_Title;

        public MenuItem(MenuItem i_Parent, string i_Title)
        {
            r_SubMenuItems = new List<MenuItem>();
            m_Title = i_Title;
            r_SubMenuItems.Add(i_Parent); // The first item in the list points to the parent MenuItem
            if (i_Parent != null) // Not the Main Menu
            {
                i_Parent.AddSubItem(this); // Insert this item into the parent's sub items list 
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }

            set
            {
                m_Title = value;
            }
        }

        public MenuItem Parent
        {
            get
            {
                return r_SubMenuItems[0];
            }
        }

        public void AddSubItem(MenuItem i_Item)
        {
            r_SubMenuItems.Add(i_Item);
        }

        public void Show()
        {
            Console.Clear();
            StringBuilder menuItemStringBuilder = new StringBuilder();
            menuItemStringBuilder.Append(string.Format("{0}{1}=============={1}", m_Title, Environment.NewLine));
            for (int i = 1; i < r_SubMenuItems.Count; i++)
            {
                menuItemStringBuilder.Append(string.Format("{0}. {1}{2}", i, r_SubMenuItems[i].Title, Environment.NewLine));
            }

            if (this is MainMenu)
            {
                menuItemStringBuilder.Append("0. Exit");
            }
            else
            {
                menuItemStringBuilder.Append("0. Back");
            }

            Console.WriteLine(menuItemStringBuilder.ToString());
            chooseOption();
        }

        private void chooseOption()
        {
            int chosenOption;
            int numOfOptions = r_SubMenuItems.Count - 1;
            string exitOrBack = this is MainMenu ? "Exit" : "go Back";

            const bool v_InvalidInput = true;
            while (v_InvalidInput)
            {
                Console.WriteLine("Please enter your choice (1-{0} or 0 to {1})", numOfOptions, exitOrBack);
                if (int.TryParse(Console.ReadLine(), out chosenOption) && validateChoice(chosenOption))
                {
                    break;
                }

                Console.WriteLine("Invalid input");
            }

            MenuItem chosenItem = r_SubMenuItems[chosenOption];
            processChosenItem(chosenItem);
        }

        private void processChosenItem(MenuItem i_ChosenItem)
        {
            if(i_ChosenItem is ActionItem)
            {
                (i_ChosenItem as ActionItem).ActionChosen();
            }
            else
            {
                if(i_ChosenItem != null)
                {
                    i_ChosenItem.Show();
                }
            }
        }

        private bool validateChoice(int i_ChosenOption)
        {
            return i_ChosenOption >= 0 && i_ChosenOption <= r_SubMenuItems.Count - 1;
        }
    }
}
