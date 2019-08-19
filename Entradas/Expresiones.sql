
int @var1 = 1;
int @punteo = 0;

Inicio();

 int Inicio() {
    LOG("-----------------CALIFICACION-----------------");
    int @var1 = 0;
    //Verificar ámbitos, se toma con prioridad la variable local ante la global.
    if (@var1 != 0)
    {
        LOG("No se toma con prioridad la variable local ante la global");
        LOG("Perdiste 5 puntos :c");
    }
    else{
        @punteo = @punteo + 5;
    }

    //Sección de declaracion de variables
    Declaracion();
    
    /*
    //seccion de manejo de ámbitos 2
    int amb1 = 5;
    Ambitos2();
    */

    //Sección de expresiones aritméticas
    Aritmeticas();

    /*
    //Seccion de expresiones lógicas
    Logicas();

    //Seccion de expresiones relacionales
    Relacionales();
    //@punteo final
    LOG("@punteo Final: "+@punteo);
    */
}

 int Declaracion(){
    /*
        SALIDA ESPERADA:
            ========= Metodo Declaracion =========
            Voy a ganar Compiladores 2 :D
            ======================================
    
    */
    LOG("========= Metodo Declaracion =========");
    int @n1, @n2, @n3, @n4 = 1;
    String @str1, @str2, @str3, @str4 = "Voy a ganar Compiladore";
    double @db1, @db2, @db3, @db4 = 0.0;
    String @chr1, @chr2, @chr3, @chr4 = "s";
    //if n modificar la asignación
    if (@db1 == @db4){
        LOG(@str1 + @chr2 +" " +@n3+" :D");
    }else {
        LOG("Problemas en el metodo declaracion :(");
    }
    LOG("======================================");
    @punteo = @punteo + 5;
}

 int Ambitos2(){
    //debería lanzar un error, cualquiera
    //comentar luego de que lanze el error
    LOG(@amb1);
    String @amb1 = "Desde ambito2";
    LOG("==============Ambitos 2===============");
    @punteo = @punteo + 5;
    LOG(@amb1);
    LOG("======================================");

}

 int Aritmeticas(){
    //suma de strings con caracteres
    /*
        SALIDA ESPERADA
    ==============Aritmeticas=============
    Hola COMPI
    El valor de  @n1 = 52.1
    El valor de @n3 = 70.0
    -Operaciones Basicas: valor esperado:   a)62   b)0   c)-19   d)256   resultados>
    a) 62
    b) 0
    c) -19
    d) 256
    ======================================
    */
    LOG("==============Aritmeticas=============");
    String @art1 = "Hola "+"C"+""+"O"+""+"M"+""+"P"+""+"I";
    LOG(@art1);
    if (@art1=="Hola COMPI"){
        @punteo = @punteo + 3;
    }else {
        LOG("Perdiste 3 puntos en suma de String y String :c");
    }

    double @n1 = 0.0 + 1 + 1 + 1 + 0.1 + 49;
    LOG("El valor de  @n1 = "+@n1);
    if (@n1 == 52.1){
        @punteo = @punteo + 5;
    }else {
        LOG("Perdiste 5 puntos en suma de enteros booleanos y caracteres :c");
    }

    double @n4 = (5750 * 2) - 11800 + 1.0;
    double @n3 = (((3 * 3) + 4) - 80 + 40.00 * 2 + 358.50 - (29 / 14.50)) - (0.50) + @n4;
    LOG("El valor de @n3 = "+@n3);
    if (@n3 == 70)
    {
        @punteo = @punteo + 3;
    }
    else 
    {
        LOG("Perdiste 3 puntos :c ");
    }
    
    operacionesBasicas();
    operacionesAvanzadas();
    LOG("======================================");
    
}

 int operacionesBasicas(){
    LOG("Operaciones Aritmeticas 1: valor esperado:   a)62   b)0   c)-19   d)256   resultados>");
    int @a = (20-10+8/2*3+10-10-10+50);
    int @b = (50/50*50+50-100+100-100);
    int @c = (100/20*9-78+6-7+8-7+7*1*2*3/3);
    int @d = (2**(20/5*2));
    LOG("a) " +@a);
    LOG("b) " +@b);
    LOG("c) " +@c);
    LOG("d) " +@d);
    if (@a==62 && @b==0 && @c == -19 && @d ==256){
        LOG("Operaciones aritmeticas 1 bien :D");
        @punteo = @punteo + 5;
    }else {
        LOG("Error en las operaciones basicas :(");
    }
}

 int operacionesAvanzadas(){
    int @aritmetica1 = 2;
    int @aritmetica2 = -10;
    LOG("Operaciones Aritmeticas 2: valor esperado>-20  41, resultado>");
    int @aritmetica3 = @aritmetica2*@aritmetica1;
    LOG(@aritmetica3+"");
    @aritmetica1 = @aritmetica3/@aritmetica1+50**2/50+50*2-100+100/100-0;
    LOG(@aritmetica1+"");
    if (@aritmetica3 == -20 && @aritmetica1 == 41){
        LOG("Operaciones aritmeticas 2 bien :D");
        @punteo = @punteo + 5;
    }else {
        LOG("Error Operaciones Aritmeticas");
    }
}
