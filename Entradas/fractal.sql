double toRadians(double @angle){
    lOG("Recibo en toRadians @angle: "+@angle);
    return @angle*3.141592653589793/180;
}

double pow(double @base, int @exponente){
    lOG("Recibo en pow @base: "+@base);
    lOG("Recibo en pow @exponente: "+@exponente);
    return @base**@exponente;
}

double sine(double @x){
    lOG("Recibo en sine @x: "+@x);
    double @sin=0.0;
    int @fact;
    for(int @i=1; @i<=10; @i++){
        @fact=1;
        for(int @j=1; @j<=2*@i-1; @j++){
             @fact=@fact*@j;
        }
        @sin=@sin+(pow((double)@x,(int)(2*@i-1))/@fact);
        
    }
    return @sin;
}

int drawTree(double @x1, double @y1, double @angle, int @depth) {
    lOG("Recibo en drawTree @x1: "+@x1);
    lOG("Recibo en drawTree @y1: "+@y1);
    lOG("Recibo en drawTree @angle: "+@angle);
    lOG("Recibo en drawTree @depth: "+@depth);
	if (@depth == 0) {
        return 0;
    }
    double @x2 = @x1 + (sine(3.141592653589793 / 2 + toRadians(@angle)) * @depth * 10.0);
    double @y2 = @y1 + (sine(toRadians(@angle)) * @depth * 10.0);
    LOG(@x1 + " " + @y1 + " " + @x2 + " " + @y2 + "\n");
    drawTree(@x2, @y2, @angle - 20, @depth - 1);
    drawTree(@x2, @y2, @angle + 20, @depth - 1);
    return 1;
}

int main() {
	drawTree(250.0, 500.0, -90.0, 9);
    return 0;
}

main();

List<int> @n = [1,2,3,4,5,6];

if(4 in {1,2,3,4,5}){
    log("correcto");
}else{
    log("incorrecto");
}

