String @prueba = "Rihanna just hit me on a text"+
"Last night I left hickeys on her neck"+
"Wait, you just dissed me? I'm perplexed"+
"Insult me in a line, compliment me on the next, damn"+
"I'm really sorry you want me to have a heart attack"+
"Was watchin' 8 Mile on my NordicTrack"+
"Realized I forgot to call you back"+
"Here's that autograph for your daughter, I wrote it on a Starter cap"+
"Stan, Stan, son, listen, man, dad isn't mad"+
"But how you gonna name yourself after a damn gun and have a man bun?"+
"The giant's woke, eyes open, undeniable";

log(@prueba.toUpperCase());
log(@prueba.toLowerCase());
log(@prueba.length());
log(@prueba.subString(0,7));

if(@prueba.startsWith("Rihanna")){
	log("Todo bien todo\ncorrecto :v");
}else{
	log(":c ya valines\nTigre :c");
}