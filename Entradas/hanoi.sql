CREATE DATABASE Funciones;

use Funciones;

procedure Hanoi(int @discos, String @origen, String @aux, String @destino), () {
    if (@discos == 1) {
        LOG("mover disco de " + @origen + " a " + @destino);
    } else {
        call Hanoi(@discos - 1, @origen, @destino, @aux);
        log("mover disco de " + @origen + " a " + @destino);
        call Hanoi(@discos - 1, @aux, @origen, @destino);
    }
}

Call hanoi(4, "1", "2", "3");
