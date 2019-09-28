int @size = 43;
do{
    @size++;
}while(!esPrimo(@size));

log(@size);

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