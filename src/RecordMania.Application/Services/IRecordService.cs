using RecordMania.Models.DTO;

namespace RecordMania.Application.Services;

public interface IRecordService
{
    public Task<List<GetRecordDto>> GetRecordsAsync(CancellationToken cancellationToken);
    public Task<CreateRecordDto?> CreateRecordAsync(
        CreateRecordDto createRecordDto, 
        CancellationToken cancellationToken
        );
}