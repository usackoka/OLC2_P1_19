//=======================Metodos LIST ================================
List @varList = new List<String>;
@varList.insert("Hola");
@varList.insert(" ");
@varList.insert("amigos");
@varList.insert(" ");
@varList.insert(":3");
@varList.insert(":O");

@varList.remove(5);

String @salida = "";
for(int @i = 0; @i<@varList.size(); @i++){
	@salida += @varList.get(@i);
}

if(@varList.contains(":O")){
	log("Está malo chico :c");
}else{
	log("Todo bien, todo correcto");
}

@varList.clear();
@varList.insert("Hola");
@varList.insert(" ");
@varList.insert("Putos");
@varList.insert(" ");
@varList.insert(":3");
@varList.set(4,":v");

@salida = "";
for(int @i = 0; @i<@varList.size(); @i++){
	@salida += @varList.get(@i);
}

if(@salida != "Hola Putos :v"){
	log("Está malo chico :c");
}else{
	log("Todo bien, todo correcto");
}

//========================= Metodos set ===============================
Set @varSet = new Set<String>;
@varSet.insert("durazno");
@varSet.insert("mango");
@varSet.insert("melocoton");
@varSet.insert("higo");
@varSet.insert("limon");
@varSet.insert("papaya");

@varSet.remove(5);

@salida = "";
for(int @i = 0; @i<@varSet.size(); @i++){
	@salida += @varSet.get(@i)+" ";
}

log("Salida varSet: "+@salida); //debería mostrar los elementos ordenados

@varSet.insert("limon"); //debería mostrar un error
@varSet.insert("uva");
@varSet.insert("sandia");

@salida = "";
for(int @i = 0; @i<@varSet.size(); @i++){
	@salida += @varSet.get(@i)+" ";
}

log("Salida varSet: "+@salida); //debería mostrar los elementos ordenados

//======================== Metodos MAP ====================================
Map @varMap = new Map<String, int>;
@varMap.insert("clave 1",1);
@varMap.insert("clave 2",2);
@varMap.insert("clave 3",3);
@varMap.insert("clave 4",4);

@salida = "";
for(int @i = 1; @i<@varMap.size()+1; @i++){
	@salida += @varMap.get("clave "+@i)+" ";
}

log("Salida varMap: "+@salida); //debería mostrar los numeros nada mas

//====================== Avanzado MAP ===================================
Map @varMap2 = new Map<int,List>;
List @varList2 = new List<String>, @varList3 = new List<String>;

@varList2.insert("Lista2: valor 1");
@varList2.insert("Lista2: valor 2");
@varList3.insert("Lista3: valor 1");
@varList3.insert("Lista3: valor 2");

@varMap2.insert(0,@varList2);
@varMap2.insert(1,@varList3);

log(@varMap2.get(0).get(0));
log(@varMap2.get(1).get(0));

//==================== LIST MODO2 =================================
List @varListM2 = new List<String>;
@salida = "";
@varListM2 = ["val1","val2","val3","val4"];
for(int @i = 0; @i<@varListM2.size(); @i++){
	@salida += @varListM2.get(@i)+" ";
}
log("salida: "+@salida);

//==================== SET MODO2 =================================
Set @varSetM2 = new Set<String>;
@salida = "";
@varSetM2 = {"val1","val2","val3","val4"};
for(int @i = 0; @i<@varSetM2.size(); @i++){
	@salida += @varSetM2.get(@i)+" ";
}
log("salida: "+@salida);

//=================== MAP MODO2 ==================================
MAP @varMapM2 = new Map<int,String>;
@varMapM2 = [0:"valor 0",1:"valor 1",2:"valor 2"];
for(int @i = 0; @i<@varMapM2.size(); @i++){
	log(@varMapM2.get(@i));
}