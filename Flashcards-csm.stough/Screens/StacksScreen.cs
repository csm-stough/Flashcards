using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Flashcards_csm.stough.Screens
{
    internal class StacksScreen : Screen
    {
        public override void Init(params object[]? p)
        {
            ListView stackList = new ListView(StacksAccessor.Get())
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(33),
                Height = Dim.Fill(),
            };

            Application.Top.Add(stackList);
        }
    }
}
