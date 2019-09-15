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

/*
ALTER TABLE Persona ADD
	cursos_x_semestre List<List<String>>;

UPDATE Persona SET cursos_x_semestre = [["ipc1","logica sistemas","mate computo1"],["ipc2","lenguajes","mate computo2"],["edd","compi1","orga"]];
*/

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
UPDATE Persona SET mascotas = mascotas + { "Coco":{"Coco","Frensh","Mancilla",'2011-06-03'} as Mascota,
								"Fito":{"Fito","Chihuahua","Mancilla",'2019-04-00'} as Mascota}
								WHERE nombre == "Oscar Cuéllar" && edad == 22;

//commit;

//use virtual;
select * from Persona;