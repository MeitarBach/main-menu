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
        private readonly List<IChoosableObserver> r_ChosenOptionObservers = new List<IChoosableObserver>();

        public MenuItem(MenuItem i_Parent, string i_Title)
        {
            r_MenuItems = new List<MenuItem>();
            m_Title = i_Title;
            r_MenuItems.Add(i_Parent);

            if(i_Parent != null)
            {
                r_ChosenOptionObservers = i_Parent.ChosenOptionsObservers;
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

        public List<IChoosableObserver> ChosenOptionsObservers
        {
            get
            {
                return r_ChosenOptionObservers;
            }
        }

        public MenuItem Parent
        {
            get
            {
                return r_MenuItems[0];
            }
        }

        public void AttachObserver(IChoosableObserver i_OptionChosenObserver)
        {
            r_ChosenOptionObservers.Add(i_OptionChosenObserver);
        }

        public void DetachObserver(IChoosableObserver i_SicknessObserver)
        {
            r_ChosenOptionObservers.Remove(i_SicknessObserver);
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
            int chosenItem;
            int numOfOptions = r_MenuItems.Count - 1;
            string exitOrBack = this is MainMenu ? "Exit" : "go Back";

            const bool v_InvalidInput = true;
            while(v_InvalidInput)
            {
                Console.WriteLine("Please enter your choice (1-{0} or 0 to {1})", numOfOptions, exitOrBack);
                if (int.TryParse(Console.ReadLine(), out chosenItem) && validateChoice(chosenItem))
                {
                    break;
                }

                Console.WriteLine("Invalid input");
            }

            notifyOptionChosenObservers(chosenItem);
        }

        private void notifyOptionChosenObservers(int i_ChosenOption)
        {
            foreach(IChoosableObserver observer in r_ChosenOptionObservers)
            {
                observer.UserChoseOption(r_MenuItems[i_ChosenOption]);
            }
        }

        private bool validateChoice(int i_ChosenOption)
        {
            return i_ChosenOption >= 0 && i_ChosenOption <= r_MenuItems.Count - 1;
        }
    }
}
