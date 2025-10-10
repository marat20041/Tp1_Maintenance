public class ReceptionistConfig
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required string Phone { get; init; }
    public int Income { get; init; }
    public string Role { get; init; } = "Receptionist";
}