Create type Estudiante(        
	carnet int,         
	nombre String
); 

Estudiante @est, @prueba1, @prueba2;                                // Crea la declaración del objeto 
@est = new Estudiante;                          // Crea una instancia del objeto 
Estudiante @est2 = new Estudiante;              // Crea otra instancia del objeto 
//Estudiante @est3 = {201504481, "Julio Arango"}  // Crea otra instancia del objeto

Alter type Estudiante Delete(
        Carnet,
        direccion 
);

Delete type Estudiante;

@var = (int)(3+4);

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

//INSERT INTO Estudiante VALUES (  1, "Juan Valdez" , "Colombia") ;

//INSERT INTO Estudiante( id, Nombre) VALUES (  2, "Juan Valdez" ) ; 

UPDATE Estudiante  
	SET Nombre= 'Pao', 
        Edad=21  
WHERE Nombre=="Paola" && Edad<18; 
 
UPDATE UserActions 
   SET total = total + 2 
WHERE userID == "B70DE1D0-9908-4AE3-BE34-5573E5B09F14" 
	&& action == "click";

DELETE FROM Estudiante  
WHERE Nombre=="Julio" && Edad<18; 

DELETE FROM Estudiante;

COUNT( < SELECT hola FROM Estudiante >);

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

if(@var==1){
	log('hola');
}else if(@var==2){
	log('hola2');
}else{
	log('hola3');
}

switch(@variable){
	case 1:
		LOG("hola mundo");
		break;
	case 2:
		LOG("Hola mundo2");
		break;
	default:
		break;
}

Int factorial(int @n){       
	if (@n == 0) {             
		return 1;       
	} 
	else {
		return @n * factorial(n - 1);      
	} 
} 

Call Proc_ejemplo(1,2,3,4,5,"aaa"); 

Int @A; Int @B; 
 
@A, @B = Call Ejemplo_Procedure (1,2);

Procedure Ejemplo_Procedure(int @n, int @p), (int @retorno1, int @ret2){
       if (@n == 0) {
       	return 1, 2;
       } 
       else {           
        return @n, @n *2;      
       } 
}

log(3+4*2**2);

int @n1,@n2=3,@n4;
log("Esto debería dar 3: "+@n1+@n2);
log("Esto debería dar 0: "+@n1+@n4);

boolean @b1 = true;
if(@b1){
	log("if super básico");
}else{
	log("está mal campeon :c");
}


double @d1 = 4.0;

if(@d1==0.0){
	log("Aquí no");
}else if(@d1==4.1){
	log("aqui tampoco");
}else{
	log("aqui si");
}

int @nn1 = 40;
@nn1 = @nn1 - 1;
log(@nn1);
if(@nn1!=30){
	log("Se imprime esto con valor: "+@nn1);
}

int @nn1 = 40;
while(@nn1!=30){
	log("valor @nn1= "+@nn1);
	@nn1 = @nn1 - 1;
}

int @entero = 3+4+3;
switch(@entero){
	case 1:
	    log("Aqui no 1");
		break;
	case 2:
	    log("Aqui no 2");
		break;
	case 10:
		log("Aquí si");
	case 11:
		log("Aquí no");
	default:
		log("Aquí no 3");
		break;
}

log(prueba());
//log(factorial(5));

Int prueba(){
	int @var = 3;
	return @var>4?"Si es mayor":"No es mayor";
}

log(1+3*4+1);

log(recursiva(4));

int recursiva(int @valor){
	if(@valor!=1){
		return recursiva(@valor-1);
	}else{
		return @valor;
	}
}


log(factorial(5));
Int factorial(int @n){       
	if (@n == 0) {             
		return 1;       
	} 
	else {
		return @n * factorial(n - 1);      
	} 
}

log(ackerman(3,4));

int ackerman(int @m, int @n){
	if(@m==0){
		return @n+1;
	}
	if(@m>0 && @n==0){
		return ackerman(@m-1,1);
	}
	if(@m>0 && @n>0){
		return ackerman(@m-1,ackerman(@m,@n-1));
	}
}