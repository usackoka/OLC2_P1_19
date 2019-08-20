CREATE TYPE Persona(
	pet Mascota,
	nombre STRING,
	edad int
);

Create type Mascota(
	nombre STRING,
	raza STRING,
	fecha_nacimiento DATE
);

Persona @pipol = new Persona;
@pipol.nombre = "Oscar";
@pipol.pet = new Mascota;
@pipol.pet.nombre = "Coco";
@pipol.pet.raza = "Frensh & Cocker";
@pipol.pet.fecha_nacimiento = '2011-06-03';

log(@pipol.pet.nombre);


Persona @pipol2 = new Persona
@pipol2 = {new Mascota,"Albita",22};
@pipol2.pet.nombre = "Panqueque";

log(@pipol2.pet.nombre);

Persona @pipol3 = {new Mascota, "Allisson", 8};
@pipol3.pet.nombre = "Fito";

log(@pipol3.pet.nombre);