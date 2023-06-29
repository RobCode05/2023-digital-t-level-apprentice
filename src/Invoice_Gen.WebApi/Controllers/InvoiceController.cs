﻿using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Gen.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService)
    {
        _logger = logger;
        _invoiceService = invoiceService;
    }

    /// <summary>
    /// Returns all invoices in the system in a list of <see cref="InvoiceViewModel"/>
    /// </summary>
    /// <returns>
    /// A list of <see cref="InvoiceViewModel"/> instances with some default data
    /// </returns>
    [HttpGet(Name = "GetInvoices")]
    [ProducesResponseType(typeof(List<InvoiceViewModel>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        using (_logger.BeginScope("Getting all invoices"))
        {
            var invoices = _invoiceService.GetInvoices();

            _logger.LogInformation("Returning list of {InvoiceViewModel}", typeof(InvoiceViewModel));
            return new OkObjectResult(invoices);
        }
    }

    /// <summary>
    /// Gets the instance of <see cref="InvoiceViewModel"/> for the provided <paramref name="invoiceId"/>
    /// </summary>
    /// <returns>
    /// The <see cref="InvoiceViewModel"/> instance which matches the supplied <paramref name="invoiceId"/>
    /// or a <see cref="StatusCodes.Status404NotFound"/> if one cannot be found
    /// </returns>
    [HttpGet("{invoiceId}", Name = "GetInvoiceById")]
    [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetInvoiceById(int invoiceId)
    {
        using (_logger.BeginScope("Getting invoice data for {ID}", invoiceId))
        {
            var invoice = _invoiceService.GetById(invoiceId);
            if (invoice == null)
            {
                _logger.LogInformation("Unable to find invoice record");
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning {InvoiceViewModel} for {ID}", nameof(InvoiceViewModel), invoiceId);
            return new OkObjectResult(invoice);
        }
    }

    /// <summary>
    /// Allows API consumers to create new Invoice records in the database using the values of the
    /// fields in the provided <see cref="InvoiceCreateModel"/>
    /// </summary>
    /// <param name="newInvoice">An object which describes the new Client record</param>
    /// <returns>
    /// OK (i.e. 200) if the new record could be created
    /// Internal Server Error (i.e. 500) if the record could not be created
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateInvoice(InvoiceCreateModel newInvoice)
    {
        using (_logger.BeginScope("Request to create new client Invoice for client {ClientId} received",
                   newInvoice.ClientId))
        {
            var response = await _invoiceService.CreateNewInvoice(newInvoice);
            if (response == default)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new CreatedResult(nameof(GetInvoiceById), new { invoiceId = response });
        }
    }
    
    /// <summary>
    /// Used to delete an Invoice record from the database. The Invoice record selected for
    /// deletion is the one which matches on <paramref name="invoiceId"/>
    /// </summary>
    /// <param name="invoiceId">The ID of the invoice record to delete</param>
    /// <returns>
    /// OK (i.e. 200) if the new record could be deleted
    /// Bad Request (i.e. 400) if clientId is incorrect
    /// </returns>
    [HttpDelete("{invoiceId}", Name = "Invoice")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteInvoice(int invoiceId)
    {
        using (_logger.BeginScope("Request to delete invoice {InvoiceId} received", invoiceId))
        {
            if (invoiceId == default)
            {
                _logger.LogInformation("Supplied InvoiceId was 0");
                return new BadRequestResult();
            }

            await _invoiceService.DeleteInvoice(invoiceId);

            return new OkResult();
        }
    }
}
