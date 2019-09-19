CREATE DATABASE virtual;

use virtual;

CREATE TABLE Persona(
	nombre STRING,
	edad inT,
	hora_creado TIME,
	fecha_creado DATE,
	lista List<Int>,
	sexo BOOLEAN
);

INSERT INTO Persona VALUES("Oscar Cuéllar",22,NOW(),Today(),[1,2,3,7],true);
INSERT INTO Persona VALUES("David Alcazar",22,NOW(),Today(),[1,2,3,5],true);
INSERT INTO Persona VALUES("Albita Chinchilla",22,NOW(),Today(),[1,2,3,6],false);

//commit;

Create type Mascota(
	nombre STRING,
	raza STRING,
	familia STRING,
	fecha_nacimiento DATE
);

ALTER TABLE Persona ADD
	mascotas map<String,Mascota>;

UPDATE Persona SET mascotas = new Map<String,Mascota>;
//UPDATE Persona SET mascotas = {"":new Mascota};
UPDATE Persona SET mascotas = mascotas + { "Coco":{"Coco","Frensh","Mancilla",'2011-06-03'} as Mascota,
								"Fito":{"Fito","Chihuahua","Mancilla",'2019-04-00'} as Mascota}
								WHERE nombre == "Oscar Cuéllar" && edad == 22;


CREATE DATABASE Database1;

USE Database1;

CREATE TYPE Curso(
nombre String,
codigo Int
);

CREATE TYPE Direccion(
Apartamento String,
Zona String,
Nivel int
);

CREATE TABLE Alumno(
carnet Counter,
Nombre String,
Edad int,
Cursor_Aprobados List<List<Curso>>
);

List @aux = [
  [{"MB1", 10} as Curso, {"MB2", 10} as Curso],
  [{"MB2", 10} as Curso, {"MB1", 10} as Curso] 
];

INSERT INTO Alumno(Nombre, Edad, Cursor_Aprobados) VALUES ("Estudiante1", 18, @aux);

commit;


use Database1;
select * from Alumno;

Cursor @c is Select Cursor_Aprobados from Alumno;

OPEN @c;
for each(List<List<Curso>> @lista) in @c{
	log(@c.get(0).Nombre);
}

Close @c;