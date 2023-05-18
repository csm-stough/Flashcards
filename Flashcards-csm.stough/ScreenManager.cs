using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Flashcards_csm.stough
{
    internal class ScreenManager
    {
        public static void SetScreen(Screen screen, params object[]? p)
        {
            Application.Top.RemoveAll();
            screen.Init(p);
            Application.Run();
        }

    }
}
