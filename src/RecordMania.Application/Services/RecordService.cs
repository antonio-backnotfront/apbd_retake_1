using RecordMania.Application.Exceptions;
using RecordMania.Models;
using RecordMania.Models.DTO;
using RecordMania.Repositories;
using Task = RecordMania.Models.Task;

namespace RecordMania.Application.Services;

public class RecordService : IRecordService
{
    IRecordRepository _repository;

    public RecordService(IRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetRecordDto>> GetRecordsAsync(CancellationToken cancellationToken)
    {
        List<Record> records = await _repository.GetAllRecordsAsync(cancellationToken);
        List<GetRecordDto> list = new List<GetRecordDto>();
        foreach (Record record in records)
        {
            RecordType? recordType =
                await _repository.GetRecordTypeByIdAsync(record.RecordTypeId, cancellationToken);
            if (recordType == null) throw new NotExistsException($"RecordType {record.RecordTypeId} not found");

            Task? task =
                await _repository.GetTaskByIdAsync(record.TaskId, cancellationToken);
            if (task == null) throw new NotExistsException($"Task {record.TaskId} not found");

            Teacher? teacher =
                await _repository.GetTeacherByIdAsync(record.TeacherId, cancellationToken);
            if (teacher == null) throw new NotExistsException($"Teacher {record.TeacherId} is not found");

            AcademicTitle? academicTitle =
                await _repository.GetAcademicTitleByIdAsync(teacher.AcademicTitleId, cancellationToken);
            if (academicTitle == null)
                throw new NotExistsException($"AcademicTitle {teacher.AcademicTitleId} is not found");

            list.Add(new GetRecordDto()
            {
                Id = record.Id,
                CreatedAt = record.CreatedAt,
                ExecutionTime = record.ExecutionTime,
                RecordType = recordType.Name,
                Task = new GetTaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    MinRequiredTime = task.MinRequiredTime,
                    RequiredLevel = task.RequiredLevel
                },
                Teacher = new GetTeacherDto()
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    MiddleName = teacher.MiddleName,
                    LastName = teacher.LastName,
                    Email = teacher.Email,
                    AcademicTitle = academicTitle.Name,
                    Level = teacher.Level,
                }
            });
        }

        return list;
    }

    public async Task<CreateRecordDto?> CreateRecordAsync(CreateRecordDto createRecordDto,
        CancellationToken cancellationToken)
    {
        // check if teacher exists
        Teacher? teacher = 
            await _repository.GetTeacherByIdAsync(createRecordDto.TeacherId, cancellationToken);
        if (teacher == null) throw new NotExistsException($"Teacher {createRecordDto.TeacherId} not found");
        
        // check recordType 
        RecordType? recordType = 
            await _repository.GetRecordTypeByIdAsync(createRecordDto.RecordTypeId, cancellationToken);
        if (recordType == null) throw new NotExistsException($"RecordType {createRecordDto.RecordTypeId} not found");
        
        // check if only task was provided and if it exists
        Task? task = 
            await _repository.GetTaskByIdAsync(createRecordDto.Task.Id, cancellationToken);
        if (task == null && (createRecordDto.Task.Name == null || createRecordDto.Task.Description == null)) 
            throw new NotExistsException($"Task {createRecordDto.Task.Id} not found");
        
        // check required level
        if (task != null)
        {
            if (task.RequiredLevel != null && teacher.Level < task.RequiredLevel)
            throw new LimitException("Teacher can't access this task");
            if (createRecordDto.ExecutionTime >= task.MinRequiredTime)
                throw new ArgumentException("Execution time is too big");
        }

        // check execution time

        if (task == null)
        {
            return new CreateRecordDto()
            {
                Id = await _repository.CreateRecordWithTaskAsync(createRecordDto, cancellationToken),
                CreatedAt = createRecordDto.CreatedAt,
                ExecutionTime = createRecordDto.ExecutionTime,
                RecordTypeId = recordType.Id,
                Task = createRecordDto.Task,
                TeacherId = createRecordDto.TeacherId,
            };
        }

        Console.WriteLine($"hello");
        return new CreateRecordDto()
        {
            Id = await _repository.CreateRecordAsync(createRecordDto, cancellationToken),
            CreatedAt = createRecordDto.CreatedAt,
            ExecutionTime = createRecordDto.ExecutionTime,
            RecordTypeId = recordType.Id,
            Task = createRecordDto.Task,
            TeacherId = createRecordDto.TeacherId,
        };
    }
}