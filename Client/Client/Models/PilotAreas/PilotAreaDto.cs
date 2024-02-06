namespace Client.Models.PilotAreas;

public class PilotAreaDto
{
    public string Name { get; set; }
    public ICollection<string> Unlocodes { get; set; }
}


