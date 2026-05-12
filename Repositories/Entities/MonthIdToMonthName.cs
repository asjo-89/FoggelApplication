using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Table("MonthIdToMonthName")]
[Index("MonthId", Name = "UX_MonthIdToMonthName_MonthId", IsUnique = true)]
public partial class MonthIdToMonthName
{
    [Key]
    public int Id { get; set; }

    public byte MonthId { get; set; }

    [StringLength(20)]
    public string MonthName { get; set; } = null!;
}
