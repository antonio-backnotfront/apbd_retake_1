using RecordMania.Models;
using RecordMania.Models.DTO;
using Task = RecordMania.Models.Task;

namespace RecordMania.Repositories;

public interface IRecordRepository
{
    public Task<List<Record>> GetAllRecordsAsync(CancellationToken cancellationToken);
    public Task<Record?> GetRecordByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Task?> GetTaskByIdAsync(int id, CancellationToken cancellationToken);
    public Task<AcademicTitle?> GetAcademicTitleByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Teacher?> GetTeacherByIdAsync(int id, CancellationToken cancellationToken);
    public Task<RecordType?> GetRecordTypeByIdAsync(int id, CancellationToken cancellationToken);
    
    public Task<int> CreateRecordAsync(CreateRecordDto record, CancellationToken cancellationToken);
    public Task<int> CreateRecordWithTaskAsync(CreateRecordDto dto, CancellationToken cancellationToken);



}