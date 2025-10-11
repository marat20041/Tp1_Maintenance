public  class UndoPay
{
    public  Action Undo { get; }
    public int Payment { get; }

    public  UndoPay(int payement, Action undo)
    {
        Payment = payement;
        Undo = undo;
    }
}