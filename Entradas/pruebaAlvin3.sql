CREATE DATABASE Pruebas;

USE Pruebas;

CREATE TABLE PruebasAgregacion(
    
    indice counter PRIMARY KEY,
    randomNum int,
    nombre string,
    carnet int,
    apellido string
    );
    
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(10, "Abc");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(11, "DEF");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(12, "GHI");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(13, "JKL");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(14, "MNO");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(15, "PQR");
INSERT INTO PruebasAgregacion(randomNum, nombre) VALUES(16, "STU");

INSERT INTO PruebasAgregacion(randomNum, apellido) VALUES(17, "ApellidoX");
INSERT INTO PruebasAgregacion VALUES(18, "Alvin", 201602688, "Alegría");

CURSOR @AUX is SELECT * FROM PruebasAgregacion;
OPEN @AUX;
FOR EACH(int @indice, int @randomNum, string @nombre, int @carnet, string @apellido) in @AUX
{
    LOG("Usuario con indice "+@indice+" Nombre: "+@nombre+" Apellido: "+@apellido+" Carnet: "+@carnet+" randomNum: "+@randomNum+"\n");
}

/*SALIDA CORRECTA
Usuario con indice 0 Nombre: Abc Apellido: null Carnet: 0 randomNum: 10

Usuario con indice 1 Nombre: DEF Apellido: null Carnet: 0 randomNum: 11

Usuario con indice 2 Nombre: GHI Apellido: null Carnet: 0 randomNum: 12

Usuario con indice 3 Nombre: JKL Apellido: null Carnet: 0 randomNum: 13

Usuario con indice 4 Nombre: MNO Apellido: null Carnet: 0 randomNum: 14

Usuario con indice 5 Nombre: PQR Apellido: null Carnet: 0 randomNum: 15

Usuario con indice 6 Nombre: STU Apellido: null Carnet: 0 randomNum: 16

Usuario con indice 7 Nombre: null Apellido: ApellidoX Carnet: 0 randomNum: 17

Usuario con indice 8 Nombre: Alvin Apellido: Alegría Carnet: 201602688 randomNum: 18*/



LOG("PRUEBAS UPDATE --------------------------------\n");
UPDATE PruebasAgregaciOn SET NoMbRe = "Alvin Emilio", aPeLlidO = "Alegría Hernández" WHERE indice <= 4 && randomNum >= 10;

CURSOR @AUX2 is SELECT * FROM PruebasAgregacion;
OPEN @AUX2;
FOR EACH(int @indice, int @randomNum, string @nombre, int @carnet, string @apellido) in @AUX2
{
    LOG("Usuario con indice "+@indice+" Nombre: "+@nombre+" Apellido: "+@apellido+" Carnet: "+@carnet+" randomNum: "+@randomNum+"\n");
}

/* SALIDA CORRECTA
Usuario con indice 0 Nombre: Alvin Emilio Apellido: Alegría Hernández Carnet: 0 randomNum: 10

Usuario con indice 1 Nombre: Alvin Emilio Apellido: Alegría Hernández Carnet: 0 randomNum: 11

Usuario con indice 2 Nombre: Alvin Emilio Apellido: Alegría Hernández Carnet: 0 randomNum: 12

Usuario con indice 3 Nombre: Alvin Emilio Apellido: Alegría Hernández Carnet: 0 randomNum: 13

Usuario con indice 4 Nombre: Alvin Emilio Apellido: Alegría Hernández Carnet: 0 randomNum: 14

Usuario con indice 5 Nombre: PQR Apellido: null Carnet: 0 randomNum: 15

Usuario con indice 6 Nombre: STU Apellido: null Carnet: 0 randomNum: 16

Usuario con indice 7 Nombre: null Apellido: ApellidoX Carnet: 0 randomNum: 17

Usuario con indice 8 Nombre: Alvin Apellido: Alegría Carnet: 201602688 randomNum: 18*/
LOG("PRUEBASS UPDATE 2 -------------------------");
UPDATE PruebasAgregaciOn SET NoMbRe = "No soy yo", aPeLlidO = nombre;

CURSOR @AUX2 is SELECT * FROM PruebasAgregacion;
OPEN @AUX2;
FOR EACH(int @indice, int @randomNum, string @nombre, int @carnet, string @apellido) in @AUX2
{
    LOG("Usuario con indice "+@indice+" Nombre: "+@nombre+" Apellido: "+@apellido+" Carnet: "+@carnet+" randomNum: "+@randomNum+"\n");
}


/*SALIDA CORRECTA 
Usuario con indice 0 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 10

Usuario con indice 1 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 11

Usuario con indice 2 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 12

Usuario con indice 3 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 13

Usuario con indice 4 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 14

Usuario con indice 5 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 15

Usuario con indice 6 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 16

Usuario con indice 7 Nombre: No soy yo Apellido: No soy yo Carnet: 0 randomNum: 17

Usuario con indice 8 Nombre: No soy yo Apellido: No soy yo Carnet: 201602688 randomNum: 18

*/

LOG("PRUEBAS ALTER TABLE");

ALTER TABLE PruebasAgregacion ADD
    dpi int,
    fechaNac Date;

CURSOR @AUX3 is SELECT * FROM PruebasAgregacion;
OPEN @AUX3;
FOR EACH(int @indice, int @randomNum, string @nombre, int @carnet, string @apellido, int dpi, Date fechaNac) in @AUX3
{
    LOG("Usuario con indice "+@indice+" Nombre: "+@nombre+" Apellido: "+@apellido+" Carnet: "+@carnet+" randomNum: "+@randomNum+" dpi: "+dpi+" fechaNac: "+((String)fechaNac)+"\n");
}    
/*SALIDA CORRECTA 
Usuario con indice 0 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 10 dpi: null fechaNac: null

Usuario con indice 1 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 11 dpi: null fechaNac: null

Usuario con indice 2 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 12 dpi: null fechaNac: null

Usuario con indice 3 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 13 dpi: null fechaNac: null

Usuario con indice 4 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 14 dpi: null fechaNac: null

Usuario con indice 5 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 15 dpi: null fechaNac: null

Usuario con indice 6 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 16 dpi: null fechaNac: null

Usuario con indice 7 Nombre: No soy yo Apellido: No soy yo Carnet: null randomNum: 17 dpi: null fechaNac: null

Usuario con indice 8 Nombre: No soy yo Apellido: No soy yo Carnet: 201602688 randomNum: 18 dpi: null fechaNac: null*/

LOG("PRUEBAS ALTER TABLE 2 ----------------------");

ALTER TABLE PruebasAgregacion DROP 
    randomNum;
    
/*CURSOR @AUX4 is SELECT * FROM PruebasAgregacion;
OPEN @AUX4;
FOR EACH(int @indice, int @randomNum, string @nombre, int @carnet, string @apellido, int dpi, Date fechaNac) in @AUX4
{
    LOG("Usuario con indice "+@indice+" Nombre: "+@nombre+" Apellido: "+@apellido+" Carnet: "+@carnet+" randomNum: "+@randomNum+" dpi: "+dpi+" fechaNac: "+((String)fechaNac)+"\n");
}  */


SELECT * FROM PruebasAgregacion;    //SALIDA EN TABLA HTML, no deberia aparecer el campo randomNum

/*SALIDA CORRECTA 
indice 	nombre 	carnet 	apellido 	dpi 	fechaNac
0 	No soy yo 	null 	No soy yo 	null 	null
1 	No soy yo 	null 	No soy yo 	null 	null
2 	No soy yo 	null 	No soy yo 	null 	null
3 	No soy yo 	null 	No soy yo 	null 	null
4 	No soy yo 	null 	No soy yo 	null 	null
5 	No soy yo 	null 	No soy yo 	null 	null
6 	No soy yo 	null 	No soy yo 	null 	null
7 	No soy yo 	null 	No soy yo 	null 	null
8 	No soy yo 	201602688 	No soy yo 	null 	null */

LOG("PRUEBAS UPDATE ASIGNACION DATE Y DELETE----------------\n");

UPDATE PruebasAgregacion SET feChAnAc = '1997-04-29', dpi = 123456 where indice == 0 ;

SELECT * FROM PruebasAgregacion;

/*SALIDA CORRECTA
indice 	nombre 	carnet 	apellido 	dpi 	fechaNac 	dpi 	fechaNac
0 	No soy yo 	null 	No soy yo 	123456 	'1997-4-29' 	null 	null
1 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
2 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
3 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
4 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
5 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
6 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
7 	No soy yo 	null 	No soy yo 	null 	null 	null 	null
8 	No soy yo 	201602688 	No soy yo 	null 	null 	null 	null  */

DELETE FROM PruebasAgregacion WHERE fechaNac < '2019-09-27';

SELECT * FROM PruebasAgregacion;


procedure getMultipleCursors(int @controlVar, boolean @state),()
{
    if(!@state && @controlVar == 5)
    {
        CURSOR @aux1 IS SELECT apellido FROM PruebasAgregacion;
        CURSOR @aux2 is SELECT @controlVar, dpi FROM PruebasAgregacion WHERE indice == 1;
        return @aux1, @aux2;
    }
    else
    {
        LOG("Ya no sale :c");
    }

}

Cursor @prueba1, @prueba2 = CALL getMultipleCursors(5, false);
OPEN @prueba1;
FOR EACH(string @apellido ) IN @prueba1
{
    LOG("Apellido ->"+@apellido);

}
OPEN @prueba2;
FOR EACH(int @controlVar, int @dpi ) IN @prueba2
{
    LOG("dpi ->"+@dpi+" - "+@controlVar);

}