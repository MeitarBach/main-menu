using System;

namespace Ex04.Menus.Delegates
{
    public class ActionItem : MenuItem
    {
        public event Action ItemChosen;

        public ActionItem(MenuItem i_Parent, string i_Title) : base(i_Parent, i_Title)
        {
        }

        internal void ActionChosen()
        {
            Console.Clear();
            OnActionChosen();
            Console.WriteLine("Press enter to go back to the previous menu...");
            Console.ReadLine();
            this.Parent.Show();
        }

        protected virtual void OnActionChosen()
        {
            if(ItemChosen != null)
            {
                ItemChosen.Invoke();
            }
        }
    }
}
