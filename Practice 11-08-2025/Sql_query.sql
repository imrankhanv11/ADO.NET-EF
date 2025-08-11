CREATE TABLE Students (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,   
    Age INT NOT NULL,               
    Course NVARCHAR(50) NOT NULL, 
    JoinDate DATE DEFAULT GETDATE()  
);

INSERT INTO Students (Name, Age, Course)
VALUES
('Imran Khan', 22, 'B.Tech AI & DS'),
('John Smith', 23, 'BCA'),
('Sara Lee', 21, 'B.Sc Computer Science');


select * from Students;