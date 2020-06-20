using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly List<MenuItem> r_MenuItems;
        private string m_Title;
        private readonly List<IChosenItemObserver> r_ChosenItemObservers = new List<IChosenItemObserver>();

        public MenuItem(MenuItem i_Parent, string i_Title)
        {
            r_MenuItems = new List<MenuItem>();
            m_Title = i_Title;
            r_MenuItems.Add(i_Parent);

            if(i_Parent != null)
            {
                r_ChosenItemObservers = i_Parent.ChosenItemsObservers;
                i_Parent.AddSubItem(this);
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

        public List<IChosenItemObserver> ChosenItemsObservers
        {
            get
            {
                return r_ChosenItemObservers;
            }
        }

        public MenuItem Parent
        {
            get
            {
                return r_MenuItems[0];
            }
        }

        public void AttachObserver(IChosenItemObserver i_OptionChosenObserver)
        {
            r_ChosenItemObservers.Add(i_OptionChosenObserver);
        }

        public void DetachObserver(IChosenItemObserver i_SicknessObserver)
        {
            r_ChosenItemObservers.Remove(i_SicknessObserver);
        }

        public void AddSubItem(MenuItem i_Item)
        {
            r_MenuItems.Add(i_Item);
        }

        public void Show()
        {
            Console.Clear();
            StringBuilder menuItemStringBuilder = new StringBuilder();
            menuItemStringBuilder.Append(string.Format("{0}{1}=============={1}", m_Title, Environment.NewLine));
            for (int i = 1 ; i < r_MenuItems.Count ; i++)
            {
                menuItemStringBuilder.Append(string.Format("{0}. {1}{2}", i, r_MenuItems[i].Title, Environment.NewLine));
            }

            if(this is MainMenu)
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
            int numOfOptions = r_MenuItems.Count - 1;
            string exitOrBack = this is MainMenu ? "Exit" : "go Back";

            const bool v_InvalidInput = true;
            while(v_InvalidInput)
            {
                Console.WriteLine("Please enter your choice (1-{0} or 0 to {1})", numOfOptions, exitOrBack);
                if (int.TryParse(Console.ReadLine(), out chosenOption) && validateChoice(chosenOption))
                {
                    break;
                }

                Console.WriteLine("Invalid input");
            }

            MenuItem chosenItem = r_MenuItems[chosenOption];
            notifyOptionChosenObservers(chosenItem);
        }

        private void notifyOptionChosenObservers(MenuItem i_ChosenItem)
        {
            foreach(IChosenItemObserver observer in r_ChosenItemObservers)
            {
                observer.UserChoseOption(i_ChosenItem);
            }
        }

        private bool validateChoice(int i_ChosenOption)
        {
            return i_ChosenOption >= 0 && i_ChosenOption <= r_MenuItems.Count - 1;
        }
    }
}
