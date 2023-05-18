using Flashcards_csm.stough.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Flashcards_csm.stough.Screens
{
    public class StacksScreen : Screen
    {
        private int stackFrameWidth;
        private int stackFrameHeight;
        private int addStackButtonWidth;
        private int addStackButtonHeight;

        public StacksScreen()
        {
            stackFrameWidth = 33;
            stackFrameHeight = 80;
            addStackButtonWidth = 30;
            addStackButtonHeight = 99 - stackFrameHeight;
        }

        public override void Init(params object[]? p)
        {
            FrameView stacksFrame = null;
            ListView stacksList = null;
            FrameView flashcardsView = null;
            TableView flashcardsTable = null;

            stacksFrame = new FrameView("Stacks")
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(stackFrameWidth),
                Height = Dim.Percent(stackFrameHeight),
            };

            stacksList = new ListView(StacksAccessor.Get())
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            stacksList.OpenSelectedItem += (stack) =>
            {
                flashcardsView.RemoveAll();
                flashcardsTable = new TableView(UIFactory.CreateFlashcardsDataTable(stack.Value as Stack))
                {
                    X = 0,
                    Y = 0,
                    Width = Dim.Fill(),
                    Height = Dim.Fill(),
                    
                };
                flashcardsView.Add(flashcardsTable);
            };

            ColorScheme buttonScheme = new ColorScheme();
            buttonScheme.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);
            buttonScheme.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);
            buttonScheme.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Green);

            Button addStackButton = new Button("New Stack")
            {
                X = Pos.Percent((stackFrameWidth - addStackButtonWidth) / 2f),
                Y = Pos.Percent(stackFrameHeight),
                Width = Dim.Percent(addStackButtonWidth),
                Height = Dim.Percent(addStackButtonHeight),
                ColorScheme = buttonScheme,
            };

            flashcardsView = new FrameView("Flashcards")
            {
                X = Pos.Percent(stackFrameWidth),
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };



            

            stacksFrame.Add(stacksList);
            Application.Top.Add(stacksFrame);
            Application.Top.Add(addStackButton);
            Application.Top.Add(flashcardsView);
        }
    }
}
