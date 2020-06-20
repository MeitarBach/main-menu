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

        public MenuItem(MenuItem i_Parent)
        {
            r_MenuItems = new List<MenuItem>();
            r_MenuItems.Add(i_Parent);
        }

        private readonly List<IChoosableObserver> r_ChosenOptionObservers = new List<IChoosableObserver>();

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

        public MenuItem GetItemFromList(int i_ItemIndex)
        {
            return r_MenuItems[i_ItemIndex];
        }

        public void Show()
        {
            StringBuilder menuItemStringBuilder = new StringBuilder();
            menuItemStringBuilder.Append(string.Format("{0}{1}=============={1}", m_Title, Environment.NewLine));
            foreach(MenuItem item in r_MenuItems)
            {
                if(item != null)
                {
                    menuItemStringBuilder.Append(string.Format("{0}{1}", item.Title, Environment.NewLine));
                }
            }

            Console.WriteLine(menuItemStringBuilder.ToString());
            chooseOption();
        }

        private int chooseOption()
        {
            int chosenOption;
            int numOfOptions = r_MenuItems.Count;
            string exitOrBack = this is MainMenu ? "Exit" : "Back";

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

            notifyOptionCohsenObservers(chosenOption);

            return chosenOption;
        }

        private void notifyOptionCohsenObservers(int i_ChosenOption)
        {
            foreach(IChoosableObserver observer in r_ChosenOptionObservers)
            {
                observer.UserChoseOption(this, i_ChosenOption);
            }
        }

        private bool validateChoice(int i_ChosenOption)
        {
            return i_ChosenOption >= 0 && i_ChosenOption <= r_MenuItems.Count;
        }
    }
}
