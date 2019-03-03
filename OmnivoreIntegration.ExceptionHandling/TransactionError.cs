namespace OmnivoreIntegration.ExceptionHandling
{
    public class TransactionError
    {
        public long TransactionID { get; set; }

        public bool Sucess { get { return false; } }

        public string ErrorMessage { get; set; }
    }
}
