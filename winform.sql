create database empdemo
use empdemo
create table emp(
    id int primary key identity(1,1),
    name varchar(30),
    age int,
    gender varchar(15),
    department varchar(30),
    salary money
)
select *from emp