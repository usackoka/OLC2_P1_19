
double @suma = 0.0;
List<int> @miArreglo = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]; // arreglo a llenar
double @promedio = 0.0;
int @mayor = 0;
int @menor = 0;
int @cont;

int main(){
   vectores();
   return 0;
}

int PARA1(int @num){
if(@num < 15){
@suma = @suma + @miArreglo.get(@num);
if (@mayor < @miArreglo.get(@num)) {
    @mayor = @miArreglo.get(@num);
}
LOG ("Posicion [ " + @num + " ] Elemento: " + @miArreglo.get(@num));
@num++;
PARA1(@num);
}
   return 0;
}

int PARA2(int @num){
if(@num < 15){
    if (@menor > @miArreglo.get(@num)) {
        @menor = @miArreglo.get(@num);
    }
    @num++;
    PARA2(@num);
}
   return 0;
}


int PARA3(int @num1, int @num2){
if(@num1 < 15){
    @cont = 0;
    PARA4(@num1, @num2);
    LOG("El numero " + @miArreglo.get(@num1)     + " se repite " + @cont + " veces");
    @num2++;
    @num1++;
    PARA3(@num1, @num2);
}
   return 0;
}

int PARA4(int @num1, int @num2){
if(@num2 < 15){
    if (@miArreglo.get(@num1)    == @miArreglo.get(@num2)   ) {
        @cont++;
    }
    @num2++;
    PARA4(@num1, @num2);
}
   return 0;
}

int vectores(){
//valores negatidos Unarios
@promedio = 0.0;
int @x   = 0;
if ( 0.0 != @promedio){
    LOG ("Error no   voido nada... NO CALIFICAR....");
    return;
}else {
    LOG ("Inicio de almacemacenamiento de arreglos ...");
}

//Llenado de arreglo

@miArreglo.set(@x,	 20);
@x++    ;
@miArreglo.set(@x,	 15);
@x++    ;
@miArreglo.set(@x,	 11);
@x++    ;
@miArreglo.set(@x,	 8);
@x++    ;
@miArreglo.set(@x,	 99);
@x++    ;
@miArreglo.set(@x,	 -3);
@x++    ;
@miArreglo.set(@x,	 0);
@x++    ;
@miArreglo.set(@x,	 5);
@x++    ;
@miArreglo.set(@x,	 33);
@x++    ;
@miArreglo.set(@x,	 88);
@x++    ;
@miArreglo.set(@x,	 99);
@x++    ;
@miArreglo.set(@x,	 88);
@x++    ;
@miArreglo.set(@x,	 57);
@x++    ;
@miArreglo.set(@x,	 47);
@x++    ;
@miArreglo.set(@x,	 99);
@x++    ;
PARA1(0);
@menor = @mayor;
PARA2(0);
//@promedio
@promedio = @suma / 15;
LOG ("***              Salida              ***");
// contar las veces que se repite cada nÃºmero

PARA3(0,0);
LOG (" - La @suma es: " + @suma);
LOG (" - El @promedio es: " + (@promedio + 0.00587));
LOG (" - El numero @mayor es: " + @mayor);
LOG (" - El numero @menor es: " + @menor);
   return 0;
}

main();

/*
SALIDA:

Inicio de almacemacenamiento de arreglos ...
Poifcion [ 0 ] Elemento: 20
Poifcion [ 1 ] Elemento: 15
Poifcion [ 2 ] Elemento: 11
Poifcion [ 3 ] Elemento: 8
Poifcion [ 4 ] Elemento: 99
Poifcion [ 5 ] Elemento: -3
Poifcion [ 6 ] Elemento: 0
Poifcion [ 7 ] Elemento: 5
Poifcion [ 8 ] Elemento: 33
Poifcion [ 9 ] Elemento: 88
Poifcion [ 10 ] Elemento: 99
Poifcion [ 11 ] Elemento: 88
Poifcion [ 12 ] Elemento: 57
Poifcion [ 13 ] Elemento: 47
Poifcion [ 14 ] Elemento: 99
***              Salida              ***
El numero 20 se repite 1 veces
El numero 15 se repite 1 veces
El numero 11 se repite 1 veces
El numero 8 se repite 1 veces
El numero 99 se repite 3 veces
El numero -3 se repite 1 veces
El numero 0 se repite 1 veces
El numero 5 se repite 1 veces
El numero 33 se repite 1 veces
El numero 88 se repite 2 veces
El numero 99 se repite 2 veces
El numero 88 se repite 1 veces
El numero 57 se repite 1 veces
El numero 47 se repite 1 veces
El numero 99 se repite 1 veces
- La @suma es: 666
- El @promedio es: 44.40587
- El numero @mayor es: 99
- El numero @menor es: -3
*/