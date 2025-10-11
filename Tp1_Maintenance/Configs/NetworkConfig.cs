public class HelperConfig
{
    public int MinDelay { get; set; }
    public int MaxDelay { get; set; }
    public int MinPhoneLength { get; set; }
    public int MaxPhoneLength { get; set; }
    public required string PhonePattern { get; set; }
    public int DefaultIncomePrincipal { get; set; }
    public int DefaultIncomeReceptionist { get; set; }
    public int DefaultIncomeTeacher { get; set; }
}
