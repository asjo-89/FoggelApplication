using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class MonthIdToMonthName
{
    public int Id { get; set; }

    public byte MonthId { get; set; }

    public string MonthName { get; set; } = null!;
}
