using Microsoft.Data.SqlClient;
using RecordMania.Models;
using RecordMania.Models.DTO;
using Task = RecordMania.Models.Task;

namespace RecordMania.Repositories;

public class RecordRepository : IRecordRepository
{
    private readonly string _connectionString;

    public RecordRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Record>> GetAllRecordsAsync(CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, TeacherId, RecordTypeId, TaskId, ExecutionTime, CreatedAt from Records";
        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        List<Record> records = new List<Record>();
        while (await reader.ReadAsync(cancellationToken))
        {
            Record record = new Record();
            record.Id = reader.GetInt32(0);
            record.TeacherId = reader.GetInt32(1);
            record.RecordTypeId = reader.GetInt32(2);
            record.TaskId = reader.GetInt32(3);
            record.ExecutionTime = reader.GetDecimal(4);
            record.CreatedAt = reader.GetDateTime(5);
            records.Add(record);
        }
        return records;
    }

    public async Task<Record?> GetRecordByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, TeacherId, RecordTypeId, TaskId, ExecutionTime, CreatedAt from Records 
                                                                     where Id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (await reader.ReadAsync(cancellationToken))
        {
            Record record = new Record();
            record.Id = reader.GetInt32(0);
            record.TeacherId = reader.GetInt32(1);
            record.RecordTypeId = reader.GetInt32(2);
            record.TaskId = reader.GetInt32(3);
            record.ExecutionTime = reader.GetDecimal(4);
            record.CreatedAt = reader.GetDateTime(5);
            return record;
        }
        return null;
    }

    public async Task<Task?> GetTaskByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, Name, Description, RequiredLevel, MinRequiredTime from Task 
                                                                     where Id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (await reader.ReadAsync(cancellationToken))
        {
            Task task = new Task();
            task.Id = reader.GetInt32(0);
            task.Name = reader.GetString(1);
            task.Description = reader.GetString(2);
            task.RequiredLevel = reader.GetInt32(3);
            task.MinRequiredTime = reader.IsDBNull(4) ? null : reader.GetDecimal(4);
            return task;
        }
        return null;
    }

    public async Task<AcademicTitle?> GetAcademicTitleByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, Name, RequiredLevel from AcademicTitle 
                                                                     where Id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (await reader.ReadAsync(cancellationToken))
        {
            AcademicTitle title = new AcademicTitle();
            title.Id = reader.GetInt32(0);
            title.Name = reader.GetString(1);
            title.RequiredLevel = reader.GetInt32(2);
            return title;
        }
        return null;
    }

    public async Task<Teacher?> GetTeacherByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, FirstName, MiddleName, LastName, Email, AcademicTitleId, Level from Teacher 
                                                                     where Id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (await reader.ReadAsync(cancellationToken))
        {
            Teacher teacher = new Teacher();
            teacher.Id = reader.GetInt32(0);
            teacher.FirstName = reader.GetString(1);
            teacher.MiddleName = reader.IsDBNull(2) ? null : reader.GetString(2);
            teacher.LastName = reader.GetString(3);
            teacher.Email = reader.GetString(4);
            teacher.AcademicTitleId = reader.GetInt32(5);
            teacher.Level = reader.GetInt32(6);
            return teacher;
        }
        return null;
    }

    public async Task<RecordType?> GetRecordTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        string query = @"select Id, Name from RecordType 
                                                                     where Id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if (await reader.ReadAsync(cancellationToken))
        {
            RecordType type = new RecordType();
            type.Id = reader.GetInt32(0);
            type.Name = reader.GetString(1);
            return type;
        }
        return null;
    }
    public async Task<int> CreateRecordAsync(CreateRecordDto dto, CancellationToken cancellationToken)
    {
        string recordQuery = @"INSERT INTO Records (TeacherId, RecordTypeId, TaskId, ExecutionTime, CreatedAt) 
            OUTPUT INSERTED.Id
            VALUES(@TeacherId, @RecordTypeId, @TaskId, @ExecutionTime, @CreatedAt)
            ";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = connection.BeginTransaction();
        try
        {
            var recordCommand = new SqlCommand(recordQuery, connection, transaction);
            recordCommand.Parameters.AddWithValue("@TeacherId", dto.TeacherId);
            recordCommand.Parameters.AddWithValue("@RecordTypeId", dto.RecordTypeId);
            recordCommand.Parameters.AddWithValue("@TaskId", dto.Task.Id);
            recordCommand.Parameters.AddWithValue("@ExecutionTime", dto.ExecutionTime);
            recordCommand.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
            var id = (int?)await recordCommand.ExecuteScalarAsync(cancellationToken);
            Console.WriteLine("hello");
            Console.WriteLine($"{id}");
            if (id == null) throw new Exception("Couldn't insert and retrieve new id");
            await transaction.CommitAsync(cancellationToken);
            return id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
    
    public async Task<int> CreateRecordWithTaskAsync(CreateRecordDto dto, CancellationToken cancellationToken)
    {
        string recordQuery = @"INSERT INTO Records (TeacherId, RecordTypeId, TaskId, ExecutionTime, CreatedAt) 
            OUTPUT INSERTED.Id
            VALUES(@TeacherId, @RecordTypeId, @TaskId, @ExecutionTime, @CreatedAt)
            ";
        
        string taskQuery = @"INSERT INTO Task (Name, Description, RequiredLevel, MinRequiredTime) 
            OUTPUT INSERTED.Id
            VALUES (@Name, @Description, @RequiredLevel, @MinRequiredTime)
            ";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = connection.BeginTransaction();
        try
        {
            var taskCommand = new SqlCommand(taskQuery, connection, transaction);

            taskCommand.Parameters.AddWithValue("@Name", dto.Task.Name);
            taskCommand.Parameters.AddWithValue("@Description", dto.Task.Description);
            taskCommand.Parameters.AddWithValue("@RequiredLevel", dto.Task.RequiredLevel);
            taskCommand.Parameters.AddWithValue("@MinRequiredTime", dto.Task.MinRequiredTime);
            var taskId = (int?)await taskCommand.ExecuteScalarAsync(cancellationToken);
            if (taskId == null) throw new Exception("Couldn't insert and retrieve new id");
            
            var recordCommand = new SqlCommand(recordQuery, connection, transaction);

            recordCommand.Parameters.AddWithValue("@TeacherId", dto.TeacherId);
            recordCommand.Parameters.AddWithValue("@RecordTypeId", dto.RecordTypeId);
            recordCommand.Parameters.AddWithValue("@TaskId", taskId);
            recordCommand.Parameters.AddWithValue("@ExecutionTime", dto.ExecutionTime);
            recordCommand.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
            var id = (int?)await recordCommand.ExecuteScalarAsync(cancellationToken);
            if (id == null) throw new Exception("Couldn't insert and retrieve new id");
            await transaction.CommitAsync(cancellationToken);
            return id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}