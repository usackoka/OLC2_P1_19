create database UltimaPrueba;
use UltimaPrueba;

create table Estudiante(
	cod_estudiante counter,
	carnet int,
	nombre string,
	id counter,
	primary key(cod_estudiante,carnet)
);

insert into Estudiante(carnet,nombre) values(201503712,"Oscar Cuellar");

begin batch
	insert into Estudiante(carnet,nombre) values(201503666,"Miguel Angel Omar Ruano Roca");
	insert into Estudiante(carnet,nombre) values(201503121,"Julia Argentina Yuyi");
	insert into Estudiante values(201408486,"Sharolin Guadalupe lacunza");
	insert into Estudiante values(201504480,"David Andres Alcazar Escobar");
	insert into Estudiante values(201504028,"Rodney Estuardo Lopez Marroquin");
apply batch;

insert into Estudiante(carnet,nombre) values(201600000,"Alvin Alegría");

commit;

use UltimaPrueba;
delete nombre from Estudiante;
select * from Estudiante;
