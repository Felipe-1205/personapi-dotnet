using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models;

public partial class Profesion
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Des { get; set; }

    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();
}
