-- Insert into AcademicTitle table
INSERT INTO AcademicTitle (Name, RequiredLevel) VALUES
('Bachelor', 2),
('Master', 4),
('PhD', 7),
('Professor', 10)

-- Insert into RecordType table (must have these three)
INSERT INTO RecordType (Name) VALUES
('Weak'),
('On-Paper'),
('Blind');

-- Insert into Task table (example tasks)
INSERT INTO Task (Name, Description, RequiredLevel, MinRequiredTime) VALUES
('Fizz-Buzz', 'Print numbers from 1 to 100, replacing multiples of 3 with "Fizz", multiples of 5 with "Buzz", and both with "FizzBuzz".', 1, 1.0),
('Fibonacci Sequence', 'Generate the first 10 Fibonacci numbers.', 4, 0.01),
('Reverse String', 'Take a string input and return it in reverse order.', 1, 0.5);

-- Insert into Teacher table
INSERT INTO Teacher (FirstName, MiddleName, LastName, Email, AcademicTitleId, Level) VALUES
('John', 'Doe', 'Smith', 'john.smith@example.com', 3, 5),
('Jane', 'Mary', 'Doe', 'jane.doe@example.com', 2, 4),
('Bob', NULL, 'Johnson', 'bob.johnson@example.com', 1, 3);

-- Insert into Records table
INSERT INTO Records (TeacherId, RecordTypeId, TaskId, ExecutionTime, CreatedAt) VALUES
(1, 1, 1, 0.005, GETDATE()),
(1, 2, 2, 0.006, GETDATE()),
(1, 3, 3, 0.001, GETDATE()),
(2, 1, 2, 0.0001, GETDATE()),
(2, 2, 3, 0.0001, GETDATE()),
(3, 3, 1, 0.0001, GETDATE());
