using System;
using Ex04.Menus.Delegates;

namespace Ex04.Test
{
    public class TestMenusDelegates
    {
        private readonly MainMenu r_MainMenu;

        public TestMenusDelegates()
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
            countCapitals.ItemChosen += countCapitals_Chosen;
            showVersion.ItemChosen += showVersion_Chosen;
            showTime.ItemChosen += showTime_Chosen;
            showDate.ItemChosen += showDate_Chosen;

            return mainMenu;
        }

        private void countCapitals_Chosen()
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

        private void showVersion_Chosen()
        {
            Console.WriteLine("Version: 20.2.4.30620");
        }

        private void showTime_Chosen()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

        private void showDate_Chosen()
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}
