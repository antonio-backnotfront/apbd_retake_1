using Microsoft.AspNetCore.Mvc;
using RecordMania.Application.Exceptions;
using RecordMania.Application.Services;
using RecordMania.Models.DTO;

namespace RecordMania.API.Controllers;

[ApiController]
[Route("api/records")]
public class RecordController : ControllerBase
{
    private readonly IRecordService _service;
    private readonly ILogger<RecordController> _logger;

    public RecordController(IRecordService service, ILogger<RecordController> logger)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "getAllRecords")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _service.GetRecordsAsync(cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateRecordDto dto, CancellationToken cancellationToken)
    {
        try
        {
            CreateRecordDto? returnDto = await _service.CreateRecordAsync(dto, cancellationToken);
            if (returnDto == null) return Problem();
            return CreatedAtAction("getAllRecords", new { Id = returnDto.Id }, returnDto);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Problem();
        }
    }
}