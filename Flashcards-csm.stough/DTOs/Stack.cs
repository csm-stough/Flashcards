using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards_csm.stough.DTOs
{
    public class Stack
    {
        public readonly int Id;
        public string Name;

        public Stack(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
