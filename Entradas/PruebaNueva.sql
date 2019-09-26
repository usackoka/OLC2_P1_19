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
    return @fecha;
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

String @nombres = "Estudiante: ";
String @carnet = "Carnet: ";

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

UPDATE Estudiante SET notas_cursos["compi2"] = 61;
UPDATE Estudiante SET notas_cursos = notas_cursos + ["logica":35,"Quimica 9":15,"mate1":99,"mate2":90,"biologia avanzada2":10];
UPDATE Estudiante SET notas_cursos = notas_cursos - {"Quimica 9","biologia avanzada2"};
//DELETE notas_cursos["filo 5"] FROM Estudiante;

Cursor @cursorAux2 is SELECT notas_cursos FROM Estudiante;
OPEN @cursorAux2;
FOR EACH(MAP @notas_cursos) in @cursorAux2
{
    log("Nota compi2->"+@notas_cursos.get("logica")+"\n");
}
CLOSE @cursorAux2;

UPDATE Estudiante SET comidas_favoritas = {"shucos de la U","aaaeliminar"};
UPDATE Estudiante SET comidas_favoritas = comidas_favoritas + {"pollo en crema","kakik","pizza","taco-bell","flor de izote"} WHERE carnet == 201503712;
UPDATE Estudiante SET comidas_favoritas = comidas_favoritas + {"flor de izote"};

Cursor @cursorAux3 is SELECT carnet, comidas_favoritas FROM Estudiante;
OPEN @cursorAux3;
FOR EACH(int @carnet, SET @comidas) in @cursorAux3
{
    log("Para el carnet # ->"+@carnet+", su comida favorita en 0 es ->"+@comidas.get(2)+"\n");
}
CLOSE @cursorAux3;

ALTER TABLE Estudiante ADD 
    numeros_favoritos LIST<INT>;

UPDATE Estudiante SET numeros_favoritos = [0];
UPDATE Estudiante SET numeros_favoritos = numeros_favoritos + [7,9], numeros_favoritos = numeros_favoritos - [0] 
    WHERE carnet == 201503712;
    
Cursor @cursorAux4 is SELECT carnet, numeros_favoritos FROM Estudiante;
OPEN @cursorAux4;
FOR EACH(int @carnet, LIST @numeros) in @cursorAux4
{
    log("Para el carnet # ->"+@carnet+", su numero favorito en 0 es ->"+@numeros.get(0)+"\n");
}
CLOSE @cursorAux4;

UPDATE Estudiante SET comidas_favoritas[0] = "Aguacate";
UPDATE Estudiante SET pet = new Mascota;
UPDATE Estudiante SET pet.raza = "Electrico";
UPDATE Estudiante SET comidas_favoritas[0] = "Pan" WHERE nombres == "Mitchel Andrea";

Cursor @cursorAux5 is SELECT carnet, pet, comidas_favoritas FROM Estudiante;
OPEN @cursorAux5;
FOR EACH(int @carnet, Mascota @pet, SET @comidas) in @cursorAux5
{
    log("El carnet #"+@carnet+" tiene como pet favorita uno de raza ->"+@pet.raza+" y el maricon cambió de gustos ->"+@comidas.get(0)+"\n");
}
close @cursorAux5;


int @conteo = SUM(<<SELECT carnet from Estudiante>>);
log("Conteo segun SUM es ->"+@conteo+" pero las matematicas me dice que es "+(201503712+201513629+201504028+201408486+201504480+201503865+201318570+201602929+201503666+201504429));

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

//commit;

SELECT * FROM Estudiante LIMIT 4;

log(count(<<SELECT * FROM Estudiante LIMIT 4>>));