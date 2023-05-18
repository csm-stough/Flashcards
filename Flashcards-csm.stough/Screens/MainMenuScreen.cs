using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Flashcards_csm.stough.Screens
{
    public class MainMenuScreen : Screen
    {
        public override void Init(params object[]? p)
        {
            Label title = new Label(
                "    ________           __                        __    \r\n" +
                "   / ____/ /___ ______/ /_  _________ __________/ /____\r\n" +
                "  / /_  / / __ `/ ___/ __ \\/ ___/ __ `/ ___/ __  / ___/\r\n" +
                " / __/ / / /_/ (__  ) / / / /__/ /_/ / /  / /_/ (__  ) \r\n" +
                "/_/   /_/\\__,_/____/_/ /_/\\___/\\__,_/_/   \\__,_/____/  ")
            {
                X = Pos.Center(),
                Y = Pos.Percent(15)
            };

            ColorScheme buttonScheme = new ColorScheme();
            buttonScheme.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);
            buttonScheme.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);
            buttonScheme.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);

            Button stacksButton = new Button("Stacks")
            {
                X = Pos.Percent(33) - Pos.At(5),
                Y = Pos.Percent(65),
                Width = 10,
                Height = 3,
                ColorScheme = buttonScheme,
            };

            stacksButton.Clicked += () =>
            {
                ScreenManager.SetScreen(new StacksScreen());
            };

            Button studyButton = new Button("Study")
            {
                X = Pos.Percent(66) - Pos.At(5),
                Y = Pos.Percent(65),
                Width = 10,
                Height = 3,
                ColorScheme = buttonScheme,
            };

            Application.Top.Add(title);
            Application.Top.Add(stacksButton);
            Application.Top.Add(studyButton);
        }

    }
}
