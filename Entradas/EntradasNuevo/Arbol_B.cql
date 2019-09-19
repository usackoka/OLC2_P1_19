Create database virtual;
use virtual;

Create type If not exists NodoB(
		Clave int,
		Nombre string,
		Izq NodoB,
		Der NodoB
);

NodoB @raiz = null;

boolean Insertar(int @clave, String @nombre){
	NodoB @nuevo = {@clave, @nombre, null, null} as NodoB;
	if(@raiz == null){
		@raiz = @nuevo;
	}else{
		try{
			NodoB @padre = null;
			NodoB @actual = @raiz;
			while(@actual != null){
				@padre = @actual;
				if(@actual.clave > @clave){
					@actual = @actual.izq;
				}else{
					@actual = @actual.der;
				}
			}
			if(@padre.clave > @clave){
				@padre.izq = @nuevo;
			}else{
				@padre.der = @nuevo;
			}
		}catch(Exception @e){
			return false;
		}
	}
	return true;
}

int preOrden(NodoB @actual){
	if(@actual != null){
		Log("Clave: " + @actual.clave + " Nombre: " + @actual.nombre + "\n");
		PreOrden(@actual.Izq);
		PreOrden(@actual.Der);
	}
	return 0;
}

int InOrden(NodoB @actual){
	if(@actual != null){
		InOrden(@actual.Izq);
		Log("Clave: " + @actual.clave + " Nombre: " + @actual.nombre + "\n");
		InOrden(@actual.Der);
	}
	return 0;
}

int PosOrden(NodoB @actual){
	if(@actual != null){
		PosOrden(@actual.Izq);
		PosOrden(@actual.Der);
		Log("Clave: " + @actual.clave + " Nombre: " + @actual.nombre + "\n");
	}
	return 0;
}

int Reemplazar(NodoB @actual){ 
 	NodoB @aux1 = @actual; 
 	NodoB @aux2 = @actual.izq; 
 	while(@aux2.der != null){ 
 		@aux1 = @aux2;
 		@aux2 = @aux2.der; 
 	} 
 	@actual.clave = @aux2.clave; 
 	if(@aux1 == @actual){
 		@aux1.izq = @aux2.izq;
 	}else{
 		@aux1.der = @aux2.izq;
 	}
 	return 1;
}

boolean Eliminar(int @elem){ 
	NodoB @Padre = @raiz; 
	NodoB @aux = @raiz;
	boolean @hijoIzq = true; 
	boolean @encontrado = false; 
	while(@aux != null){ 
		if(@aux.clave == @elem){ 
			@encontrado = true; 
			if(@aux==@raiz){ 
				if(@raiz.izq==null){
					@raiz = @raiz.der;
				}else if(@raiz.der==null){
					@raiz = @raiz.izq;
				}else{
					Reemplazar(@raiz);
				} 
			}else{ 
				Log("Encontrado...");
				if(@aux.izq == null){ 
					if(hijoIzq){
						@Padre.izq=@aux.der;
					}else{
						@Padre.der=@aux.der;
					}
				}else if(@aux.der == null){ 
					if(@hijoIzq){
						@Padre.izq=@aux.izq;
					}else{
						@Padre.der=@aux.izq;
					}
				}else{
					Reemplazar(@aux);
				}
			}
			@aux=null;
		}else{ 
			@Padre = @aux;
			if(@aux.valor > @elem){ 
				@aux=@aux.izq;
				@hijoIzq=true; 
			}else{ 
				@aux=@aux.der;
				@hijoIzq=false;
			}
		}
	}
	if(!@encontrado){
		Log("El elemento no existe...");
		return false;
	}
	return true;
}

Insertar(1,"Soy la clave 1");
Insertar(2,"Soy la clave 2");
Insertar(3,"Soy la clave 3");
Insertar(4,"Soy la clave 4");
log("============= PRE ORDEN =============");
preOrden(@raiz);
log("============= IN ORDEN =============");
InOrden(@raiz);
log("============= POS ORDEN =============");
PosOrden(@raiz);
log("======== ELIMINANDO CLAVE 3 =========");
Eliminar(3);
preOrden(@raiz);