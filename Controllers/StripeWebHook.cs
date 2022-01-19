using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using System.IO;
using NestLinkV2.Services;
using NestLinkV2.ViewModels;
using NestLinkV2.Data;
using NestLinkV2.Models;
using Hangfire;

namespace NestLinkV2.Controllers
{
    [Route("api/[controller]")]
    public class StripeWebHook : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public StripeWebHook(ApplicationDbContext context, IBackgroundJobClient backgroundJobClient)
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                Invoice invoice = null;

                if (stripeEvent.Type == Events.InvoicePaid)
                {
                    invoice = stripeEvent.Data.Object as Invoice;
                    handlePaymentIntentSucceeded(invoice);
                }
                else if (stripeEvent.Type == Events.InvoicePaymentFailed)
                {
                    invoice = stripeEvent.Data.Object as Invoice;
                    handlePaymentIntentFailed(invoice);
                }
                else if (stripeEvent.Type == Events.InvoiceSent)
                {
                    invoice = stripeEvent.Data.Object as Invoice;
                    handleInvoiceSent(invoice);
                }
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        public void GenerateStripeSalesInvoice()
        {
            
        }

        private void handlePaymentIntentSucceeded(Invoice invoice)
        {
            
        }

        private void handlePaymentIntentFailed(Invoice invoice)
        {
            
        }

        private void handleInvoiceSent(Invoice invoice)
        {
            
        }
    }
}
