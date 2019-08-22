CREATE TABLE IF NOT EXISTS Estudiante(
	carnet int PRIMARY KEY,
	nombres STRING, 
	apellidos STRING,
	fecha_nacimiento DATE,
	hora_nacimiento TIME	
);

CREATE TABLE Libro(
	cod_libro counter PRIMARY KEY,
	nombre STRING
);

CREATE TABLE Prestamo(
	carnet int,
	cod_libro int,
	PRIMARY KEY(carnet,cod_libro)
);

CREATE TABLE Temp(
	valor STRING
);

TRUNCATE TABLE Temp;

DROP TABLE IF EXISTS Temp;


ALTER TABLE Libro ADD
	serie STRING,
	folio STRING;

ALTER TABLE Estudiante DROP
	hora_nacimiento;


