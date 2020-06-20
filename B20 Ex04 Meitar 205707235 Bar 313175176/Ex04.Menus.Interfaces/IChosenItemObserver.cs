namespace Ex04.Menus.Interfaces
{
    public interface IChosenItemObserver
    {
        void UserChoseOption(MenuItem i_ChosenItem);

        void Act(string i_ActionTitle);
    }
}
