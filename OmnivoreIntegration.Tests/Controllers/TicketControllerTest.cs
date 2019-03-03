using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OmnivoreIntegration.Business;
using OmnivoreIntegration.Service;
using OmnivoreIntegration.ExceptionHandling;

namespace OmnivoreIntegration.Tests
{
    [TestClass]
    public class TicketControllerTest
    {
        [TestMethod]
        [ExpectedException (typeof(BadRequestException), "PaymentRequest is invalid")]
        public void TicketController_Throws_BadRequestException_When_Payment_Is_Missed()
        {
            // Arrange
            TicketRepository ticketRepository = new TicketRepository(null);
            PaymentWorker paymentWorker = new PaymentWorker(ticketRepository, null);
            TicketController controller = new TicketController(paymentWorker);
            PaymentRequest paymentRequest = new
                PaymentRequest()
            {
                
                BillNo = 123,
                PaymentTypeId = "100",                
                TipAmount = 0,
                TransactionID = 123,
                LocationIdentifier = "ABC",
                PaymentTypeName = "3rd_party",
                POSPaymentID = 123
            };

            MockControllerRequest(controller);

            // Act
            controller.Validate(paymentRequest);
            HttpResponseMessage result = controller.Post(paymentRequest) as HttpResponseMessage;
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException), "PaymentRequest is invalid")]
        public void TicketController_Throws_BadRequestException_When_Payment_Is_Zero()
        {
            // Arrange
            TicketRepository ticketRepository = new TicketRepository(null);
            PaymentWorker paymentWorker = new PaymentWorker(ticketRepository, null);
            TicketController controller = new TicketController(paymentWorker);
            PaymentRequest paymentRequest = new
                PaymentRequest()
            {
                BillNo = 123,
                BillAmount = 0,
                PaymentTypeId = "100",
                TipAmount = 0,
                TransactionID = 123,
                LocationIdentifier = "ABC",
                PaymentTypeName = "3rd_party",
                POSPaymentID = 123
            };

            MockControllerRequest(controller);

            // Act
            controller.Validate(paymentRequest);
            HttpResponseMessage result = controller.Post(paymentRequest) as HttpResponseMessage;
        }

        [TestMethod]
        //[ExpectedException(typeof(BadRequestException), "PaymentRequest is invalid")]
        public void TicketController_Returns_Error()
        {
            // Arrange
            TicketRepository ticketRepository = new TicketRepository(null);
            PaymentWorker paymentWorker = new PaymentWorker(ticketRepository, null);
            TicketController controller = new TicketController(paymentWorker);
            PaymentRequest paymentRequest = new
                PaymentRequest()
            {
                BillNo = 123,
                BillAmount = 500,
                PaymentTypeId = "100",
                TipAmount = 0,
                TransactionID = 123,
                LocationIdentifier = "ABC",
                PaymentTypeName = "3rd_party",
                POSPaymentID = 123
            };

            MockControllerRequest(controller);

            // Act
            controller.Validate(paymentRequest);
            HttpResponseMessage result = controller.Post(paymentRequest) as HttpResponseMessage;
        }

        #region Helper Methods

        private void MockControllerRequest(TicketController controller)
        {
            //Mock Http request:
            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            controller.Request.Properties["MS_HttpConfiguration"] = config;
        }

        #endregion Helper Methods
    }
}
