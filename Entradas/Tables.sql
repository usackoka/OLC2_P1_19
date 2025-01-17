CREATE DATABASE virtual;

USE virtual;

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

CREATE TYPE Mascota(
	nombre STRING,
	raza STRING,
	edad INT
);

CREATE TABLE IF NOT EXISTS Estudiante(
	carnet int PRIMARY KEY,
	contador COUNTER,
	comidas_favoritas SET<String>,
	nombres STRING, 
	apellidos STRING,
	fecha_nacimiento DATE,
	pet MASCOTA,
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

//SELECT @nombres+nombres,@carnet+carnet FROM Estudiante WHERE carnet > 201500000 && carnet < 201600000; 

Procedure Ejemplo_Procedure(int @n, int @p), (int @retorno1, int @ret2){
	if (@n == 0) {
		return 1, 2;
	}
	else {
		return @n, @n *2;      
	} 
} 

Procedure Ejemplo_Procedure(int @n, String @mensaje),(int @ret1, String @ret2){
	if (@n == 0) {
		return 1, "";
	}
	else {
		return @n*2,@mensaje+" Devuelto";      
	} 
}

Int @A,@B,@D;
String @C;
 
@A, @B = Call Ejemplo_Procedure (3,3);
@D, @C = Call Ejemplo_Procedure (3, "Mensaje");

//log(@A);
//log(@B);
//log(@C);
//log(@D);

ALTER TABLE Estudiante ADD 
	notas_cursos MAP<String,int>;

UPDATE Estudiante SET notas_cursos = ["compi2":0,"social":45,"filo 5":50];
UPDATE Estudiante set notas_cursos["compi2"] = 61;
UPDATE Estudiante SET notas_cursos = notas_cursos + {"logica":35,"Quimica 9":15,"mate1":99,"mate2":90,"biologia avanzada2":10};
UPDATE Estudiante SET notas_cursos = notas_cursos - {"Quimica 9","biologia avanzada2"};
DELETE notas_cursos["filo 5"] FROM Estudiante;

UPDATE Estudiante SET comidas_favoritas = {"shucos de la U","aaaeliminar"};
UPDATE Estudiante SET comidas_favoritas = comidas_favoritas + {"pollo en crema","kakik","pizza","taco-bell","flor de izote"} WHERE carnet == 201503712;
UPDATE Estudiante SET comidas_favoritas = comidas_favoritas - {"flor de izote"};

ALTER TABLE Estudiante ADD 
	numeros_favoritos LIST<INT>;

UPDATE Estudiante SET numeros_favoritos = [0];
UPDATE Estudiante SET numeros_favoritos = numeros_favoritos + [7,9], numeros_favoritos = numeros_favoritos - [0] 
	WHERE carnet == 201503712;

UPDATE Estudiante SET comidas_favoritas[0] = "Aguacate";
UPDATE Estudiante SET pet = new Mascota;
UPDATE Estudiante SET pet.raza = "Electrico";

DELETE comidas_favoritas[0] FROM Estudiante;

//SELECT * FROM Estudiante;
//SELECT nombres,pet.raza,numeros_favoritos FROM Estudiante;

//int @conteo = SUM(<<SELECT carnet from Estudiante>>);
//log(@conteo);

Procedure getCursor(),(CURSOR @cur){
	log("hola estoy dentro de esta mierda D:");
	CURSOR @cc is SELECT nombres FROM Estudiante;
	return @cc;
}

CURSOR @ccc = CALL getCursor();
OPEN @ccc;
FOR EACH(String @nombres) IN @ccc{
	log(@nombres);
}
CLOSE @ccc;

BEGIN BATCH
INSERT INTO Estudiante(nombres,carnet) values("Esto no se guardará",200000);
INSERT INTO Estudiante(nombres,carnet) values("Esto no se guardará",200001);
INSERT INTO Estudiante(nombres,carnet) values("Esto no se guardará",200002);
INSERT INTO Estudiante values("Esto no se guardará",200003);
APPLY BATCH;

SELECT * FROM Estudiante Order by carnet asc;

log(count(<<SELECT * FROM Estudiante LIMIT 4>>));

Cursor @cursor1, @cursor2, @cursor3;

Procedure retornoMultiple(),(Cursor @c1, Cursor @c2, Cursor @c3){
	Cursor @n is select nombres,carnet from Estudiante where nombres == "Oscar René";
	Cursor @n2 is select nombres,carnet from Estudiante limit 5;
	Cursor @n3 is select nombres,carnet from Estudiante limit 10;
	return @n, @n2, @n3;
}

@cursor1, @cursor2, @cursor3 = call retornoMultiple();

try{
	OPEN @cursor1;
}catch(Exception @e){
	LOG("======= CATCH CAPTURADO ======");
	LOG(@e.message);
}
LOG("==================== IMPRIMIENDO CURSOR 1 =================");
for each(String @name, int @carnet) in @cursor1{
	LOG("Nombre: "+@name+" -- Carnet: "+@carnet);
}
CLOSE @cursor1;

OPEN @cursor2;
LOG("==================== IMPRIMIENDO CURSOR 2 =================");
for each(String @name, int @carnet) in @cursor2{
	LOG("Nombre: "+@name+" -- Carnet: "+@carnet);
}
CLOSE @cursor2;

OPEN @cursor3;
LOG("==================== IMPRIMIENDO CURSOR 3 =================");
for each(String @name, int @carnet) in @cursor3{
	LOG("Nombre: "+@name+" -- Carnet: "+@carnet);
}
CLOSE @cursor3;

create database db;
use db;

Procedure orderList(boolean @asc_),()
{
 	List @arr = [5,6,4,7,3,8,2,9,1,10];
	if(@asc_){
	    for (int @i = 0; @i < @arr.size(); @i++) {
	        for (int @j = 0; @j < @arr.size()-@i-1; @j++) 
	        {
	            if (@arr.get(@j) > @arr.get(@j+1)) 
	            { 
	                // intercambia las posiciones
	                int @temp = @arr.get(@j); 
	                @arr.set(@j,@arr.get(@j+1)); 
	                @arr.set(@j+1,@temp);
	            } 
	        }
	    }
	}else{
	    for (int @i = 0; @i < @arr.size(); @i++) {
	        for (int @j = 0; @j < @arr.size()-@i-1; @j++) 
	        {
	            if (@arr.get(@j) < @arr.get(@j+1)) 
	            { 
	                // intercambia las posiciones
	                int @temp = @arr.get(@j); 
	                @arr.set(@j,@arr.get(@j+1)); 
	                @arr.set(@j+1,@temp);
	            } 
	        }
	    }
	}

	for(int @cont =0;@cont<@arr.size(); @cont++){
	    log(@arr.get(@cont));
	}    
}

call orderList(true);

