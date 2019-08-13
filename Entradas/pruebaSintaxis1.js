Create type Estudiante(        
	int carnet,         
	String nombre 
); 

Estudiante @est;                                // Crea la declaración del objeto 
@est = new Estudiante;                          // Crea una instancia del objeto 
Estudiante @est2 = new Estudiante;              // Crea otra instancia del objeto 
Estudiante @est3 = {201504481, “Julio Arango”}  // Crea otra instancia del objeto

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



