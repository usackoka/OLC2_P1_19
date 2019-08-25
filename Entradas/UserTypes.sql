CREATE TYPE Persona(
	pet Mascota,
	nombre STRING,
	comidas_favoritas LIST,
	edad int
);

Create type Mascota(
	nombre STRING,
	raza STRING,
	familia STRING,
	fecha_nacimiento DATE
);

Persona @selvin = {{"Firulais","Electrico","Recinos",'2015-08-08'} as Mascota,"Selvin",new List<String>,25} as Persona;
//log(@selvin.nombre+" tiene un perro llamado: "+@selvin.pet.nombre+"\n"+"De raza: "+@selvin.pet.raza);

List @list1 = new List<Persona>;
@list1.insert({new Mascota, "Persona 1", new List<String>,15} as Persona);
@list1.insert(new Persona);
@list1.insert({{"Firulais","electrico","Suarez",'1998-10-10'} as Mascota, "Luis Lizama :v",["hamburguesa","pizza","pollo"],30} as Persona);

log(@list1.get(2).nombre);
log(@list1.get(2).pet.nombre);

log(@list1.get(2).comidas_favoritas.get(0));

//===================== SEGUNDA INSTANCIA ================================
Persona @pipol2 = new Persona;
@pipol2 = {new Mascota,"Albita",22} as Persona;
@pipol2.pet.nombre = "Panqueque";

Persona @pipol3 = {new Mascota, "Allisson", 8} as Persona;
@pipol3.pet.nombre = "Fito";

log(@pipol2.pet.nombre);
log(@pipol3.pet.nombre);
log(@pipol3.nombre);

Persona @pipol = new Persona;
@pipol.nombre = "Oscar";
@pipol.pet = new Mascota;
@pipol.pet.nombre = "Coco";
@pipol.pet.raza = "Frensh & Cocker";
@pipol.pet.fecha_nacimiento = '2011-06-03';
@pipol.pet.familia = "Mancilla";


Persona @kokoa = new Persona;
@kokoa.pet = new Mascota;
@kokoa.pet.nombre = "Fito";

log(@pipol.pet.nombre);
log(@kokoa.pet.nombre);