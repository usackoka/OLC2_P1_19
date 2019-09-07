CREATE TABLE Persona(
	nombre STRING,
	edad inT,
	hora_creado TIME,
	fecha_creado DATE,
	sexo BOOLEAN
);

INSERT INTO Persona VALUES("Oscar Cuéllar",22,NOW(),Today(),true);
INSERT INTO Persona VALUES("David Alcazar",22,NOW(),Today(),true);
INSERT INTO Persona VALUES("Albita Chinchilla",22,NOW(),Today(),false);

commit;