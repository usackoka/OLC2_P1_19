//funciones auxiliares-------------
int ins(int @carnet, String @nombres, String @apellidos){
	INSERT INTO Estudiante(carnet, nombres, apellidos) VALUES (@carnet, @nombres, @apellidos);
}

int modificarEdad(INT @carnet, INT @edad){
	UPDATE Estudiante SET edad = @edad WHERE carnet == @carnet;
}

Date calcularFechaNacimiento(int @edad){
	Date @hoy = Today();
	int @ano = @hoy.getYear();
	@ano = @ano-@edad;
	String @fecha = ""+@ano+"-"+@hoy.getMonth()+"-"+@hoy.getDay();
	return (Date)@fecha;
}

CREATE TABLE IF NOT EXISTS Estudiante(
	carnet int PRIMARY KEY,
	nombres STRING, 
	apellidos STRING,
	fecha_nacimiento DATE,
	hora_nacimiento TIME	
);

CREATE TABLE IF NOT EXISTS Libro(
	cod_libro counter PRIMARY KEY,
	nombre STRING
);

CREATE TABLE IF NOT EXISTS Prestamo(
	carnet int,
	cod_libro int,
	PRIMARY KEY(carnet,cod_libro)
);

CREATE TABLE IF NOT EXISTS Temp(
	valor STRING
);

TRUNCATE TABLE Temp;

DROP TABLE IF EXISTS Temp;

ALTER TABLE Libro ADD
	serie STRING,
	folio STRING;

ALTER TABLE Estudiante DROP
	hora_nacimiento;

ALTER TABLE Estudiante ADD
	facultad STRING,
	edad INT;

List @carnets = [201503712,201513629,201504028,
201408486,201504480,201503865,201318570
,201602929,201503666,201504429];
List @edades = new List<INT>;

ins(201503712,"Oscar René","Cuéllar Mancillar");
ins(201513629,"Alba Jeanneeth","Chinchilla Morales");
ins(201504028,"Rodney Estuardo","Lopez Marroquín");
ins(201403904,"Irving Samuel","Rosales Dominguez");
ins(201408486,"Sharolin Guadalupe", "Lacunza González");
ins(201504480,"David Andres","Alcazar Escobar");
ins(201503865,"Julia Argentina","Sierra Herrera");
ins(201318570,"Mitchel Andrea","Cano Marroquín");
ins(201602929,"Sebastián","Gómez Lavarreda");
ins(201503666,"Miguel Angel Omar","Ruano Roca");
ins(201504429,"Gustavo Adolfo","Gamboa Cruz");

String @nombres = "Estudiante: ", @carnet = "Carnet: ";

BEGIN BATCH
UPDATE Estudiante SET facultad="Ingeniería";
DELETE FROM Estudiante WHERE nombres=="Irving Samuel";
APPLY BATCH;

modificarEdad(201503712,22);
modificarEdad(201513629,22);
modificarEdad(201504028,22);
modificarEdad(201408486,23);
modificarEdad(201504480,22);
modificarEdad(201503865,23);
modificarEdad(201318570,23);
modificarEdad(201602929,21);
modificarEdad(201503666,24);
modificarEdad(201504429,23);

CURSOR @res_edades IS SELECT edad FROM Estudiante;
OPEN @res_edades;
FOR EACH(int @edad) IN @res_edades{
	@edades.insert(@edad);
}
CLOSE @res_edades;

try{
	for(int @i=0; @i<@carnets.size();@i++){
		Date @fe = calcularFechaNacimiento(@edades.get(@i));
		UPDATE Estudiante SET fecha_nacimiento=@fe WHERE carnet == @carnets.get(@i);
	}
}catch(IndexOutException @ex){
	log(@ex);
}

SELECT * FROM Estudiante;
//SELECT @nombres+nombres,@carnet+carnet FROM Estudiante WHERE carnet > 201500000 && carnet < 201600000; 
