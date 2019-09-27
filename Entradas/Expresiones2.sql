int aritmeticas_basicas(){
	LOG("1) Operaciones Aritmeticas Basicas");
	// 1765
	double @temp1 = 10 + 45 * 78 / 2;
	String @texto = "1.1) ";
	LOG(@texto + ": " + @temp1);
	// 80
	int @temp2 = 0 + (10 * 8);
	@texto = "1.2) ";
	LOG(@texto + ": " + @temp2);
	// 9
	//int @temp3 = int(8 - 7 + 2**3)
	int @temp3 = 8 - 7 + 2**3;
	@texto = "1.3) ";
	LOG(@texto + ": " + @temp3);
}

	int aritmeticas_avanzadas(){
		LOG("2) Operaciones Aritmeticas Avanzadas");
		// 112
		double @temp1 = 15 * 7 - 2 / 2 - 8 * (5 - 6);
		String @texto = "2.1) ";
		LOG(@texto + " : " + @temp1);
		// 143
		//int @temp2 = int (0 + (10 * 8) - 18 + 3**4 )
		int @temp2 = 0 + (10 * 8) - 18 + 3**4 ;
		@texto = "2.2) ";
		LOG(@texto + " : " + @temp2);
		// 14
		//int @temp3 = int ((8 - 7 + 2**3 / 3) * 4)
		int @temp3 = (int)(8 - 7 + 2**3 / 3) * 4;
		@texto = "2.3) ";
		LOG(@texto + " : " + @temp3);
	}
		
	

int operaciones_relacionales_basicas(){
	LOG("3) Operaciones Relacionales Basicas");
	int @salida = 0;
	if(@salida < 10){
		@salida = 5 * 9;
		if(@salida > 44){
			@salida = @salida + 1;
		}
	}		
	else{
		@salida = 1;
	}
	if(@salida != 1){
		if (@salida == 46){
			LOG("@salida CORRECTA!!");
		}
		else{
			LOG("@salida incorrecta!!");
		}
	}			
	else{
		LOG("@salida incorrecta!!");
	}
}		 

	int operaciones_relacionales_avanzadas(){
		LOG("4)Operaciones Relacionales Avanzadas");
		if(10 - 15 >= 0){
			LOG("@salida incorrecta!!");
		}
		else{
			if(15 + 8 == 22 - 10 + 5 * 3 - 4){
				if(10 != 11 - 2){
					LOG("@salida CORRECTA!!");
				}
				else{
					LOG("@salida incorrecta!!");
				}
			}
			else{
				if(1 == 1){
					LOG("@salida incorrecta!!");
				}
				else{
					LOG("@salida incorrecta!!");
				}
			}
		}
	}

	int operaciones_logicas_basicas(){
		LOG("5) Operaciones Logicas Basicas");
		if(((True == True) && (True != False)) || (1 > 10) && (!(True == True) == True)){
			LOG("@salida CORRECTA!!");
		}
		else{
			LOG("@salida incorrecta!!");
		}
	}
		
	int operaciones_logicas_avanzadas(){
		LOG("6) Operaciones Logicas Avanzadas");
		if(!(15 == 14) || (((15 * 2 >= 15) && (12 < 24)) || ((98 / 2 == 15) || (!(15 != 6 - 1))))){
			LOG("@salida CORRECTA!!");
		}
		else{
			LOG("@salida incorrecta!!");
		}
	}

	int operaciones_conjuntas(){
		LOG("7) Operaciones Conjuntas");
		if (!(5 * 3 - 1 == 14) && !(!(15 == 6 - 1))){
			LOG("@salida incorrecta!!");
		}
		else{
			double @variable = -1 * (54 / 6 + 9 + 9 - 1 * 8 / 2 * 17);
			double @var2 = 48 / 4 * 79 - 2 + 8;
			String @salida = (String)(@variable) + "" + (String)(@var2);
			//LOG("------------>" , @salida)
			if(@salida=="41954" || @salida == "41.0954.0"){
				LOG("@salida CORRECTA!!");
			}
			else{
				LOG("@salida incorrecta!!");
			}
		}
	}

aritmeticas_basicas();
aritmeticas_avanzadas();
operaciones_relacionales_basicas();
operaciones_relacionales_avanzadas();
operaciones_logicas_basicas();
operaciones_logicas_avanzadas();
operaciones_conjuntas();

