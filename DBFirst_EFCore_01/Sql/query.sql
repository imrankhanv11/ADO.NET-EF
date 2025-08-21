alter procedure sp_departmentemployee
as
begin
	select d.DepartmentName as DepartmentName, e.Name as EmployeeName, e.Gender as Gender
	from department d
	join Employee e on e.DepartmentID = d.DepartmentID
end

exec sp_departmentemployee;

create procedure sp_employeeCount
as 
begin
	select count(*) as TotalEmp
	from Employee
end

exec sp_employeeCount;

create procedure sp_departmentEmployeeID( @ID int )
as
begin
	select Name as EmployeeName, Gender
	from Employee 
	where DepartmentID = @ID;
end


exec sp_departmentEmployeeID 1;

create procedure sp_projectCount ( @output int output)
as
begin
	select @output = count(*)
	from Projects;
end

declare @value int;
exec sp_projectCount @value output;
print @value

create table benefits(
	id int identity(1,1) primary key,
	BenefitName nvarchar(50) not null,
);

create table employeeBenefits(
	EmpId int,
	BenefitId int,
	AddDate Date,
	Foreign key (EmpId) references Employee(ID),
	Foreign key (BenefitID) references benefits(id),
	primary key (EmpId, BenefitId)
);

alter procedure sp_insertDepartment(@name nvarchar(50), @location nvarchar(10), @id int out)
as
begin
	insert into department (DepartmentName, Locations) 
	values(@name, @location)

	set @id = SCOPE_IDENTITY();
end

DECLARE @output INT;

EXEC sp_insertDepartment 'IT', 'block b', @output OUTPUT;

PRINT @output;
