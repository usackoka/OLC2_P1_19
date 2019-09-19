CREATE DATABASE dbListaSimple;

use dbListaSimple;

create type if not exists Nodo(
	siguiente Nodo,
	anterior Nodo,
	valor String
);

Nodo addPrimero(Nodo @cabeza, String @obj)
{
    Nodo @temp = @cabeza;
    //nodo Nuevo
    Nodo @nuevo = new Nodo;
    @nuevo.valor = @obj;

    @nuevo.siguiente = @temp;
    @temp.anterior = @nuevo;
    @cabeza = @nuevo;
    return @cabeza;
}

String stringLista(Nodo @cabeza){
	String @recorrido = "";
	@recorrido += "Valor: "+@cabeza.valor+"\n";
	while(@cabeza.siguiente!=null){
		@cabeza = @cabeza.siguiente;
		@recorrido += "Valor: "+@cabeza.valor+"\n";
	}
	return @recorrido;
}

Nodo @lista = new Nodo;
@lista.valor = "Raiz";
@lista = addPrimero(@lista, "Primer valor insertado");
@lista = addPrimero(@lista, "Segundo valor insertado");
@lista = addPrimero(@lista, "Tercer valor insertado");
log(stringLista(@lista));

//============================== CARGA DE PROCEDURES ================================
create database db;
use db;

procedure imprimirMensajes(String @msm1, String @msm2),(){
	log(@msm1);
	log(@msm2);
}

commit;

use db;

call imprimirMensajes("Hola mensaje1","Hola mensaje2");