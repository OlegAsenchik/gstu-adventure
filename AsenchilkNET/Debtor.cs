using System;

public class Debtor
{
    public int DebtorID { get; set; } // Primary Key
    public int ListenerID { get; set; } // Foreign Key (One-to-One)
    public decimal DebtAmount { get; set; }
    public DateTime LastPaymentDate { get; set; }

    // Navigation Property
    public Listener Listener { get; set; }
}
