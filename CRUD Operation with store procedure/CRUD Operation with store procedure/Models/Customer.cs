using System;
using System.Collections.Generic;

namespace CRUD_Operation_with_store_procedure.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;
}
