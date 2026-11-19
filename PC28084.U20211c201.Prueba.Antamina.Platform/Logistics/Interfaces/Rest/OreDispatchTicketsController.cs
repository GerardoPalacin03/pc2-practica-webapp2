using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.CommandServices;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Resources;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Transform;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest;

/// <summary>
///     REST controller that exposes the ore dispatch tickets endpoint (<c>/api/v1/ore-dispatch-tickets</c>).
/// </summary>
/// <param name="oreDispatchTicketCommandService">The ore dispatch ticket command service.</param>
/// <param name="problemDetailsFactory">The factory used to build localized problem details.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Ore Dispatch Tickets")]
public class OreDispatchTicketsController(
    IOreDispatchTicketCommandService oreDispatchTicketCommandService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>
    ///     Creates an ore dispatch ticket.
    /// </summary>
    /// <param name="resource">The data of the ticket to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     HTTP 201 with the created ticket, including its generated id and its update date; HTTP 400 when the request
    ///     structure or the primitive types are invalid; HTTP 409 when the ticket code is duplicated or the truck
    ///     already has a ticket for the same dispatch date and shift; HTTP 422 when a business rule is violated.
    /// </returns>
    [HttpPost]
    [EndpointSummary("Create an ore dispatch ticket")]
    [EndpointDescription(
        "Creates an ore dispatch ticket. The id is generated when the ticket is stored. The status is Scheduled and the unit is MT when they are not specified.")]
    [ProducesResponseType(typeof(OreDispatchTicketResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateOreDispatchTicket([FromBody] CreateOreDispatchTicketResource resource,
        CancellationToken cancellationToken)
    {
        var createOreDispatchTicketCommand =
            CreateOreDispatchTicketCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await oreDispatchTicketCommandService.Handle(createOreDispatchTicketCommand, cancellationToken);

        return LogisticsActionResultAssembler.ToActionResultFromCreateOreDispatchTicketResult(
            this,
            result,
            problemDetailsFactory,
            createdTicket => StatusCode(StatusCodes.Status201Created,
                OreDispatchTicketResourceFromEntityAssembler.ToResourceFromEntity(createdTicket))
        );
    }
}
