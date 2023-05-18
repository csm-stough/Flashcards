using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough.DTOs
{
    public class Flashcard
    {

        public readonly int Id;
        public string Question;
        public string Answer;
        public DTOs.Stack stack;

        public Flashcard(int id, string question, string answer, Stack stack)
        {
            Id = id;
            Question = question;
            Answer = answer;
            this.stack = stack;
        }
    }
}
