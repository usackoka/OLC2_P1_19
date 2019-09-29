create database hashTable;

use hashTable;

create type if not exists HashTable(
    nodosHash List<NodoHash>
); 

create type if not exists Reservacion(
    costo double,
    tiempo double,
    num_reservacion int,
    nombre_cliente String
);

create type if not exists NodoHash(
    llave int,
    calendario Calendario,
    value Reservacion,
    lista_planVuelo Lista
);

create type if not exists Nodo(
    valor String,
    siguiente Nodo,
    anterior Nodo
);

create type if not exists Lista(
    cabeza Nodo,
    size int
);

Create table Pais(
    nombre String
);

//================== paises a guardar ============================
INSERT INTO Pais values("Guatemala");
INSERT INTO Pais values("Francia");
INSERT INTO Pais values("Jamaica");
INSERT INTO Pais values("Ungria");
INSERT INTO Pais values("Arabia Saudita");
INSERT INTO Pais values("EEUU");
INSERT INTO Pais values("Bolivia");
INSERT INTO Pais values("Tus ojos bb");
INSERT INTO Pais values("Belice");
INSERT INTO Pais values("El Salvador");
INSERT INTO Pais values("Brasil");
INSERT INTO Pais values("Argentina");
INSERT INTO Pais values("Costa Rica");
INSERT INTO Pais values("Ecuador");
INSERT INTO Pais values("Colombia");
INSERT INTO Pais values("Guinea");
INSERT INTO Pais values("Sierra Leona");
INSERT INTO Pais values("Haití");
INSERT INTO Pais values("Panamá");
INSERT INTO Pais values("Canadá");

//Lista que representan los días
//Mapa representa las actividades que se realizan en determinada hora del día
//ejemplo Mapa[3] = {"correr","jugar"}; //se corre y se juega a las 3 am de ese día
create type if not exists Calendario(
    almanaque List<Map<int,Set<String>>>
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

int @rand=0;
int random(){
    return @rand++;
}

List<NodoHash> getNodos(){
    return @nodosHash;
}

 int addNodoHash(Reservacion @reservacion, Lista @lista_planDeVuelo){
    /*Número @random para la K*/
    /*el método nos devuelve un @random no @repetido para la K*/
    int @no_reservacion = getRandom();
    
    /*Preguntar si ya está ocupado mas del 50%*/
     if(is50percent()){
         reHash();
         LOG("Realizando reHash... nuevo num @primo: "+@nodosHash.size());
     }
    
    
    //sacamos una nueva @llave según la funcion hash
    int @llave = getKey(@no_reservacion);
    LOG("Key para el @nodoHash @llave: "+@llave);
    

    //creamos el @nodoHash
    NodoHash @nodoHash = new NodoHash;
    @nodoHash.lista_planVuelo = @lista_planDeVuelo;
    @nodoHash.value = @reservacion;
    @nodoHash.value.num_reservacion = @no_reservacion;
    @nodoHash.llave = @llave;
    

    //metemos el @nodo en la tabla hash, en la posición "@llave"
    //si ya hay algo en esta posición, es una colisión
    if(@nodosHash.get(@llave)!=null){
        int @newKey = collision(@llave);
        @nodoHash.llave = @newKey;
        @nodosHash.set(@newKey,@nodoHash);
        LOG("Colision resolBida :v -> antigua @llave: "+@llave+" nueva @llave: "+@newKey);
    }else{
        @nodosHash.set(@llave,@nodoHash);
    }
    
    return 0;
}

 int collision(int @llave){
    int @newKey = @llave;
    while(@nodosHash.get(@newKey)!=null){
       @newKey++;
       if(@newKey>=@nodosHash.size()){
           @newKey = 0;
       }
    }
    return @newKey;
}

/*aumentar el tamaño de la tabla hash al siguiente número @primo*/
 int reHash(){
     /*El tamaño inicial será 43, 
    en caso de desbordamiento (suponga 50% de la capacidad de la tabla) 
    se aumentara el tamaño al siguiente número @primo. */
    int @size = @nodosHash.size();
    do{
        @size++;
    }while(!esPrimo(@size));
    
    //System.out.LOG(@size);
    List<NodoHash> @nodosTemp = @nodosHash;
    @nodosHash = new List<NodoHash>;
    for(int @i=0; @i<@size; @i++){
        @nodosHash.insert(null);
    }
    for (int @i = 0; @i < @nodosTemp.size(); @i++) {
        @nodosHash.set(@i,@nodosTemp.get(@i));
    }
}

 int getRandom(){
    int @random = 0;
    //verficar que no sea @repetido
    boolean @repetido = true;
    while(@repetido){
        @random = (int)(random() * 2000);
        @repetido = false;
        for (int @i=0; @i<@nodosHash.size(); @i+=1) {
            NodoHash @nodo = @nodosHash.get(@i);
            if(@nodo!=null){
                if(@nodo.value.num_reservacion==@random){
                    @repetido=true;
                }
            }
        }
    }
    return @random;
}

 NodoHash getNodoHash(int @no_reservacion){
    for (int @i=0; @i<@nodosHash.size(); @i+=1){
        NodoHash @nodosHash = @nodosHash.get(@i);
        if (@nodoHash.value.num_reservacion == @no_reservacion){
            return @nodoHash;
        }
    }
    LOG("Error: No se encuentra el @numero de @reservacion: "+@no_reservacion);
    return new NodoHash;
}

 int getKey(int @k){
    /*Hasing por División.
    En este caso la función se calcula simplemente como h(@k) = @k mod M 
    usando el 0 como el primer índice de la tabla hash de tamaño M.*/
    return @k%@nodosHash.size();
}

 boolean is50percent(){
    int @cont=0;
    for (int @i=0; @i<@nodosHash.size(); @i++) {
        NodoHash @nodo = @nodosHash.get(@i);
        if (@nodo != null) {
            @cont++;
        }
    }
    //LOG("========= CONTADOR = "+@cont+" y division = "+(@nodosHash.size()/2)+" y bool = "+( @cont>=(@nodosHash.size()/2))+"===========");
    return @cont>=(@nodosHash.size()/2);
    //return false;
}

 boolean esPrimo(int @numero){
    //si es par, no es @primo, mas óptimo
    if (@numero%2==0){
        return false;
    }
    
    int @contador = 2;
    boolean @primo=true;
    while ((@primo) && (@contador!=@numero)){
        if (@numero % @contador == 0){
            @primo = false;
        }
        @contador++;
    }
    return @primo;
}

 String recorrido(){
    String @recorrido = "";
    
    @recorrido = @recorrido +  "digraph g{\n" +
        "    rankdir=LR;\n" +
        "    node [shape=record];";
    
    @recorrido = @recorrido +  "hash [label = \"";
    for (int @i=0; @i<@nodosHash.size(); @i+=1) {
        NodoHash @nodoHash = @nodosHash.get(@i);
        if(@nodoHash!=null){
            @recorrido = @recorrido +  "| <"+@nodoHash.llave+"> Key: "+@nodoHash.llave+"\nTiempo: "+@nodoHash.value.tiempo+
                " Costo: "+@nodoHash.value.costo+"\nNombre Cliente: "+@nodoHash.value.nombre_cliente;
        }else{
            @recorrido = @recorrido +  "";
        }
    }
    @recorrido = @recorrido +  "\"];\n\n";
    
    int @cont1 = 0;
    for (int @i=0; @i<@nodosHash.size(); @i+=1) {
        NodoHash @nodoHash = @nodosHash.get(@i);
        if(@nodoHash!=null){
            Nodo @raiz = @nodoHash.lista_planVuelo.cabeza;
            int @cont2 = @cont1;
            while(@raiz != null){
                @recorrido = @recorrido +  "nodo"+(@cont2++)+" ";
                @recorrido = @recorrido +  "[label = \"{<izq> | "+@raiz.valor+" |<der>}\"];\n";
                @raiz = @raiz.siguiente;
            }

            for (int @i = @cont1; @i < @cont2; @i++) {
                if(@i==@cont1){
                    @recorrido = @recorrido +  "hash:"+@nodoHash.llave+" -> nodo"+@i+":izq;\n";
                }
                if(@i+1!=@cont2){
                    @recorrido = @recorrido +  "nodo"+@i+":der -> nodo"+(@i+1)+":izq;\n";
                }
            }
            @cont1 = @cont2;
            @cont1++;
            @recorrido = @recorrido +  "\n\n";
        }
    }
    @recorrido = @recorrido +  "\n\n}";
    return @recorrido;
}

//=========== main =================
HashTable @hashTable = new HashTable;
//tamaño incial = 43
List<NodoHash> @nodosHash = @hashTable.nodosHash;
@nodosHash = new List<NodoHash>;
for (int @i; @i<43; @i+=1) {
    @nodosHash.insert(null);
}
LOG("================ EMPIEZA CREACION DE LA HASH =================");
Cursor @cur is Select * FROM Pais;
OPEN @cur;
for (int @i = 0; @i < 70; @i++) {
    Lista @lista = new Lista;
    int @n = 0;
    int @n2 = @i;
    for each(String @pais) in @cur{
        if(@n++ == @n2){
            addPrimero(@lista,@pais);
        }
    }
    Reservacion @res = new Reservacion;
    @res.nombre_cliente = "Kokita bb <3";
    addNodoHash(@res, @lista);
}
CLOSE @cur;
LOG("================= EMPIEZA GRAFICA DE LA HASH ==================");
LOG(recorrido());
//==================================


