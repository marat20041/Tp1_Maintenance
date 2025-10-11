public class HelperConfig
{
    public int MinDelay { get; set; }
    public int MaxDelay { get; set; }
    public int MinPhoneLength { get; set; }
    public int MaxPhoneLength { get; set; }
    public required string PhonePattern { get; set; }
    public int DefaultIncome { get; set; }
}
