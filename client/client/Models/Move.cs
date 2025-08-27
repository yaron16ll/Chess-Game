using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Models
{
    public class Move
    {

        public int MoveId { get; set; } // Primary Key
        public int GameId { get; set; } // Foreign Key
        public int FromX { get; set; }
        public int FromY { get; set; }
        public int ToX { get; set; }
        public int ToY { get; set; }
        public string PieceType { get; set; }

    }
}
