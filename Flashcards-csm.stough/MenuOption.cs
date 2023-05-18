using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    public class MenuOption
    {
        public string Text;
        public Action action;

        public MenuOption(string text, Action action)
        {
            Text = text;
            this.action = action;
        }
    }
}
