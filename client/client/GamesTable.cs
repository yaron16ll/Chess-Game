using client;
using System.Collections.Generic;
using System;

public partial class GamesTable
{
    public GamesTable()
    {
        this.MovesTable = new HashSet<MovesTable>();
    }

    public int Id { get; set; }
    public int Length { get; set; }
    public DateTime StartDate { get; set; }

    public virtual ICollection<MovesTable> MovesTable { get; set; }
}
