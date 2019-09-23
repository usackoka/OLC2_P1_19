create database exceptions;
create database exceptIOns3;
create database exceptIOns4;
create database exceptIOns5;
create user exc1 with password "1234";
create user exc3 with password "1234";
create user exc4 with password "1234";
create user exc5 with password "1234";
grant exc5 on exceptIOns3;
grant exc5 on exceptIOns4;

use exceptions;

//=========== IndexOutException
List<int> @hola = [1,2,3,4,5,6];
List<int> @hola2 = [1,2,3,4,5,6,7];

try{
	for(int @i; @i<@hola2.size(); @i+=1){
		@hola.set(@i,@hola2.get(@i));
	}
	LOG("======== No se capturó IndexOutException :( ==========");
}catch(IndexOutException @e){
	LOG("Capturado "+@e.message);
}

try{
	func1();
	LOG("======== No se capturó IndexOutException2 :( ==========");
}catch(IndexOutException @e){
	LOG("Capturado "+@e.message);
}

int func1(){
	int @index = -1;
	if(@index<0){
		throw new IndexOutException;
	}else{
		return @hola2.get(@index);
	}
}

//========== TypeAlreadyExists 
create type Except(
	nombre string
);

try{
	create type Except(
		nombre int
	);
	LOG("======== No se capturó TypeAlreadyExists :( ==========");
}catch(TypeAlreadyExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== TypeDontExists 
try{
	Kokoa2 @k = new Kokoa2;
	LOG("======== No se capturó TypeDontExists :( ==========");
}catch(TypeDontExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== BDAlreadyExists 
try{
	create database exceptIOns;
	LOG("======== No se capturó BDAlreadyExists :( ==========");
}catch(BDAlreadyExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== BDDontExists   
try{
	use exceptIOnss;
	LOG("======== No se capturó BDDontExists :( ==========");
}catch(BDDontExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== BDDontExists   
try{
	use exceptIOnss;
	LOG("======== No se capturó BDDontExists :( ==========");
}catch(BDDontExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== TableAlreadyExists
create table TablaExceptions(
	id Counter Primary Key
);

create table TablaExceptions2(
	valor1 string,
	valor2 int,
	valor3 boolean,
	valor4 date
);

try{
	create table TablaExceptions(
		hola string
	);
	LOG("======== No se capturó TableAlreadyExists :( ==========");
}catch(TableAlreadyExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========= CounterTypeException
try{
	insert into TablaExceptions(hola) values(1);
	LOG("======== No se capturó CounterTypeException :( ==========");
}catch(CounterTypeException @e){
	LOG("Capturado NICE "+@e.message);
}

//========= UserAlreadyExists 
try{
	create user exc4 with password "1111";
	LOG("======== No se capturó UserAlreadyExists :( ==========");
}catch(UserAlreadyExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========= UserDontExists 
try{
	revoke exc0 on exceptIOns3;
	LOG("======== No se capturó UserDontExists :( ==========");
}catch(UserDontExists @e){
	LOG("Capturado NICE "+@e.message);
}

//========== ValuesException 
try{
	insert into TablaExceptions2 values("hola",1,true);
	LOG("======== No se capturó ValuesException :( ==========");
}catch(ValuesException @e){
	LOG("Capturado NICE "+@e.message);
}

//========== ColumnException
try{
	insert into TablaExceptions2(valor1,valor2,valor5) values("hola",1,true);
	LOG("======== No se capturó ColumnException :( ==========");
}catch(ColumnException @e){
	LOG("Capturado NICE "+@e.message);
}

//========== BatchException
try{
	begin batch
		insert into TablaExceptions2(valor1,valor2,valor5) values("hola",1,true);
	apply batch;
	LOG("======== No se capturó BatchException :( ==========");
}catch(BatchException @e){
	LOG("Capturado NICE "+@e.message);
}

//========== ArithmeticException
try{
	func2();
	LOG("======== No se capturó ArithmeticException :( ==========");
}catch(ArithmeticException @e){
	LOG("Capturado NICE "+@e.message);
}

int func2(){
	throw new ArithmeticException;
}

//========== NullPointerException 
try{
	Except @obj = null;
	@obj.nombre = "hola";
	LOG("======== No se capturó NullPointerException :( ==========");
}catch(NullPointerException @e){
	LOG("Capturado NICE "+@e.message);
}

//========== FunctionAlreadyExists 
try{
	func3();
	LOG("======== No se capturó FunctionAlreadyExists :( ==========");
}catch(FunctionAlreadyExists @e){
	LOG("Capturado NICE "+@e.message);
}

int func3(){
	throw new FunctionAlreadyExists;
}

//========== ObjectAlreadyExists  
try{
	func4();
	LOG("======== No se capturó ObjectAlreadyExists  :( ==========");
}catch(ObjectAlreadyExists  @e){
	LOG("Capturado NICE "+@e.message);
}

int func4(){
	throw new ObjectAlreadyExists ;
}

try{
	Except @hola = new Except;
	Except @hoLA = new Except;
	LOG("======== No se capturó ObjectAlreadyExists2  :( ==========");
}catch(ObjectAlreadyExists  @e){
	LOG("Capturado NICE "+@e.message);
}

//========================= ESTOS HAY QUE INICIAR SESION CON OTRO USER PARA PROBARLOS
//===== UseBDException  
