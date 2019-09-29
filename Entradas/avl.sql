create dataBase avl;
use avl;

int @contador = 0;

create type NodoAVL(
	valor int,
	izquierdo NodoAVL,
	derecho NodoAVL,
	altura int,
	id int
);

create type ArbolAVL(
	raiz NodoAVL
);

int insertar(int @valor){
	@arbol.raiz = insertar(@valor, @arbol.raiz);
	return 0;
}

NodoAVL insertar(int @valor, NodoAVL @raiz) {
    //Si en nodo recibido fuera nulo entonces el nuevo nodo se puede insertar 
    //en esa posición y se terminan las llamadas recursivas a este método.
    if (@raiz == null) {
        @raiz = new NodoAVL;
        @raiz.valor = @valor;
        @raiz.id = @contador++;
        //Si el nuevo @valor fuera menor que el nodo de actual entonces
    } else if (@valor < @raiz.valor) {
        //Se llama recursivamente al método para explorar el subarbol izquierdo
        //porque el @valor a insertar es menor que el del nodo actual.
        @raiz.izquierdo = insertar(@valor, @raiz.izquierdo);
        if (altura(@raiz.derecho) - altura(@raiz.izquierdo) == -2) //Si el factor de equilibrio esta desbalanceado, hay que hacer 
        //rotación de nodos, como el fe=-2 hay dos posibilidades de 
        //rotación dependiendo de:
        {
            if (@valor < @raiz.izquierdo.valor) //Si el nuevo @valor fuera menor que la izquierda del nodo des-
            //balanceado, se sabe que el nuevo nodo será insertado a la 
            //izquierda de la actual izquierda, entonces tenemos una rotación 
            //simple por la izquierda o sea una IzquierdaIzquierda.
            {
                @raiz = IzquierdaIzquierda(@raiz);
            } else //de lo contrario, se sabe que el nuevo nodo será insertado 
            //a la derecha del la actual izquierda, por lo que se tiene 
            //un caso de rotación doble por la izquierda 
            //o sea una IzquierdaDerecha.
            {
                @raiz = IzquierdaDerecha(@raiz);
            }
        }
    } else if (@valor > @raiz.valor) //Si el nuevo @valor fuera mayor que el nodo de la actual entonces:
    {
        //Se llama recursivamente al método para explorar el subarbol derecho
        //porque el @valor a insertar es mayor que el del nodo actual.            
        @raiz.derecho = insertar(@valor, @raiz.derecho);
        if (altura(@raiz.derecho) - altura(@raiz.izquierdo) == 2) //Si el factor de equilibrio esta desbalanceado, hay que hacer 
        //rotación de nodos, como el fe=2 hay dos posibilidades de 
        //rotación dependiendo de:                
        {
            if (@valor > @raiz.derecho.valor) //Si el nuevo @valor fuera mayor que la derecha del nodo des-
            //balanceado, se sabe que el nuevo nodo será insertado a la 
            //derecha de la actual derecha, entonces tenemos una rotación 
            //simple por la derecha o sea una DerechaDerecha.                    
            {
                @raiz = DerechaDerecha(@raiz);
            } else //de lo contrario, se sabe que el nuevo nodo será insertado 
            //a la izquierda del la actual derecha, por lo que se tiene 
            //un caso de rotación doble por la derecha
            //o sea una DerechaIzquierda.
            {
                @raiz = DerechaIzquierda(@raiz);
            }
        }
    } else // De lo contrario signifca que el @valor que se quiere insertar ya existe, 
    //como no se permite la duplicidad de este dato no se hace nada.
    {
    	LOG("@valor: "+@valor+" @raiz.valor: "+@raiz.valor);
        LOG("No se permiten los @valores duplicados: \""
                + ((String)@valor) + "\".");
    }

    //finalmente, por cada llamada recursiva debe hacerse una reasignacion 
    //de la altura esta se hará hasta para los nodos que no cambiaron de nivel 
    //en el transcurso porque no hay forma de saber cuales cambiaron de nivel 
    //y cuales no. La altura,será la altura del hijo que tiene
    //la altura más grande, es decir, la rama mas profunda, más 1.
    @raiz.altura = mayor(altura(@raiz.izquierdo), altura(@raiz.derecho)) + 1;
    return @raiz;
}

int altura(NodoAVL @nodo) {
    if (@nodo == null) {
        return -1;
    } else {
        return @nodo.altura;
    }
}

int mayor(int @n1, int @n2) {
    return @n1 > @n2 ? @n1 : @n2;
}

NodoAVL IzquierdaIzquierda(NodoAVL @n1) {
    NodoAVL @n2 = @n1.izquierdo;
    @n1.izquierdo = @n2.derecho;
    @n2.derecho = @n1;
    @n1.altura = mayor(altura(@n1.izquierdo), altura(@n1.derecho)) + 1;
    @n2.altura = mayor(altura(@n2.izquierdo), @n1.altura) + 1;
    return @n2;
}

NodoAVL DerechaDerecha(NodoAVL @n1) {
    NodoAVL @n2 = @n1.derecho;
    @n1.derecho = @n2.izquierdo;
    @n2.izquierdo = @n1;
    @n1.altura = mayor(altura(@n1.izquierdo), altura(@n1.derecho)) + 1;
    @n2.altura = mayor(altura(@n2.derecho), @n1.altura) + 1;
    return @n2;
}

NodoAVL IzquierdaDerecha(NodoAVL @n1) {
    @n1.izquierdo = DerechaDerecha(@n1.izquierdo);
    return IzquierdaIzquierda(@n1);
}

NodoAVL DerechaIzquierda(NodoAVL @n1) {
    @n1.derecho = IzquierdaIzquierda(@n1.derecho);
    return DerechaDerecha(@n1);
}

int inorden() {
    LOG("Recorrido inorden del árbol binario de búsqueda:");
    call inorden(@arbol.raiz);
    LOG("");
    return 0;
}

Procedure inorden(NodoAVL @a),(int @ret) {
    if (@a == null) {
        return 0;
    }
    call inorden(@a.izquierdo);
    LOG(@a.valor + ",");
    call inorden(@a.derecho);
    return 0;
}

String getDot(NodoAVL @raiz) {
    return "digraph grafica{\n"
            + "rankdir=TB;\n"
            + "node [shape = record, style=filled, fillcolor=seashell2];\n"
            + getDotNodosInternos(@raiz)
            + "}\n";
}

String getDotNodosInternos(NodoAVL @raiz) {
	NodoAVL @izquierdo = @raiz.izquierdo;
	NodoAVL @derecho = @raiz.derecho;
    String @etiqueta;
    if (@izquierdo == null && @derecho == null) {
        @etiqueta = "nodo" + @raiz.id + " [ label =\"" + @raiz.valor + "\"];\n";
    } else {
        @etiqueta = "nodo" + @raiz.id + " [ label =\"<C0>|" + @raiz.valor + "|<C1>\"];\n";
    }
    if (@izquierdo != null) {
        @etiqueta = @etiqueta + getDotNodosInternos(@izquierdo)
                + "nodo" + @raiz.id + ":C0->nodo" + @izquierdo.id + "\n";
    }
    if (@derecho != null) {
        @etiqueta = @etiqueta + getDotNodosInternos(@derecho)
                + "nodo" + @raiz.id + ":C1->nodo" + @derecho.id + "\n";
    }
    return @etiqueta;
}

CREATE TABLE Usuario(
	arbol ArbolAVL,
	nombre STRING,
	cod_usuario counter primary key
);

ArbolAVL @arbol = new ArbolAVL;
insertar(12);
insertar(5);
insertar(26);
insertar(33);
insertar(59);
insertar(27);
insertar(15);
insertar(47);
insertar(74);
insertar(84);
insertar(88);
insertar(90);
insertar(124);
insertar(612);
inorden();
LOG("============== GRAFICA =================");
LOG(getDot(@arbol.raiz));


INSERT into Usuario(arbol,nombre) values(@arbol,"Koka nnmms");
commit;


//================ luego de cargar el chison
/*
use avl;
Cursor @c IS SELECT * FROM Usuario;
OPEN @c;
for each(ArbolAVL @arbol, String @nombre, Counter @cod_usuario) in @c{
	LOG("============= IN ORDEN DEL ARBOL DE: "+@nombre+" cod: "+@cod_usuario);
	call inorden(@arbol.raiz);
}
*/
