using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.ModelBinding;

namespace OmnivoreIntegration.ExceptionHandling
{
    public static class ErrorExtensions
    {
        #region Error Slags

        private enum Error4xxSlugs
        {
            bad_query,
            bug,
            call_not_implemented,
            call_not_supported,
            cant_void_sent,
            cant_void_ticket,
            cant_void_voided,
            card_already_tendered,
            card_balance_insufficient,
            card_invalid,
            card_rejected,
            conflict,
            employee_clocked_out,
            employee_locked,
            entry_inapplicable,
            entry_is_open,
            entry_not_open,
            entry_unavailable,
            excessive_amount,
            failed_after_success,
            insufficient_amount,
            invalid_api_key,
            invalid_input,
            invalid_payload,
            mag_card_invalid,
            mag_card_required,
            malformed_header,
            no_content_type,
            not_found,
            out_of_stock,
            param_not_implemented,
            param_not_supported,
            parameter_required,
            parse_error,
            prerequisites_failed,
            readonly_installation,
            reference_not_found,
            reference_required,
            required_modifiers_missing,
            table_unavailable,
            ticket_closed,
            ticket_locked,
            ticket_requirements_not_met,
            tips_not_allowed,
            too_many_discounts,
            unsupported_type
        }

        private enum Error5xxSlugs
        {
            agent_config_error,
            agent_offline,
            cache_still_loading,
            internal_error,
            invalid_store,
            pos_component_missing,
            pos_config_error,
            pos_failure,
            pos_not_responding,
            pos_not_responding_retry,
            pos_offline,
            timeout,
            unknown
        }

        #endregion Error Slags

        private static readonly Dictionary<string, HttpStatusCode> errorSlugs = new Dictionary<string, HttpStatusCode>();

        static ErrorExtensions()
        {
            //4xx Error Slugs
            errorSlugs.Add(Error4xxSlugs.bad_query.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.bug.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.call_not_implemented.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.call_not_supported.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.conflict.ToString(), HttpStatusCode.InternalServerError);
            errorSlugs.Add(Error4xxSlugs.excessive_amount.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.failed_after_success.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.insufficient_amount.ToString(), HttpStatusCode.PaymentRequired);
            errorSlugs.Add(Error4xxSlugs.invalid_api_key.ToString(), HttpStatusCode.Unauthorized);
            errorSlugs.Add(Error4xxSlugs.invalid_input.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.invalid_payload.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.malformed_header.ToString(), HttpStatusCode.InternalServerError);
            errorSlugs.Add(Error4xxSlugs.no_content_type.ToString(), HttpStatusCode.InternalServerError);
            errorSlugs.Add(Error4xxSlugs.not_found.ToString(), HttpStatusCode.NotFound);
            errorSlugs.Add(Error4xxSlugs.param_not_implemented.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.param_not_supported.ToString(), HttpStatusCode.MethodNotAllowed);
            errorSlugs.Add(Error4xxSlugs.parse_error.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.readonly_installation.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.reference_not_found.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.reference_required.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.ticket_closed.ToString(), HttpStatusCode.BadRequest);
            errorSlugs.Add(Error4xxSlugs.ticket_locked.ToString(), HttpStatusCode.Forbidden);
            errorSlugs.Add(Error4xxSlugs.ticket_requirements_not_met.ToString(), HttpStatusCode.Forbidden);
            errorSlugs.Add(Error4xxSlugs.tips_not_allowed.ToString(), HttpStatusCode.Forbidden);
            errorSlugs.Add(Error4xxSlugs.unsupported_type.ToString(), HttpStatusCode.InternalServerError);
            //5xx Error Slugs
            errorSlugs.Add(Error5xxSlugs.agent_config_error.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.agent_offline.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.cache_still_loading.ToString(), HttpStatusCode.ServiceUnavailable);
            errorSlugs.Add(Error5xxSlugs.internal_error.ToString(), HttpStatusCode.ServiceUnavailable);
            errorSlugs.Add(Error5xxSlugs.invalid_store.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_component_missing.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_config_error.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_failure.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_not_responding.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_not_responding_retry.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.pos_offline.ToString(), HttpStatusCode.BadGateway);
            errorSlugs.Add(Error5xxSlugs.timeout.ToString(), HttpStatusCode.GatewayTimeout);
            errorSlugs.Add(Error5xxSlugs.unknown.ToString(), HttpStatusCode.BadGateway);
        }

        public static TransactionError ToTransactionError(this string message)
        {
            long transactionId = 0;
            var responseError = new TransactionError();
            string[] data = message.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length == 3)
            {
                responseError.ErrorMessage = string.Format("{0}. {1}", data[0], data[1]);
                long.TryParse( data[2], out transactionId);
                responseError.TransactionID = transactionId;
            }
            else
            {
                responseError.ErrorMessage = message;
            }

            return responseError;
        }

        public static string FirstErrorMessage(this ModelStateDictionary modelState)
        {
            string errorMessage = string.Empty;

            if (modelState == null || modelState.Values.Count <= 0) return errorMessage;
            foreach (ModelState state in modelState.Values)
            {
                foreach (ModelError error in state.Errors)
                {
                    if(!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        errorMessage = error.ErrorMessage;
                        break;
                    }
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// returns error message together with transaction identifier.
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <param name="transactionId">Uniue identifie of payment transaction</param>
        /// <returns></returns>
        public static string ToCombinedErrorMessage(this string errorMessage, long transactionId)
        {
            if (string.IsNullOrEmpty(errorMessage)) return transactionId.ToString();
            return errorMessage + "::" + transactionId.ToString();
        }

        public static HttpStatusCode ToHttpStatusCode(this string slugError)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if(!string.IsNullOrEmpty(slugError) && errorSlugs.ContainsKey(slugError))
            {
                statusCode = errorSlugs[slugError];
            }
            return statusCode;
        }

        public static ApiException ToApiException(this HttpStatusCode statusCode, string message)
        {
            string fullyQualifiedName = "OmnivoreIntegration.ExceptionHandling." + statusCode.ToString() + "Exception";
            Type t = Type.GetType(fullyQualifiedName);
            return Activator.CreateInstance(t, message) as ApiException;
        }
    }
}
