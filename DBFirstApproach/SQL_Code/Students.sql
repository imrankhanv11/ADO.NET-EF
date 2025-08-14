Create Database Students;

-- =========================================
-- 1?? Departments Table
-- =========================================
CREATE TABLE Departments
(
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(100)
);

-- =========================================
-- 2?? Teachers Table
-- =========================================
CREATE TABLE Teachers
(
    TeacherID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- =========================================
-- 3?? Students Table
-- =========================================
CREATE TABLE Students
(
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Age INT,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- =========================================
-- 4?? Courses Table
-- =========================================
CREATE TABLE Courses
(
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    TeacherID INT,
    DepartmentID INT,
    FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- =========================================
-- 5?? Enrollments Table
-- =========================================
CREATE TABLE Enrollments
(
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Grade CHAR(2),
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);


-- =========================================
-- 1?? Departments Table
-- =========================================
INSERT INTO Departments (DepartmentName, Location) VALUES
('Computer Science', 'Block A'),
('Mechanical', 'Block B'),
('Electrical', 'Block C'),
('Civil', 'Block D'),
('Electronics', 'Block E');

-- =========================================
-- 2?? Teachers Table
-- =========================================
INSERT INTO Teachers (Name, Email, DepartmentID) VALUES
('John Doe', 'john@school.com', 1),
('Jane Smith', 'jane@school.com', 2),
('Mike Johnson', 'mike@school.com', 3),
('Emily Davis', 'emily@school.com', 4),
('Robert Brown', 'robert@school.com', 5);

-- =========================================
-- 3?? Students Table
-- =========================================
INSERT INTO Students (Name, Email, Age, DepartmentID) VALUES
('Alice', 'alice@mail.com', 21, 1),
('Bob', 'bob@mail.com', 22, 2),
('Charlie', 'charlie@mail.com', 23, 3),
('Diana', 'diana@mail.com', 20, 4),
('Ethan', 'ethan@mail.com', 24, 5);

-- =========================================
-- 4?? Courses Table
-- =========================================
INSERT INTO Courses (CourseName, TeacherID, DepartmentID) VALUES
('Data Structures', 1, 1),
('Thermodynamics', 2, 2),
('Circuits', 3, 3),
('Structural Analysis', 4, 4),
('Digital Electronics', 5, 5);

-- =========================================
-- 5?? Enrollments Table
-- =========================================
INSERT INTO Enrollments (StudentID, CourseID, Grade) VALUES
(1, 1, 'A'),
(2, 2, 'B'),
(3, 3, 'A'),
(4, 4, 'C'),
(5, 5, 'B');



CREATE PROCEDURE GetStudentsByDepartment
    @DeptID INT
AS
BEGIN
    SELECT s.StudentID, s.Name, s.Email, s.Age, d.DepartmentName
    FROM Students s
    INNER JOIN Departments d ON s.DepartmentID = d.DepartmentID
    WHERE s.DepartmentID = @DeptID;
END


CREATE PROCEDURE AddEnrollment
    @StudentID INT,
    @CourseID INT,
    @Grade CHAR(2)
AS
BEGIN
    INSERT INTO Enrollments (StudentID, CourseID, Grade)
    VALUES (@StudentID, @CourseID, @Grade);
END


CREATE VIEW vw_StudentCourses AS
SELECT s.StudentID, s.Name AS StudentName, s.Age, d.DepartmentName, c.CourseName, e.Grade
FROM Students s
INNER JOIN Departments d ON s.DepartmentID = d.DepartmentID
INNER JOIN Enrollments e ON s.StudentID = e.StudentID
INNER JOIN Courses c ON e.CourseID = c.CourseID;



CREATE VIEW vw_TeacherCourses AS
SELECT t.TeacherID, t.Name AS TeacherName, d.DepartmentName, c.CourseName
FROM Teachers t
INNER JOIN Departments d ON t.DepartmentID = d.DepartmentID
INNER JOIN Courses c ON t.TeacherID = c.TeacherID;
