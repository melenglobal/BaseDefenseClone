namespace Abstract.Interfaces
{
    public interface ICustomer
    {

        public bool canPay { get; set; } 
        int StartPayment();

        void StopPayment();
    }
}