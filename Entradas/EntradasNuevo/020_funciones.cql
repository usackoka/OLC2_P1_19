/**
    Funciones recursivas
*/

CREATE DATABASE Funciones;

use Funciones;

log("Potencia 4,2: "+potencia((double)4,(double)2));
log("Factorial 10: "+factorial(10));
log("Modulo 35/6: "+modulo((double)35,(double)6));
log("MCD 5,6: "+mcd(5,6));
log("Fibonacci 9: "+fibonaci(9));
log("Ackerman 3,3: "+ackermann(3,4));
log("hofstaderFemenina 10: "+hofstaderFemenina(10));
log("hofstaderMasculino 10: "+hofstaderMasculino(10));
log("Par 90: "+par(90));
log("Impar 90: "+impar(90));
Call hanoi(4, "1", "2", "3");

double potencia(double @base, double @exp) {
    return @exp == 0 ? 1 : @base * potencia(@base, @exp - 1);
}

int factorial(int @n) {
    return @n == 0 ? 1 : @n * factorial(@n - 1);
}

int modulo(double @n, double @p) {
    return @n < @p ? (int)@n : modulo(@n - @p, @p);
}

int mcd(int @a, int @b) {
    return @b == 0 ?  @a : mcd(@b, modulo((double)@a, (double)@b));
}

int fibonaci(int @n){
    return (@n==1 || @n==2) ? 1 : fibonaci(@n-1) + fibonaci(@n-2);
} 

int ackermann(int @m, int @n) {
    if (@m == 0) {
        return (@n + 1);
    } else if (@m > 0 && @n == 0) {
        return ackermann(@m - 1, 1);
    } else {
        return ackermann(@m - 1, ackermann(@m, @n - 1));
    }
}

procedure Hanoi(int @discos, String @origen, String @aux, String @destino), () {
    if (@discos == 1) {
        LOG("mover disco de " + @origen + " a " + @destino);
    } else {
        call Hanoi(@discos - 1, @origen, @destino, @aux);
        log("mover disco de " + @origen + " a " + @destino);
        call Hanoi(@discos - 1, @aux, @origen, @destino);
    }
}

int hofstaderFemenina(int @n) {
    if (@n < 0) {
        return 0;
    } else {
        return (@n == 0) ? 1 : @n - hofstaderFemenina(@n - 1);
    }
}

int hofstaderMasculino(int @n) {
    if (@n < 0) {
        return 0;
    } else {
        return @n == 0 ? 0 : @n - hofstaderMasculino(@n - 1);
    }
}

int par(int @nump) {
    if (@nump == 0) {
        return 1;
    }
    return impar(@nump - 1);
}

int impar(int @numi) {
    if (@numi == 0) {
        return 0;
    }
    return par(@numi - 1);
}