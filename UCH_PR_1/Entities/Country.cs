using System;
using System.Collections.Generic;

namespace UCH_PR_1.Entities;

public partial class Country
{
    public int Code { get; set; }

    public string CharCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EnglishName { get; set; } = null!;
}
