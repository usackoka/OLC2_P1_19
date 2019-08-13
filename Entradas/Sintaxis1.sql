Create type Estudiante(        
	int carnet,         
	String nombre 
); 

Estudiante @est;                                // Crea la declaración del objeto 
@est = new Estudiante;                          // Crea una instancia del objeto 
Estudiante @est2 = new Estudiante;              // Crea otra instancia del objeto 
Estudiante @est3 = {201504481, "Julio Arango"}  // Crea otra instancia del objeto

Alter type Estudiante Delete(
        Carnet,
        direccion 
);

Delete type Estudiante;

(double)(3+4);

Create database if not exists base1;
Create database base2;

use base1;

drop DATABASE base1;

CREATE TABLE EspeciesDeMono (
  especie string PRIMARY KEY,
  nombre_comun string,  
  poblacion int,
  tamanio_promedio int 
);

CREATE TABLE Muro (
  usuario_id int,
  mes_publicado int,  
  hora_publicado double,  
  contenido string,  
  publicado_por string,  
  PRIMARY KEY (usuario_id, mes_publicado, hora_publicado) 
);

ALTER TABLE tabla1
DROP columna1, columna2;

ALTER TABLE tabla1
ADD columna1 INT, columna2 STRING;

ALTER TABLE tabla1
DROP columna1;

DROP TABLE IF EXISTS usuarios; 

DROP TABLE usuarios;

CREATE USER Pedro WITH PASSWORD "1234"; 

GRANT Pedro ON Prueba1; 

REVOKE Pedro ON Prueba1;

INSERT INTO Estudiante VALUES (  1, "Juan Valdez" , "Colombia") ;

INSERT INTO Estudiante( id, Nombre) VALUES (  2, "Juan Valdez" ) ; 

UPDATE Estudiante  
	SET Nombre= 'Pao', 
        Edad=21  
WHERE Nombre=="Paola" && Edad<18; 
 
UPDATE UserActions 
   SET total = total + 2 
WHERE userID = "B70DE1D0-9908-4AE3-BE34-5573E5B09F14" 
	&& action = "click";

DELETE FROM Estudiante  
WHERE Nombre=="Julio" && Edad<18; 

DELETE FROM Estudiante;

COUNT( < SELECT Id FROM Estudiante >);

SELECT alumno.carnet FROM Estudiante;

SELECT name, occupation FROM users 
	WHERE userid IN (199, 200, 207); 

SELECT tiempo, value 
FROM events 
WHERE event_type == "my event"
  && tiempo > '2011-02-03' 
  && tiempo <= '2012-01-01'
ORDER BY value ASC, tiempo DESC
LIMIT 5;

