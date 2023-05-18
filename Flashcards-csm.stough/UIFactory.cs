using ConsoleUtilities;
using Flashcards_csm.stough.DTOs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough
{
    internal class UIFactory
    {
        public static DataTable CreateFlashcardsDataTable(Stack stack)
        {
            DataTable table = new DataTable("Flashcards");

/*            DataColumn idColumn = new DataColumn("id", typeof(int));
            idColumn.Unique = true;*/

            DataColumn questionColumn = new DataColumn("Question", typeof(string));

            DataColumn answerColumn = new DataColumn("Answer", typeof(string));

            //table.Columns.Add(idColumn);
            table.Columns.Add(questionColumn);
            table.Columns.Add(answerColumn);

            List<Flashcard> flashcards = FlashcardsAccessor.Get(stack);

            DataSet flashcardsData = new DataSet("flashcards");

            flashcards.ForEach(flashcard =>
            {
                DataRow row = table.NewRow();
                row["Question"] = flashcard.Question;
                row["Answer"] = flashcard.Answer;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}
