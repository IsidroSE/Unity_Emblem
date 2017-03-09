# Unity_Emblem
Proyecto de final de ciclo de DAM. Videojuego que hice utilizando la herramienta Unity con el lenguaje C#. ES un RPG táctico de estrategia por turnos basado en la clásica franquicia de Nintendo llamada "Fire Emblem".

Es un RPG táctico en el cual el objetivo es matar al rey enemigo, pierdes si todos tus personajes mueren. Los personajes podrán moverse X número de pasos por turno y sólo se podrán mover de forma horizontal y vertical, por lo tanto si estamos en la celda 0,0 y queremos movernos a la 1,1, tendremos que pasar antes por la 1,0 o bien, por la 0,1.

Una vez un personaje haya efectuado una acción (atacar/curar/esperar), no podrá volver a moverse hasta el siguiente turno, esto se reflejará en pantalla mostrando al personaje en un tono más oscuro para que el jugador sepa que ya no puede mover a este personaje. Existen diferentes tipos de celdas y cada tipo de celda tiene un coste de pasos, este coste de pasos varía dependiendo del tipo de personaje.

Un personaje sólo podrá moverse un máximo de la cantidad de pasos que pueda hacer este personaje, la cantidad de pasos también varía dependiendo de la clase de este, por ejemplo la infantería pesada tiene 4 pasos mientras que los jinetes de pegaso tendrán 7. Además, esta cantidad de pasos puede verse reducida dependiendo del tipo de terreno que haya alrededor del personaje.

El usuario controlará un puntero amarillo que podrá mover por la pantalla haciendo click sobre alguna celda del mapa. Si posicionas el puntero sobre un personaje, se mostrará un cuadro con el retrato, nombre, clase, nivel, entre otras estadísticas del personaje. Si al tener el puntero sobre un personaje volvemos a hacer click en la misma posición, seleccionaremos el personaje y podremos ordenarle que haga una acción.

Cuando seleccionemos al personaje, entrará en acción un algoritmo de búsqueda que buscará y mostrará en pantalla todas las celdas a las que el personaje podrá moverse. Cuando tengamos a un personaje seleccionado, no podremos mover el puntero fuera del área pintada en pantalla. Las casillas azules representan casillas a las que te puedes mover o casillas por las que puedes pasar.

Un personaje puede atravesar celdas ocupadas por un aliado, pero no quedarse en una celda ocupada ya sea por un aliado o enemigo, no pueden haber 2 personajes en la misma celda y un personaje no puede atravesar celdas ocupadas por un enemigo. Las celdas rojas o verdes representan a celdas a las que no te podrás mover por no tener suficientes pasos pero si posicionarte en una celda adyecente a esta para efectuar una acción.

Una vez hayamos seleccionado a un personaje, podremos elegir entre varias opciones.

Podremos mover al personaje a una celda posicionando el puntero en la celda a la que queramos mover el personaje y luego volviendo a pulsar click en esta celda. De este modo veremos una breve animación de cómo el personaje toma la ruta más corta hacía esta celda.

Una vez movido el personaje a esta celda, dependiendo de si el personaje es sanador o no y si hay aliados o enemigos cerca, este personaje podrá efectuar las acciones de curar o atacar. También existe la opción de esperar que lo que hará es ordenarle al personaje que se quede en esa posición hasta el siguiente turno. Una vez efectuadas las acciones de curar o atacar, tampoco podremos volver a mover al personaje hasta el siguiente turno.

En caso de que prefieras mover al personaje a otra ubicación porque no te gusta la posición donde se ha quedado o no te gustan las previsiones del ataque o curación (se hablará de esto más adelante), siempre tendremos la opción de cancelar el movimiento y hacer como si no se hubiera movido (el personaje se quedará en la celda en la que estaba antes de moverlo) pero no será posible hacer esto si ese personaje ya ha efectuado una acción.

Una vez hayas movido a un personaje, se hará una comprobación para saber si quedan personajes del jugador por moverse, tu turno no acabará hasta que hayas movido a todos tus personajes. Una vez hayas movido a todos tus personajes, el turno del enemigo empezará. Durante este turno, el puntero del jugador desaparecerá y no podrás efectuar ninguna acción, ya que ahora le toca a la IA mover a sus personajes.

Durante el turno del enemigo, la IA hará una comprobación en todos los personajes del enemigo, si hay personajes del jugador al alcance, se moverán para atacarles, en caso contrarío, no se moverán. En el caso de que haya más de un personaje del jugador con menos defensas contra su tipo de ataque (los magos atacan con magia y los personajes se defienden de la magia con resistencia, el resto ataca con fuerza y los personajes se defienden de la fuerza con defensa).

Cuando la IA haya movido a todos los personajes, acabará el turno del enemigo y volverá a ser el turno del jugador, el puntero volverá a aparecer y podremos volver a mover a todos nuestros personajes.
