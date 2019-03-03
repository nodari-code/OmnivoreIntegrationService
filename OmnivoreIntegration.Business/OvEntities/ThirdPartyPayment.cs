using System.ComponentModel.DataAnnotations;

namespace OmnivoreIntegration.Business
{
    /// <summary>
    /// Payment that was made from calling service.
    /// For Omnivore it is a 3rd party payment applied to a ticket that is usually associated to a Tender Type. 
    /// Amount is applied to the principal, with tip being separate.
    /// </summary>
    public class ThirdPartyPayment 
    {
        #region Private Variables

        int amount;
        int tip;

        #endregion Private Variables

        #region Cnstr

        public ThirdPartyPayment(int amount, int tip)
        {
            this.amount = amount;
            this.tip = tip;
        }

        #endregion Cnstr

        #region Public Properties

        /// <summary>
        /// Ticket unique identifier
        /// </summary>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "TicketId is invalid.")]
        public long TicketId { get; set; }

        /// <summary>
        /// Transaction unique identifier
        /// </summary>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "TransactionID value is invalid.")] 
        public long TransactionId { get; set; }

        /// <summary>
        /// POS location identifier.
        /// <c>For Omnivore it may look like i95jea7T</c>
        /// </summary>
        [Required(ErrorMessage = "Location identifier is required.")]
        [StringLength(10, ErrorMessage = "Maximum Location Identifier length is 10 symbols")]
        public string LocationIdentifier { get; set; }

        /// <summary>
        /// The amount in cents, excluding tip, to be paid.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bill amount is invalid.")]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// Amount of tips in cents.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Tip amount is invalid.")]
        public int Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        /// <summary>
        /// Tender type identifier.
        /// </summary>
        [Required (ErrorMessage ="Tender type identifier is required")]
        [StringLength(10, ErrorMessage = "Maximum Tender Type Id length is 10 symbols")]
        public string TenderTypeId { get; set; }

        /// <summary>
        /// Tender type name as a source of payment.
        /// "3rd_party" for Third Party payments in virtual POS.
        /// </summary>
        [Required]
        public string Type { get; set; }

        public int Change { get; set; }

        /// <summary>
        /// The comment attached to the payment.
        /// </summary>
        [StringLength(255, ErrorMessage = "Maximum comment length is 255 symbols")]
        public string Comment { get; set; }

        #endregion Public Properties
    }
}
