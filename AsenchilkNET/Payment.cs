using System;

public class Payment
{
    public int PaymentID { get; set; } // Primary Key
    public int ListenerID { get; set; } // Foreign Key
    public DateTime PaymentDate { get; set; }
    public string PaymentPurpose { get; set; }
    public decimal Amount { get; set; }

    // Navigation Property
    public Listener Listener { get; set; }
}
