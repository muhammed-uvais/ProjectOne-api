using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class AdmnDoc
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public long PrimaryId { get; set; }

    public string Extension { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string UploadFileName { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string? Description { get; set; }

    public int? IsActive { get; set; }
}
