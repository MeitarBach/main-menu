using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class ActionItem : MenuItem
    {
        private readonly List<IChosenActionObserver> r_ChosenActionObservers;

        public ActionItem(MenuItem i_Parent, string i_Title) : base(i_Parent, i_Title)
        {
            r_ChosenActionObservers = new List<IChosenActionObserver>();
        }

        public void AttachObserver(IChosenActionObserver i_OptionChosenObserver)
        {
            r_ChosenActionObservers.Add(i_OptionChosenObserver);
        }

        public void DetachObserver(IChosenActionObserver i_SicknessObserver)
        {
            r_ChosenActionObservers.Remove(i_SicknessObserver);
        }

        internal void ActionChosen()
        {
            notifyChosenActionObservers();
            this.Parent.Show();
        }

        private void notifyChosenActionObservers()
        {
            foreach (IChosenActionObserver observer in r_ChosenActionObservers)
            {
                observer.UserChoseAction(this);
            }
        }
    }
}
