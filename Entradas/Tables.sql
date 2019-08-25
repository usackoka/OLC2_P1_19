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

/*
ALTER TABLE Libro ADD
	serie STRING,
	folio STRING;

ALTER TABLE Estudiante DROP
	hora_nacimiento;
*/

INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201503712,"Oscar René","Cuéllar Mancillar");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201513629,"Alba Jeanneeth","Chinchilla Morales");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201504028,"Rodney Estuardo","Lopez Marroquín");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201504480,"David Andres","Alcazar Escobar");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201503865,"Julia Argentina","Sierra Herrera");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201503666,"Miguel Angel Omar","Ruano Roca");
INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (201504429,"Gustavo Adolfo","Gamboa Cruz");

String @nombres = "Estudiante: ", @carnet = "Carnet: ";

SELECT @nombres+nombres,@carnet+carnet FROM Estudiante;

//WHERE carnet > 201500000;




