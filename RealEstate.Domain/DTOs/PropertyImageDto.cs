namespace RealEstate.Domain.DTOs;

public class PropertyImageDto
{
    public string IdPropertyImage { get; set; } = string.Empty;
    public string IdProperty { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}

public class CreatePropertyImageDto
{
    public string IdProperty { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
}

public class UpdatePropertyImageDto
{
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
