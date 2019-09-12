CREATE DATABASE virtual;

use virtual;

Create type Mascota(
	nombre STRING,
	raza STRING,
	familia STRING,
	fecha_nacimiento DATE
);

CREATE TABLE Persona(
	nombre STRING,
	edad inT,
	hora_creado TIME,
	fecha_creado DATE,
	lista List<Int>,
	mascotas Set<Mascota>,
	sexo BOOLEAN
);

INSERT INTO Persona VALUES("Oscar Cuéllar",22,NOW(),Today(),[1,2,3,4],{{"coco","frensh","mancilla",'2010-04-03'} as Mascota},true);
INSERT INTO Persona VALUES("David Alcazar",22,NOW(),Today(),[1,2,3,5],{{"fito","chihuahua","mancilla",'2019-04-03'} as Mascota},true);
INSERT INTO Persona VALUES("Albita Chinchilla",22,NOW(),Today(),[1,2,3,6],{{"marlley","schnauzer","corona",'2009-04-03'} as Mascota},false);

//UPDATE Persona SET mascotas = mascotas + {new Mascota};

commit;


use virtual;
select * from persona;