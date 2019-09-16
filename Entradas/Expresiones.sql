
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
    
    //seccion de manejo de ámbitos 2
    int @amb1 = 5;
    Ambitos2();

    //Sección de expresiones aritméticas
    Aritmeticas();

    
    //Seccion de expresiones lógicas
    Logicas();

    
    //Seccion de expresiones relacionales
    Relacionales();

    //@punteo final
    LOG("@punteo Final: "+@punteo);
}

 int Declaracion(){
    /*
        SALIDA ESPERADA:
            ========= Metodo Declaracion =========
            Voy a ganar Compiladores 2 :D
            ======================================
    
    */
    LOG("========= Metodo Declaracion =========");
    int @n1, @n2, @n3, @n4 = 2;
    String @str1, @str2, @str3, @str4 = "Voy a ganar Compiladore";
    double @db1, @db2, @db3, @db4 = 0.0;
    String @chr1, @chr2, @chr3, @chr4 = "s";
    //if n modificar la asignación
    if (@db1 == @db4){
        LOG(@str4 + @chr4 +" " +@n4+" :D");
    }else {
        LOG("Problemas en el metodo declaracion :(");
    }
    LOG("======================================");
    @punteo = @punteo + 5;
}

 int Ambitos2(){
    //debería lanzar un error, cualquiera
    //comentar luego de que lanze el error
    LOG("========= Error Ambitos ==============");
    LOG("Debería lanzar error: "+@amb1);
    String @amb1 = "Desde ambito2";
    LOG("======================================");
    LOG("================ Nice ================");
    @punteo = @punteo + 5;
    LOG("Sin error: "+@amb1);
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
    -Operaciones Basicas: valor esperado:   a)62   @b)0   c)-19   d)256   resultados>
    a) 62
    @b) 0
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
    LOG("Operaciones Aritmeticas 1: valor esperado:  \na)62   \nb)0   \nc)-19   \nd)256   \nresultados>");
    int @a = (20-10+8/2*3+10-10-10+50);
    int @b = (50/50*50+50-100+100-100);
    int @c = (100/20*9-78+6-7+8-7+7*1*2*3/3);
    int @d = (2**(20/5*2));
    LOG("a) " +@a);
    LOG("@b) " +@b);
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
    LOG("Operaciones Aritmeticas 2: valor esperado> -20  41, resultado>");
    int @aritmetica3 = @aritmetica2*@aritmetica1;
    LOG(@aritmetica3+"");
    @aritmetica1 = @aritmetica3/@aritmetica1+50**2/50+50*2-100+100/100-0;
    LOG(@aritmetica1+"");
    if (@aritmetica3 == -20 && @aritmetica1 == 41){
        LOG("Operaciones aritmeticas 2 bien :D");
        @punteo = @punteo + 5;
    }else {
        LOG("Error Operaciones Aritmeticas :c alv :c");
    }
}

//FN5HU-3uykL

 int Logicas(){
     LOG("==============Logicas1=============");
    if (!!!!!!!!!!!!!!!!!!!!!!true){
        @punteo = @punteo + 1;
        LOG("Bien primera condicion :)");
    }else {
        LOG("Perdiste 1 punto :c");
    }

    if (true && true || false && false && false || !true){
        @punteo = @punteo + 1;
        LOG("Bien segunda condicion:)");
    }else {
        LOG("Perdiste 1 punto :c");
    }
    LOG("======================================");
    Logicas2();
}

 int Logicas2(){
    int @n0 = 16;
         LOG("==============Logicas2=============");

    if (!(!(@n0 == 16 && false==true) && !(true))){
            LOG("Not y Ands Correctos");
                        @punteo = @punteo +3;

    }else {
                LOG("No funcionan nots y ands :(");
        }
    int @n1 = @n0 /16;
    @n1 = @n1 + (int)true;
        boolean @condicion1 = @n1 !=2; //esto es false
        int @aritmetica1 = @n0/16 + ((int)((!(true||false)))); // @aritmetica1 = 0
        boolean @condicion2 = @aritmetica1 == @n1; //false
        boolean @condicion3 = !true; //false
        
    if (!(!(!(@condicion1||@condicion2) || @condicion3 ))){
        LOG("Nots y Ors correectos");
                @punteo = @punteo + 3;
    }else {
            LOG("No Funciona nots y ands :(");
        }
            LOG("======================================");
}

 int Relacionales(){
    int @n0 = 34;
    int @n1 = 16;
    
    relaciones1(@n0);
    relaciones2(@n1);
}


 int relaciones1(int @salida)
{
        LOG("==============relacionales1=============");
        double @n0 = @salida + 0.0;
        if (@n0 < 34.44)
            {
                @salida = @salida+15;
                if (@salida > 44)
                    {
                        @salida++;
                    }
            }
            else {
                @salida = 1;
            }
        
        if (@salida != 1)
            {
                if (@salida == 50)
                    {
                        LOG("@salida Correcta Relacionales 1!");
                        @punteo = @punteo + 5;
                    }
                    else {
                        LOG("@salida incorrecta!!");
                    }
            }
            else {
                LOG("@salida incorrecta!!");
            }
        LOG("======================================");
}

 int relaciones2(int @n0){
            LOG("vas bien, animo :D");
            LOG("============Relacionales2=============");

            if (10-15 >= 0 && 44.44 == 44.44)
            {
                LOG("@salida incorrecta primer if relacionales2!!");
            }
            else {
                if (15+8 == 22-10+5*3-4 && 13*0>-1)
                    {
                        if (10.0 != 11.0-1.01 )
                            {
                                LOG("@salida CORRECTA en relacionales2!!");
                                @punteo = @punteo + 5;
                            }
                            else {
                                LOG("@salida incorrecta segundo if relacionales 2!!");
                            }
                    }
                    else {
                        if (1 == 1)
                            {
                                LOG("@salida incorrecta relacionales 2 3er if !!");
                            }
                            else {
                                LOG("@salida incorrecta relacionales 2 Sino3er if !!");
                            }
                    }
            }
        LOG("======================================");
        FactorialIterativo(7);
}

int FactorialIterativo(int @n2){
                     LOG("==============for Calificar Ciclos=============");

    int @numeroFactorial = @n2;
    while(@numeroFactorial > -1){
        mostrarFactorial(@numeroFactorial);
        @numeroFactorial--;
    }
        SentenciasAnidadas();
        LOG("======================================");

}

 int mostrarFactorial(int @n2){
    int @fact = 1;
    String @str= "El factorial de: "+@n2 +" = ";
    if (@n2 !=0){
        for(int @i=@n2; @i >0; @i--){
            @fact = @fact * @i;
            @str = @str + @i;
            if (@i > 1){
                @str = @str + " * ";

            }else {
                @str = @str + " = ";
            }
        }
    }
        @str = @str + @fact+ ";";
    LOG(@str);
}


 int figura1(int @n){
    
    String @StringFigura = "";
    for (int @i = -3*@n/2; @i <= @n; @i++) {
        @StringFigura = "";
            for (int @j = -3*@n/2; @j <= 3*@n/2; @j++) {

                int @absolutoi = @i;
                int @absolutoj = @j;
                if (@i <0){
                    @absolutoi = @i*-1;
                }
                if (@j < 0){
                    @absolutoj = @j*-1;
                }
                if ((@absolutoi + @absolutoj < @n)
                    || ((-@n/2-@i) * (-@n/2-@i) + ( @n/2-@j) * ( @n/2-@j) <= @n*@n/2)
                    || ((-@n/2-@i) * (-@n/2-@i) + (-@n/2-@j) * (-@n/2-@j) <= @n*@n/2)) {
                    @StringFigura = @StringFigura + "* ";
                }
                else{
                    @StringFigura = @StringFigura + ". ";
                }
            }
            LOG(@StringFigura);
        }
    LOG("if la figura es un corazon +10 <3");
}

 int figura2(){
    String @StringFigura = "";
     String @c = "* ";
        String @b = "  ";
        int @altura = 10;
        int @ancho = 1;
        for (int @i = 0; @i < @altura/4; @i++){
            for (int @k = 0; @k < @altura - @i; @k++){
                @StringFigura = @StringFigura+@b;
            }
            for (int @j = 0; @j < @i*2 + @ancho; @j++){
                @StringFigura = @StringFigura + @c;
            }
            
            LOG(@StringFigura);
            @StringFigura ="";
        }
         @StringFigura = "";
         for(int @i = 0; @i < @altura/4; @i++){
            for(int @k = 0; @k < (@altura - @i) - 2; @k++){
                @StringFigura = @StringFigura + @b;
            }
            for(int @j = 0; @j < @i*2 + 5; @j++){
                @StringFigura = @StringFigura + @c;
            }
            
            LOG(@StringFigura);
            @StringFigura = "";
        }
         @StringFigura = "";
        for(int @i = 0; @i < @altura/4; @i++){
            for(int @k = 0; @k < (@altura - @i) - 4; @k++){
                @StringFigura = @StringFigura + @b;
            }
            for(int @j = 0; @j < @i*2 + 9; @j++){
                @StringFigura = @StringFigura +@c;
            }
            
            LOG(@StringFigura);
            @StringFigura = "";
        }
        
        @StringFigura ="";
        for(int @i = 0; @i < @altura/4; @i++){
            for(int @k = 0; @k < (@altura - @i) - 6; @k++){
                @StringFigura = @StringFigura + @b;
            }
            for(int @j = 0; @j < @i*2 + 13; @j++){
                @StringFigura = @StringFigura + @c;
            }
            
            LOG(@StringFigura);
            @StringFigura = "";
        }
        @StringFigura = "";
        for(int @i = 0; @i < @altura/4; @i++){
            for(int @k = 0; @k < @altura -2; @k++){
                @StringFigura = @StringFigura + @b;
            }
            for(int @j = 0; @j < 5; @j++){
                @StringFigura = @StringFigura + @c;
            }
            
            LOG(@StringFigura);
            @StringFigura = "";
        }
        
            LOG("if la figura es un Arbol +10 <3");

       }

 int SentenciasAnidadas(){
    int @numero1 = 0;
    do{
    switch(@numero1){
        case 0:
        figura0(8);
        break;
        case 1:
        figura1(10);
        break;
        case 2:
        figura2();
        break;
        default:
            LOG("Esto se va a LOG 2 veces :3");
            
    }
    @numero1 = @numero1 + 1;
    }while(@numero1 <5);
}

 int figura0(int @numero){
    int @i = 0;
    while(@i < @numero){
        int @j = 0;
        int @numeroMostrar = 1;
        String @unaFila = "";
        while(@j <= @i){
            @unaFila = @unaFila + " " + @numeroMostrar;
            @numeroMostrar  = @numeroMostrar + 1;
            @j = @j + 1;
        }
        LOG(@unaFila);
        @i = @i + 1;
    }
    LOG("if la figura es un triangulo de numeros + 5 :3");
}
