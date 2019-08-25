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

ins(201503712,"Oscar René","Cuéllar Mancillar");
ins(201513629,"Alba Jeanneeth","Chinchilla Morales");
ins(201504028,"Rodney Estuardo","Lopez Marroquín");
ins(201403904,"Irving Samuel","Rosales Dominguez");
ins(201408486,"Sharolin Guadalupe", "Lacunza González");
ins(201504480,"David Andres","Alcazar Escobar");
ins(201503865,"Julia Argentina","Sierra Herrera");
ins(201318570,"Mitchel Andrea","Cano Marroquín");
ins(201602929, "Sebastián","Gómez Lavarreda");
ins(201503666,"Miguel Angel Omar","Ruano Roca");
ins(201504429,"Gustavo Adolfo","Gamboa Cruz");

int ins(int @carnet, String @nombres, String @apellidos){
	INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (@carnet, @nombres, @apellidos);
}

String @nombres = "Estudiante: ", @carnet = "Carnet: ";

SELECT @nombres+nombres,@carnet+carnet FROM Estudiante WHERE carnet > 201500000 && carnet < 201600000; 



