List<int> @array = [10, 7, 8, 9, 1, 5, 4, 3];
sort(@array, 0, @array.size() - 1);
printArray(@array);

int partition(List @array, int @low, int @high)
{
	int @pivot = @array.get(@high);
	int @i = (@low - 1);
	for(int @j = @low; @j < @high; @j++)
	{
		if(@array.get(@j) < @pivot)
		{
			@i++;
			///swap
			int @temp = @array.get(@i);
			@array.set(@i, @array.get(@j));
			@array.set(@j, @temp);
		}
	}
	int @temp = @array.get(@i+1);
	@array.set(@i+1, @array.get(@high));
	@array.set(@high, @temp);

	return @i + 1;
}

int sort(List @array,  int @low, int @high)
{
	if(@low < @high)
	{
		int @pi = partition(@array, @low, @high);
		LOG("@pi ->"+@pi);
		sort(@array, @low, @pi - 1);
		sort(@array, @pi + 1, @high);
	}
}

int printArray(List @array)
{
	LOG("*****IMPRIMIENDO ARREGLO ORDENADO*******\n");
	for(int @i = 0; @i < @array.size(); @i++)
	{
		LOG("ARRAY EN "+@i+"  ->"+@array.get(@i)+"\n");
	}
	LOG("******FIN ARREGGLO");
}
