using ConsoleUtilities;
using Flashcards_csm.stough.DTOs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    internal class UIFactory
    {
        public static Panel CreateFlashcardDetailsPanel(Flashcard flashcard)
        {
            return new Panel(flashcard.Question + '\n' + flashcard.Answer);
        }

        public static void BetterMenu(string Title, List<MenuOption> options)
        {
            SelectionPrompt<MenuOption> menu = new SelectionPrompt<MenuOption>()
            {
                Title = Title,
                Converter = new Func<MenuOption, string>(option => option.Text),
                WrapAround = true,
            };

            options.ForEach(option => { menu.AddChoice(option); });

            AnsiConsole.Prompt(menu).action();
        }

        public static void YesNo(string prompt, Action yes, Action no)
        {
            BetterMenu(prompt, new List<MenuOption>()
            {
                new MenuOption("Yes", yes),
                new MenuOption("No", no)
            });
        }

        public static Table GetFlashcardsTable(Stack stack)
        {
            List<Flashcard> flashcards = FlashcardsAccessor.Get(stack);
            Table flashcardsTable = new Table();

            flashcardsTable.AddColumn(new TableColumn("Question").Centered());
            flashcardsTable.AddColumn(new TableColumn("Answer").Centered());
            flashcards.ForEach(flashcard =>
            {
                flashcardsTable.AddRow(new string[] { flashcard.Question, flashcard.Answer });
            });

            flashcardsTable.Centered();

            return flashcardsTable;
        }
    }
}
