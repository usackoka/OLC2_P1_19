Procedure Ejemplo_Procedure(int @n, int @p), (int @retorno1, int @ret2){
	if (@n == 0) {
		return 1, 2;
	}
	else {
		return @n, @n *2;      
	} 
} 

Procedure Ejemplo_Procedure(int @n, String @mensaje),(int @ret1, String @ret2){
	if (@n == 0) {
		return 1, "";
	}
	else {
		return @n*2,@mensaje+" Devuelto";      
	} 
}

Int @A,@B,@D;
String @C;
 
@A, @B = Call Ejemplo_Procedure (3,3);
@D, @C = Call Ejemplo_Procedure (3, "Mensaje");

log(@A);
log(@B);
log(@C);
log(@D);