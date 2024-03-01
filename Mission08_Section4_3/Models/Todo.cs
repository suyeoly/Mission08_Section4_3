using System;
using System.Collections.Generic;

namespace Mission08_Section4_3.Models;

public partial class Todo
{
    public int TaskId { get; set; }

    public string Task { get; set; } = null!; // "= null!" This randomly created when I scaffolded the database. I am not sure we should leave it or not

    public string? DueDate { get; set; }

    public string Quadrant { get; set; } = null!;

    public string? Category { get; set; }

    public int Completed { get; set; }
}
