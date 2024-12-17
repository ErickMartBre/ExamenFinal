create database ConstructoraUH

use ConstructoraUH

create table Colaboradores(
Carnet int primary key,
PrimerNombre nvarchar(50) not null,
SegundoNombre nvarchar(50),
PrimerApellido nvarchar(50) not null,
SegundoApellido nvarchar(50),
FechaNacimiento date,
Direccion nvarchar(255) default 'San Jose',
Telefono nvarchar(10),
Email nvarchar(100) unique,
Salario decimal(10,2) default 250000,
Categoria nvarchar(20)
)

create table Proyectos(
CodigoProyecto int primary key,
NombreProyecto nvarchar(255) not null,
FechaInicio date,
FechaFin date
)

CREATE TABLE Asignaciones (
    Carnet int,
    CodigoProyecto int,
    FechaAsignacion date default getdate(),
    primary key (Carnet, CodigoProyecto),
    foreign key (Carnet) references Colaboradores(Carnet),
    foreign key (CodigoProyecto) references Proyectos(CodigoProyecto)
);

insert into Colaboradores(Carnet,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,FechaNacimiento,Direccion,Telefono,Email, Categoria)
values (1, 'Erick', NULL, 'Martinez', 'Brenes', '2003-04-28', 'San Jose, Pavas', '61301234', 'erickmartbre@topicreations.com', 'Administrador'),
	   (2, 'Gianmarco', NULL, 'Oporta', 'Perez', '2003-05-17', 'San Jose, Uruca', '62367000', 'gianmarcoop@topicreations.com', 'Operario')

insert into Proyectos(CodigoProyecto,NombreProyecto,FechaInicio)
values (101, 'Amazon-Alajuela', '2023-05-24'),
	   (102, 'Accenture-SanJose', '2023-12-03'),
	   (103, 'Concentrix-Limon', '2025-02-12')

insert into Asignaciones(Carnet, CodigoProyecto)
values (1,101),
	   (1,103),
	   (2,102)

select * from Colaboradores
select * from Proyectos
select * from Asignaciones