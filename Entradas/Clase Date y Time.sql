Date @fecha = '2019-08-18'; //‘yyyy-mm-dd’ 
String @salida = "";

@salida += @fecha.getYear(); //devuelve año en entero
@salida += "/";
@salida += @fecha.getMonth(); 
@salida += "/";
@salida += @fecha.getDay();

log(@salida);

@salida = "";
Time @hora = '11:10:00'; // ‘hh:mm:ss’ 
@salida += "Hora: ";
@salida += @hora.getHour();
@salida += "\nMinutos: ";
@salida += @hora.getMinuts();
@salida += "\nSegundos: ";
@salida += @hora.getSeconds();

log(@salida);