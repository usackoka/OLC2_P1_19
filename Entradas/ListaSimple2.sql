create database ListaSimple;

use ListaSimple;

create type if not exists Nodo(
    valor String,
    siguiente Nodo,
    anterior Nodo
);

create type if not exists Lista(
    cabeza Nodo,
    size int
);

int imprimirLista(Lista @lista){
    for(int @i =0; @i<@lista.size; @i++){
        LOG("Valor "+@i+": "+getValorNodo(@lista,@i));
    }
    return 0;
}    

//este metodo agrega un nodo al principio de la @lista
 int addPrimero(Lista @lista, String @obj)
{
    if(@lista.cabeza == null)
    {
        @lista.cabeza = new Nodo;
        @lista.cabeza.valor = @obj;
    }
    else
    {
        Nodo @temp = @lista.cabeza;
        Nodo @nuevo = new Nodo;
        @nuevo.valor = @obj;
        @nuevo.siguiente = @temp;
        @temp.anterior = @nuevo;
        @lista.cabeza = @nuevo;
    }
    @lista.size = @lista.size + 1;
    return 0;
}
      
//da el valor que halla en el nodo que se le especifique en el parametro @index
 Nodo getNodo(Lista @lista, int @index)
{
    int @contador = 0;
    Nodo @temporal = @lista.cabeza;
    while(@contador < @index)
    {
        @temporal = @temporal.siguiente;
        @contador++;
    }
    return @temporal;
}

String getValorNodo(Lista @lista, int @index)
{
    int @contador = 0;
    Nodo @temporal = @lista.cabeza;
    while(@contador < @index)
    {
        @temporal = @temporal.siguiente;
        @contador ++;
    }
    return @temporal.valor;
}


//------------------------- LLAMADA -----------------------------
Lista @l = new Lista;
@l.size = 0;
addPrimero(@l,"Hola");
addPrimero(@l,"Hola2");
imprimirLista(@l);