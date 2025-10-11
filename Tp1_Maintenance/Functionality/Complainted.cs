
using SchoolManager;
using System;
public class Complaint : EventArgs
{

    public DateTime ComplaintTime { get; set; }
    public string? ComplaintRaised { get; set; }
    public string? ComplaintText { get; set; }



}