List<int> @hola = [1,2,3,4,5,6];

try{
	@hola.get(10);
}catch(IndexOutException @e){
	LOG(@e.message());
}