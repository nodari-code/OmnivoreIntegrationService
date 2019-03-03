using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

namespace OmnivoreIntegration.Business
{
    public class PaymentRequest
    {
        /// <summary>
        /// Payment transaction unique identifier
        /// </summary>
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "TransactionID value is invalid.")]
        public long TransactionID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "BillNo value is invalid.")]
        public int BillNo { get; set; }

        public List<POSDepartment> POSDepartments { get; set; }

        [Integer]
        public int POSPaymentID { get; set; }

        /// <summary>
        /// The amount in cents, excluding tip, to be paid.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bill amount is invalid.")]
        public int BillAmount { get; set; }

        [Integer]
        public int PortionAmount { get; set; }

        /// <summary>
        /// Amount of tips in cents.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Tip amount is invalid.")]
        public int TipAmount { get; set; }

        [Integer]
        public int GlobalMerchantLocationID { get; set; }
        
        /// <summary>
        /// POS location identifier.
        /// <c>For Omnivore it may look like i95jea7T</c>
        /// </summary>
        [Required (ErrorMessage ="Location identifier is required.")]
        public string LocationIdentifier { get; set; }

        [Required(ErrorMessage = "Payment type identifier is required.")]
        public string PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Payment type name is required.")]
        public string PaymentTypeName { get; set; }
    }
}
