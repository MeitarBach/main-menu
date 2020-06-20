namespace Ex04.Menus.Interfaces
{
    public interface IChoosableObserver
    {
        void UserChoseOption(MenuItem i_ChosenItem);

        void Act(string i_ActionTitle);
    }
}
