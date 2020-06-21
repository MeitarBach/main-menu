using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Test
{
    public class TestMenusInterfaces : IChosenActionObserver
    {
        private readonly MainMenu r_MainMenu;

        public TestMenusInterfaces()
        {
            r_MainMenu = buildMenu();
        }

        public MainMenu MainMenu
        {
            get
            {
                return r_MainMenu;
            }
        }

        private MainMenu buildMenu()
        {
            // level 1 - Main Menu
            MainMenu mainMenu = new MainMenu("Main Menu");

            // level 2 - Menu Items
            MenuItem versionsAndCapitals = new MenuItem(mainMenu, "Version and Capitals");
            MenuItem showDateOrTime = new MenuItem(mainMenu, "Show Date/Time");

            // level 3 - Action Items
            ActionItem countCapitals = new ActionItem(versionsAndCapitals, "Count Capitals");
            ActionItem showVersion = new ActionItem(versionsAndCapitals, "Show Version");
            ActionItem showTime = new ActionItem(showDateOrTime, "Show Time");
            ActionItem showDate = new ActionItem(showDateOrTime, "Show Date");
            countCapitals.AttachObserver(this);
            showVersion.AttachObserver(this);
            showTime.AttachObserver(this);
            showDate.AttachObserver(this);

            return mainMenu;
        }
        
        public void UserChoseAction(ActionItem i_ChosenAction)
        {
            act(i_ChosenAction.Title);
        }

        private void act(string i_ActionTitle)
        {
            Console.Clear();

            switch (i_ActionTitle)
            {
                case "Count Capitals":
                    countCapitals();
                    break;
                case "Show Version":
                    showVersion();
                    break;
                case "Show Time":
                    showTime();
                    break;
                case "Show Date":
                    showDate();
                    break;
            }

            Console.WriteLine("Press enter to go back to the previous menu...");
            Console.ReadLine();
        }

        private void countCapitals()
        {
            Console.WriteLine("Enter a sentence");
            string sentence = Console.ReadLine();
            int capitalsCount = 0;
            foreach (char character in sentence)
            {
                if (char.IsUpper(character))
                {
                    capitalsCount++;
                }
            }

            Console.WriteLine("There are {0} capital letters in your string!", capitalsCount);
        }

        private void showVersion()
        {
            Console.WriteLine("Version: 20.2.4.30620");
        }

        private void showTime()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

        private void showDate()
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}
