using System.Net;
using Api.Common;
using Api.Models;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TransportProxy.Application.LocationInformation.V2;
using TransportProxy.Application.StopEvent;

namespace Api.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}")]
public class TransportDataController : ApiController
{
    private readonly StopEventService _stopEventService;
    private readonly LocationInformationService _locationInformationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public TransportDataController(StopEventService stopEventService,
        LocationInformationService locationInformationService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _stopEventService = stopEventService;
        _locationInformationService = locationInformationService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    /// <summary>
    /// -
    /// </summary>
    /// <remarks>
    /// The "stationBoard" endpoint provides a list of outgoing connections from a specified location.
    /// This information can be used for creating a station-board for a specific location.
    /// </remarks>
    /// <param name="station">Here is the description for ID.</param>
    /// <param name="id">The stationRef identifier</param>
    /// <param name="dateTimeString">Datetime in xxx Format</param>
    /// <param name="limit">Result count</param>
    /*<response code="200">Product created</response>
    <response code="400">Product has missing/invalid values</response>
    <response code="500">Oops! Can't create your product right now</response>*/
    [HttpGet($"stationBoard", Name = $"v2/{nameof(TransportDataController)}/{nameof(GetStationBoard)}")]
    [ProducesResponseType(typeof(StationBoardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStationBoard(
        [FromHeader(Name = "authorization")] string authorizationHeader,
        [FromQuery(Name = "station"), BindRequired] string station,
        [FromQuery(Name = "id")] int? id,
        [FromQuery(Name = "datetime")] string? dateTimeString,
        [FromQuery(Name = "limit")] int? limit)
    {
        string? token = null;
        if (!string.IsNullOrEmpty(authorizationHeader) &&
            authorizationHeader.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
        {
            token = authorizationHeader.Substring("Bearer ".Length).Trim();
        }
        
        DateTime now = DateTime.UtcNow; // Get current UTC time
        var dateTime = dateTimeString != null ? DateTime.Parse(dateTimeString) : now;
        var formattedDateTimeNow = now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        var formattedDateTime = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Console.WriteLine(formattedDateTime);

        int finalId;
        if (id == null)
        {
            var lirRequest = new LocationInformationRequestModel()
            {
                now = formattedDateTimeNow,
                Locationname = station,
                Limit = 1
            };
            try
            {
                var lir = await _locationInformationService.GetOjpLocationInformation(lirRequest, token, _httpContextAccessor);
                finalId = lir.OJPResponse.ServiceDelivery.OJPLocationInformationDelivery.Locations.First().Location
                    .StopPlace.StopPlaceRef;
            }
            catch (CustomAuthenticationException e)
            {
                var error = _mapper.Map<ExceptionDto>(e);
                return StatusCode((int)HttpStatusCode.Unauthorized, error);
            }
            catch (CustomForbiddenException e)
            {
                var error = _mapper.Map<ExceptionDto>(e);
                return StatusCode((int)HttpStatusCode.Forbidden, error);
            }
            catch (CustomTooManyRequestsException e)
            {
                var error = _mapper.Map<ExceptionDto>(e);
                return StatusCode((int)HttpStatusCode.TooManyRequests, error);
            }
            catch (CustomInternalException e)
            {
                var error = _mapper.Map<ExceptionDto>(e);
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }
        }
        else
        {
            finalId = id ?? 0;
        }

        var stopEventRequest = new StopEventRequestModel()
        {
            Now = formattedDateTimeNow,
            Datetime = formattedDateTime,
            Location = " ",
            Stopref = finalId,
            Onwardcalls = "true",
            Prevcalls = "true",
            Type = StopEventType.both,
            Limit = limit ?? 3
        };

        try
        {
            var data = await _stopEventService.GetOjpStopEvent(stopEventRequest, token, _httpContextAccessor);
            var dto = _mapper.Map<StationBoardDto>(data);

            return Ok(dto);
        }
        catch (CustomAuthenticationException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.Unauthorized, error);
        }
        catch (CustomForbiddenException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.Forbidden, error);
        }
        catch (CustomTooManyRequestsException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.TooManyRequests, error);
        }
        catch (CustomInternalException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, error);
        }
    }

    /// <summary>
    /// -
    /// </summary>
    /// <remarks>
    /// This endpoint provides you with a list of locations matching your parameter.
    /// You can use this for receiving a list of locations for easy selection as well
    /// as the location id needed for other endpoints.
    /// </remarks>
    /// <param name="locationName">The location Name</param>
    /// <param name="limit">Result count</param>
    [HttpGet($"locationInformation", Name = $"v2/{nameof(TransportDataController)}/{nameof(GetLocationInformation)}")]
    [ProducesResponseType(typeof(LocationInformationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLocationInformation(
        [FromHeader(Name = "Authorization")] string authorizationHeader,
        [FromQuery(Name = "locationName"), BindRequired] string locationName,
        [FromQuery(Name = "limit")] int? limit)
    {
        string? token = null;
        if (!string.IsNullOrEmpty(authorizationHeader) &&
            authorizationHeader.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
        {
            token = authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        DateTime now = DateTime.UtcNow; // Get current UTC time
        var formattedDateTime = now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Console.WriteLine(formattedDateTime);

        var locationInformationRequest = new LocationInformationRequestModel()
        {
            now = formattedDateTime,
            Locationname = locationName,
            Limit = limit ?? 10
        };

        try
        {
            var data = await _locationInformationService.GetOjpLocationInformation(locationInformationRequest, token, _httpContextAccessor);
            var dto = _mapper.Map<LocationInformationDto>(data);

            return Ok(dto);
        }
        catch (CustomAuthenticationException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.Unauthorized, error);
        }
        catch (CustomForbiddenException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.Forbidden, error);
        }
        catch (CustomTooManyRequestsException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.TooManyRequests, error);
        }
        catch (CustomInternalException e)
        {
            var error = _mapper.Map<ExceptionDto>(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, error);
        }
    }
}