namespace Abstract.Interfaces
{
    public interface ICustomer
    {

        public bool canPay { get; set; } 
        void MakePayment();
    }
}