using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour {

    // ------------------------------------------------------------------------------------------------
    // Equipos
    private static List <Personaje> equipoJugador;
    private static List <Personaje> equipoEnemigo;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Mapa
    public Mapa gestorDeMapas;
    private int[,] caracteristicasCeldas;
    private Personaje[,] mapa;
    public CreadorPersonajes creadorPersonajes;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Lista donde se guardaran las celdas que están pendientes para comprobarse el método de crear rutas
    private List <Celda> listaCeldasMov;
    //Lista donde se guardarán las celdas que ya han sido comprobadas en el método de crear rutas
    private List <Celda> listaCeldasAtaq;
    //Lista donde se guardarán las celdas adjecentes a la posición del personaje después de haberse movido para comprobar las posibles acciones que este podrá realizar
    private List<Celda> listaCeldasObjetivos;
    //Guardará el index de la primera posición ocupada de listaCeldasObjetivos
    private int primerObjetivo;
    //Los cuadros que se mostrarán en pantalla (y los que se guardaran en la lista
    public GameObject cuadroAzul;
    public GameObject cuadroRojo;
    public GameObject cuadroVerde;
    /*Lista que guardará todas las celdas, tanto de la lista de movimiento como las de ataque, esto se utilizará para que,
    en caso de que el usuario pulse cancelar, se destruyan todas las celdas pintadas*/
    private List<GameObject> listaCeldas;
    //Puntero que controlará el jugador
    public Puntero puntero;
    //Controlará si ya hay un personaje moviendose o no
    private bool personajeMov = false;
    //Controlará si el personaje que se está moviendo va a ejecutar alguna accion o no
    private bool accionEnCurso = false;
    //Referencia al personaje que va a moverse
    private Personaje persoMoviendose;
    //Clase que controlará los dos canvas del modo atacar/curar y previsiones
    private ControladorCanvas controladorCanvas;
    //Clase que controlará los sonidos de los ataques
    private ControladorSonidos controladorSonidos;
    //Variables donde se guardarán las previsiones de daño de los personajes que vayan a atacar o curar
    private static int Vida = 0;
    private static int DanyoCausado = 1;
    private static int ProbGolpe = 2;
    private static int Ataques = 3;
    private static int Exp = 4;
    private static int tamanyoVectorPrevisiones = 5;
    private int[] previsionesUnidadSeleccionada = new int[tamanyoVectorPrevisiones];
    private int[] previsionesObjetivo = new int[tamanyoVectorPrevisiones];
    //Número por el que se multiplicará el daño total que inflinge una unidad a otra si esta hace un golpe crítico
    int danyo_criticos = 3;
    //Variable que guardará la referencia del personaje sobre el que se ejecutará el ataque o curación
    private Personaje objetivo;
    //Esperiencia que ganaran los personajes
    //Puntos de experiencia que ganaran los personajes al atacar o curar
    private int expAccion = 20;
    //Puntos de defensa que ganará el personaje atacado si este puede defenderse
    private int expDefensa = 10;
    //Variable que marcará si es nuestro turno o el del enemigo
    private bool turnoEnemigo = false;
    //Guardará las rutas que tomarán los enemigos
    private List<Celda> listaCeldasEnemigo = null;
    //Variable que se usará para saber si se ha acabado el juego
    private bool acabarJuego = false;


    // ------------------------------------------------------------------------------------------------

    //Indica si hay un personaje moviendose para controlar el movimiento del puntero
    public void setPersonajeMov (bool orden) {
        personajeMov = orden;
    }

    // ------------------------------------------------------------------------------------------------
    //Añadir o borrar unidades de las listas
    public static void insertarJugador (Personaje personaje) {
        equipoJugador.Add(personaje);
    }

    private static void borrarJugador(Personaje personaje) {
        equipoJugador.Remove(personaje);
    }

    public static void insertarEnemigo(Personaje personaje) {
        equipoEnemigo.Add(personaje);
    }

    private static void borrarEnemigo(Personaje personaje) {
        equipoEnemigo.Remove(personaje);
    }

    //Comprueba siel personaje pasado como parámetro es del jugador o no
    public static bool esDelJugador (Personaje personaje) {

        if (equipoJugador.Contains(personaje)) {
            return true;
        }
        else {
            return false;
        }

    }
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Getters y Setters

    //Devuelve el mapa de personajes (se usa en el creador de personajes para meter a los personajes directamente sin tener que referenciar al controlador cada vez)
    public Personaje[,] getMapa() {
        return mapa;
    }

    //Obtiene la referencia a la matriz de personajes
    public void setMapa(Personaje[,] mapa) {
        this.mapa = mapa;
    }

    //Obtiene el personaje que se encuentre en la posición indicada
    public Personaje getPosMapa(int x, int y) {
        return mapa[x, y];
    }

    //Mueve al personaje pasado como parámetro a la posición seleccionada
    private void moverPersonajeEnMatriz(Personaje personaje, int x, int y) {

        if (x != personaje.getX() || y != personaje.getY()) {
            mapa[x, y] = mapa[personaje.getX(), personaje.getY()];
            mapa[personaje.getX(), personaje.getY()] = null;
            personaje.setX(x);
            personaje.setY(y);

        }

    }

    //Devuelve el personaje seleccionado actual
    public Personaje getPersoMoviendose () {
        return persoMoviendose;
    }

    //Devuelve true si hay una acción en curso (modo ataque o curar), esto determinará el filtro del puntero a la hora de moverse
    public bool getAccionEnCurso () {
        return accionEnCurso;
    }

    //Guarda la referencia del objetivo actual
    public void set_Objetivo (Personaje objetivo) {
        this.objetivo = objetivo;
    }

    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Crear todas las posibles rutas que podrá tomar un personaje al moverse

    //Muestra la información de la unidad sobre la que esté posicionada el puntero en el canvas de información del personaje
    public void mostrarInformacionPersonaje (Personaje personaje) {
        GetComponent<ControladorCanvas>().mostrarEstadisticas(personaje);
    }

    //Activa o desactiva el canvas de estadísticas
    public void gestorCanvasEstadisticas (bool activar) {
        GetComponent<ControladorCanvas>().gestionarCanvasEstadisticas(activar);
    }

    //Comprueba si el canvas de estadísticas está activado o no
    public bool isCanvasEstadisticasActive () {
        return GetComponent<ControladorCanvas>().isEstadisticasActive();
    }

    //Obtiene todas las posibles rutas que podrá tomar un personaje
    private void obtenerRutas (Personaje personaje) {

        //Inicializamos las listas donde se irán guardando las celdas donde se harán las comprobaciones
        listaCeldasMov = new List <Celda> ();
        listaCeldasAtaq = new List <Celda> ();

        /*Metemos en la lista abierta la celda actual del personaje (como no especificamos un padre, este valor será nulo y por lo
        tanto, esta celda no tendrá padre (cosa obvia porque es la celda de donde empezará este algoritmo)*/
        Celda celda = new Celda(personaje.getX(), personaje.getY(), 0);

        //Agregamos la celda a la lista
        listaCeldasMov.Add(celda);

        //Guardamos el personaje para tener una referencia y no tener que pasarlo en cada recursividad
        this.persoMoviendose = personaje;

        //Llamamos al método que empezará a buscar recursivamente todas las rutas que el personaje podrá tomar
        sacarRutas(personaje.getX()+1, personaje.getY(), 0, celda);
        sacarRutas(personaje.getX()-1, personaje.getY(), 0, celda);
        sacarRutas(personaje.getX(), personaje.getY()+1, 0, celda);
        sacarRutas(personaje.getX(), personaje.getY()-1, 0, celda);

        /*Una vez finalizado el proceso anterior, buscaremos también recursivamente, las celdas adyecentes a las anteriores según el rango del personaje para determinar su rango
        de ataque o curación guardando también las mismas celdas como padres de las que añadiremos ahora, de esta forma, siempre guardaremos las rutas más cortas a cada celda*/
        for (int i = 0 ; i < listaCeldasMov.Count ; i++) {
            sacarRutasAtaque(listaCeldasMov[i].getX(), listaCeldasMov[i].getY(), 0, listaCeldasMov[i]);
        }

    }

    /*Guarda todas las celdas a las que un personaje podrá moverse, filtrando el terreno por coste de pasos y guardando siempre
    la ruta más corta a cada celda*/
    private void sacarRutas(int x, int y, int pasos, Celda padre) {

        Celda celda = null;
        bool ocupada = false;

        /*Si la celda actual está dentro del mapa, seguiremos, en caso contrario no haremos nada, 
        así no nos saldremos de las matrices de los mapas*/
        if (x <= gestorDeMapas.limiteMapaX && y <= gestorDeMapas.limiteMapaY && x >= 0 && y >= 0) {

            /*Si los pasos que tenemos son menores o iguales a la cantidad de pasos que puede dar el personaje, 
            procederemos hacer el filtrado para añadir la casilla a la lista*/
            if (pasos <= persoMoviendose.getPasos()) {

                // ------------------------------------------------------------------------------------------------
                //CASILLA LIBRE
                //Si la casilla no está ocupada (es null), filtraremos por tipo de unidad y terreno
                if (mapa[x, y] == null) {
                    pasos = filtradoPorTipoUnidad(x, y, pasos);
                }
                //CASILLA OCUPADA
                /*Si la casilla ya está ocupada por una unidad, podrán pasar 2 cosas:
                    1. Si la unidad que ocupa esta casilla es aliada, no podrás permanecer en esta casilla, pero si atravesarla.
                    2. Si la unidad que ocupa esta casilla es enemiga, no podrás permanecer en esta casilla ni atravesarla.*/
                else if (equipoJugador.Contains(persoMoviendose)) {
                    if (equipoJugador.Contains(mapa[x, y])) {
                        pasos = filtradoPorTipoUnidad(x, y, pasos);
                        ocupada = true;
                    }
                    else if (equipoEnemigo.Contains(mapa[x, y])) {
                        pasos = -1;
                    }
                }
                else if (equipoEnemigo.Contains(persoMoviendose)) {
                    if (equipoEnemigo.Contains(mapa[x, y])) {
                        pasos = filtradoPorTipoUnidad(x, y, pasos);
                        ocupada = true;
                    }
                    else if (equipoJugador.Contains(mapa[x, y])) {
                        pasos = -1;
                    }
                }
                // ------------------------------------------------------------------------------------------------

                // ------------------------------------------------------------------------------------------------
                //AÑADIR LA CELDA ACTUAL A LA LISTA
                /*Si la unidad tiene suficientes pasos para pasar de esta celda, la añadiremos a la lista, 
                en caso contrario, no haremos nada*/
                if (pasos != -1 && pasos <= persoMoviendose.getPasos()) {
                    
                    /*Instanciaremos la celda para facilitar el tratamiento de esta y le asignaremos la celda pasada como
                    parámetro al principio de este método como padre para así saber el recorrido tomado para llegar hasta aquí*/
                    celda = new Celda(x, y, pasos, padre);

                    //Diremos si esta ocupada o no por un aliado para luego tenerlo en cuenta a la hora de moverse
                    celda.setOcupada(ocupada);

                    //Si la celda no está ocupada, entraremos
                    if (!celda.getOcupada() || ((celda.getOcupada()) && (!padre.getOcupada()) && (pasos != persoMoviendose.getPasos()))) {

                        //Comprobaremos si esta celda ya existe en la lista de celdas (tiene la misma x e y)
                        if (listaCeldasMov.Contains(celda)) {

                            //Si existe, recogeremos la posición donde está situada
                            int pos = listaCeldasMov.IndexOf(celda);

                            /*Comparararemos si la ruta tomada es menor que la que se tomó cuando se insertó la celda de la lista la primera vez*/
                            if (celda.getPasos() < listaCeldasMov[pos].getPasos()) {

                                /*Si es menor, cambiaremos la celda que ya estaba por la actual para guardar siempre
                                la ruta más corta a esta celda, en caso contrario, no haremos nada*/
                                listaCeldasMov[pos] = celda;
                            }
                            //Si tienen el mismo número de pasos si tirará un dado y se elegirá una aleatoria entre las dos
                            else if (celda.getPasos() == listaCeldasMov[pos].getPasos()) {

                                int alea = Random.Range(0, 1);
                                if (alea == 1) {
                                    listaCeldasMov[pos] = celda;
                                }

                            }
                        }
                        //En el caso de que esta celda no exista en la lista, la añadiremos
                        else {
                            listaCeldasMov.Add(celda);
                        }
                    }
                    
                }
            }
            // END AÑADIR LA CELDA ACTUAL A LA LISTA
            // ------------------------------------------------------------------------------------------------


            // ------------------------------------------------------------------------------------------------
            //BUSCAR EN LAS CELDAS ADJECENTES
            /*Comprobaremos si la casilla actual es nula para prevenir algún posible error.
            Una vez comprobado, volveremos a llamar a este método para que compruebe las celdas adjecentes en linia vertical y
            horizontal, pasándole el número de pasos que ya hemos dado y la celda actual para que estas sepan quien es su padre
            en el caso de que vayan a añadirse a la lista*/
            if (celda != null) {
                sacarRutas(x, y + 1, pasos, celda);
                sacarRutas(x, y - 1, pasos, celda);
                sacarRutas(x + 1, y, pasos, celda);
                sacarRutas(x - 1, y, pasos, celda);
            }
            // END BUSCAR EN LAS CELDAS ADJECENTES
            // ------------------------------------------------------------------------------------------------
        } // end x <= gestorDeMapas.limiteMapaX && y <= gestorDeMapas.limiteMapaY && x >= 0 && y >= 0

    } // end sacarRutas()

    // ------------------------------------------------------------------------------------------------
    //FILTRADO POR TIPO DE UNIDAD
    private int filtradoPorTipoUnidad (int x, int y, int pasos) {

        //Estándar o sanador
        /*Estas unidades tendrán un coste estándar en todas las casillas menos en los rios, que no podrán cruzarlos 
        y los poblados*/
        if (persoMoviendose.gettipoUnidad() == Personaje.Estandar || persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            //Comprobar el tipo de terreno
            //Una casilla llana tendrá un coste de 1
            if (caracteristicasCeldas[x, y] == Mapa.Llano || caracteristicasCeldas[x, y] == Mapa.Puerta) {
                pasos++;
            }
            //Una casilla con un bosque tendrá un coste de 2
            else if (caracteristicasCeldas[x, y] == Mapa.Bosque) {
                pasos += 2;
            }
            //Un pico tendrá un coste de 4 para las unidades no montadas
            else if (caracteristicasCeldas[x, y] == Mapa.Pico) {
                pasos += 4;
            }
            //Una unidad estándar no puede cruzar ríos, ni subir montes y el poblado es infranqueable por todos
            else if (caracteristicasCeldas[x, y] == Mapa.Rio || caracteristicasCeldas[x, y] == Mapa.Monte || caracteristicasCeldas[x, y] == Mapa.Poblado) {
                pasos = -1;
            }
        }

        //Bárbaros
        /*Estas unidades son exactamente iguales que las estándar y los sanadores, con la diferencia de que estos si
        que podrán cruzar rios (aunque el coste será muy elevado)*/
        else if (persoMoviendose.gettipoUnidad() == Personaje.Barbaro) {
            //Comprobar el tipo de terreno
            if (caracteristicasCeldas[x, y] == Mapa.Llano || caracteristicasCeldas[x, y] == Mapa.Puerta) {
                pasos++;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Bosque) {
                pasos += 2;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Pico) {
                pasos += 4;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Rio) {
                pasos += 5;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Poblado || caracteristicasCeldas[x, y] == Mapa.Monte) {
                pasos = -1;
            }
        }

        //Jinetes montados a caballo
        /*Estas unidades tienen un rango de pasos muy elevado pero también necesitan más pasos para pasar por ciertas casillas. No podrán cruzar rios, montes ni picos*/
        else if (persoMoviendose.gettipoUnidad() == Personaje.Caballo) {
            //Comprobar el tipo de terreno
            if (caracteristicasCeldas[x, y] == Mapa.Llano) {
                pasos++;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Puerta) {
                pasos += 2;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Bosque) {
                pasos += 3;
            }
            else if (caracteristicasCeldas[x, y] == Mapa.Rio || caracteristicasCeldas[x, y] == Mapa.Poblado || caracteristicasCeldas[x, y] == Mapa.Monte || caracteristicasCeldas[x, y] == Mapa.Pico) {
                pasos = -1;
            }
        }

        //Volador
        /*Para las unidades voladoras casi no hará falta comprobar el terreno porque para ellos todas las casillas
        tendrán un coste de 1 (menos los poblados, que son casillas "pared" y no se podrán franquear ni permanecer en ellas)*/
        else if (persoMoviendose.gettipoUnidad() == Personaje.Volador) {
            if (caracteristicasCeldas[x, y] == Mapa.Poblado) {
                pasos = -1;
            }
            else {
                pasos++;
            }
        }

        return pasos;

    }
    // END FILTRADO POR TIPO DE UNIDAD
    // ------------------------------------------------------------------------------------------------


    /*Busca entre las celdas guardadas con el método sacarRutas() para que, dependiendo del alcance de la unidad, pinte 1 o 2 celdas alrededor de esta 
    (siempre y cuando esas celdas no esten guardadas ya en la lista anterior)*/
    private void sacarRutasAtaque(int x, int y, int alcance, Celda padre) {

        Celda celda = null;

        if (!padre.getOcupada()) {
            //Si el alcance dado como parametro es mayor que el alcance maximo del personaje, no hará nada
            if (alcance <= persoMoviendose.getAlcance()) {

                //Comprobaremos que no estamos fuera del mapa
                if (x >= 0 && y >= 0 && x <= gestorDeMapas.limiteMapaX && y <= gestorDeMapas.limiteMapaY) {

                    //Comprobaremos que esta celda no sea un poblado porque ahí no se podrá atacar
                    if (caracteristicasCeldas[x, y] != Mapa.Poblado) {

                        //Comprobaremos si la celda actual ya está en la lista
                        celda = new Celda(x, y, alcance, padre);

                        //En caso de que la celda ya exista en la lista de movimiento, no haremos nada
                        if (!listaCeldasMov.Contains(celda)) {

                            //Comprobaremos si esta celda ya existe en la lista que está llenando este método
                            if (listaCeldasAtaq.Contains(celda)) {

                                //En el caso de que exista, obtendremos la posición donde se guarda en la lista
                                int pos = listaCeldasAtaq.IndexOf(celda);

                                //Y si los pasos son menores que los que se han dado para llegar a esta, cambiaremos esta celda por la ya existente
                                if (celda.getPasos() < listaCeldasAtaq[pos].getPasos()) {
                                    listaCeldasAtaq[pos] = celda;
                                }
                            }
                            else {
                                //Si no existe en ninguna de las 2 listas, la añadiremos a esta
                                listaCeldasAtaq.Add(celda);
                            }
                        }
                    }
                }
            }
        }

        //Accederemos a todas las celdas adjecentes a esta y si estas no están en ninguna de las 2 listas, se añadirán también
        if (celda != null) {

            /*Si la celda es la misma que el padre, pondremos como padre al padre de esta y no a la misma celda, ya que sino podríamos guardar 2 veces la misma celda y luego veríamos
            como el personaje se queda unos milisegundos caminando en la misma celda*/
            if (celda.Equals(padre)) {
                sacarRutasAtaque(x, y + 1, alcance + 1, padre);
                sacarRutasAtaque(x, y - 1, alcance + 1, padre);
                sacarRutasAtaque(x + 1, y, alcance + 1, padre);
                sacarRutasAtaque(x - 1, y, alcance + 1, padre);
            }
            else {
                sacarRutasAtaque(x, y + 1, alcance + 1, celda);
                sacarRutasAtaque(x, y - 1, alcance + 1, celda);
                sacarRutasAtaque(x + 1, y, alcance + 1, celda);
                sacarRutasAtaque(x - 1, y, alcance + 1, celda);
            }

        }

    }

    /*Cuando el usuario hace click en la misma casilla en la que está el puntero y en esta casilla hay un personaje, seleccionaremos el personaje y crearemos las posibles rutas
    que este podrá tomar para ejecutar sus acciones*/
    public void crearRutas(Personaje personaje) {

        //Si no se ha creado ya una ruta, procederemos a crear una para el personaje pasado como parámetro
        if (!personajeMov) {

            //Inicializamos la lista de celdas donde guardaremos todas las celdas
            listaCeldas = new List<GameObject>();

            //Obtenemos todas las rutas que podrá tomar este personaje
            obtenerRutas(personaje);

            //Las pintaremos en pantalla
            pintarRutas();

            //Ahora que le hemos enseñado al usuario las posibles rutas que podrá tomar el personaje, activaremos el canvas de las acciones
            controladorCanvas.gestionarCanvasMov(true);

            //Hará que el puntero no pueda salirse de las casillas iluminadas hasta que se pulse cancelar o el personaje haga una acción
            personajeEstaMoviendose(true);

            //Activará el panel que enseña las estadísticas del personaje en caso de que no este activado ya
            if (puntero.isActiveCanvasEstadisticas() == false) {
                controladorCanvas.gestionarCanvasEstadisticas(true);
            }

            /*Avisará al controlador de que va a mover un personaje, por lo tanto, no deberá crear más rutas hasta que el usuario
            mueva al personaje o pulse el botón de cancelar*/
            setPersonajeMov(true);

            //Guardamos la referencia a este personaje
            persoMoviendose = personaje;

            //Cambia la animación del personaje de DePie a Moviendose
            setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, true);

            //Comprobamos si en su posición actual puede hacer alguna acción
            comprobarPosiblesAcciones();

        }

    }

    //Hace que el puntero controle los movimientos dependiendo de si el personaje se está moviendo o no
    public void personajeEstaMoviendose(bool mov) {
        puntero.personajeMoviendose(mov);
    }

    //Pinta las celdas en las listas de movimiento y ataque/curacion por pantalla para que el usuario pueda ver las rutas que podrá tomar
    private void pintarRutas () {

        //Aquí guardaremos todas las instancias de las celdas antes de añadirlas a la lista
        GameObject celda;

        for (int i = 0; i < listaCeldasMov.Count; i++) {
            //El método instanciate devuelve un Object, haremos un casting a GameObject y lo guardaremos en la celda para tener una referencia a esta y poder destruirla después
            celda = (GameObject)
                Instantiate(cuadroAzul, new Vector3(listaCeldasMov[i].getX(), listaCeldasMov[i].getY(), 0), Quaternion.identity);
            listaCeldas.Add(celda);

        }

        
        GameObject cuadro;
        //Dependiendo de si el personaje es sanador o no, los cuadros de los alrededores (según el alcance) serán verdes o rojos
        if (persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            cuadro = cuadroVerde;
        }
        else {
            cuadro = cuadroRojo;
        }

        /*Pasamos el cuadro que queramos pintar y pintaremos todas las casillas a las que el personaje podra atacar o curar pero no permanecer en ellas debido 
        a que no tiene pasos suficientes (y tiene el alcance para llegar ahí)*/
        for (int i = 0; i < listaCeldasAtaq.Count; i++) {
            celda = (GameObject)
                Instantiate(cuadro, new Vector3(listaCeldasAtaq[i].getX(), listaCeldasAtaq[i].getY(), 0), Quaternion.identity);
            listaCeldas.Add(celda);
        }

    }

    //Gestiona las animaciones del personaje pasado como parámetro
    public void setAnimacionPersonaje (Personaje personaje, string nombreAnimacion, bool activa) {

        if (personaje == null) {
            personaje = persoMoviendose;
        }

        Animator anim = personaje.gameObject.GetComponent<Animator>();
        anim.SetBool(nombreAnimacion, activa);

    }

    /*Destruye todas las celdas pintadas en pantalla. También ocultará la UI de las acciones que puede tomar un personaje al moverse*/
    public void destruirCeldas() {

        for (int i = 0; i < listaCeldas.Count; i++) {
            Destroy(listaCeldas[i]);
        }

        controladorCanvas.gestionarCanvasMov(false);

    }

    //Destruye las listas que contienen las celdas a las que un personaje puede moverse o atacar/curar
    public void destruirListasCeldas () {
        listaCeldasMov = null;
        listaCeldasAtaq = null;
    }

    /*Dice si la celda pasada como parámetro está dentro de alguna de las listas filtrando según lo que esté haciendo el usuario. El puntero utiliza este método para controlar 
    su movimiento, ya que este no podrá salirse de las celdas pintadas en pantalla cuando tenga una unidad seleccionada*/
    public bool dentroLaLista (Celda celda) {
        
        if (celda != null) {

            
            if (!accionEnCurso) {
                if (listaCeldasMov.Contains(celda) || listaCeldasAtaq.Contains(celda)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                int index = listaCeldasObjetivos.IndexOf(celda);
                if (index != -1) {
                    //En el caso de la lista de objetivos, el puntero sólo se podrá mover a las celdas donde haya un objetivo al alcance
                    if (listaCeldasObjetivos[index].getOcupada()) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
                else {
                    return false;
                }
            }
            

        }
        else {
            return false;
        }
        
    }

    //Recoge la ruta más corta hasta la celda seleccionada y mueve al personaje hasta esta posición siguiendo esta ruta
    public void prepararMovimientoDePersonaje (Celda celda) {

        if (!turnoEnemigo) {
            //Desactivaremos el botón de acción
            controladorCanvas.setBotonAccion(false);
            //Y la misma UI para que tampoco podamos pulsar los botones de cancelar o esperar mientras nos estamos moviendo
            controladorCanvas.gestionarCanvasMov(false);
        }

        Celda celdaSeleccionada;
        bool celdaMov;
        //Guardaremos el index de la celda (si la casilla seleccionada es azul)
        int posCelda = listaCeldasMov.IndexOf(celda);
        
        //Si es -1 quiere decir que no está dentro la lista de movimiento (y por lo tanto no es azul), por lo que la buscaremos en la otra lista y guardaremos la referencia a esta celda
        if (posCelda == -1) {

            posCelda = listaCeldasAtaq.IndexOf(celda);
            celdaSeleccionada = listaCeldasAtaq[posCelda];

            //Indicaremos que no es una celda azul
            celdaMov = false;

        }
        //Si la celda es azul, guardaremos la referencia a esta zelda
        else {

            celdaSeleccionada = listaCeldasMov[posCelda];

            //Indicaremos que es una celda azul
            celdaMov = true;
        }

        //Crearemos una lista de celdas donde guardaremos todo el recorrido que el personaje recorrerá hasta llegar a la celda seleccionada
        List<Celda> listaCeldas = new List<Celda>();
        
        //Si la celda seleccionada es azul
        if (celdaMov) {
            //Y esta no está ocupada por un aliado, la guardaremos en la lista. En caso contrario no lo haremos porque no podemos permanecer en una casilla donde ya hay un personaje
            if (!celdaSeleccionada.getOcupada()) {
                listaCeldas.Add(celdaSeleccionada);
            }
        }

        //Si estamos en el turno enemigo, haremos esto para que los magos y arqueros ataquen desde 2 casillas de distancia, no que se muevan a la casilla de al lado como el resto
        if (turnoEnemigo) {
            if (persoMoviendose.getAlcance() > 1) {
                celdaSeleccionada = celdaSeleccionada.getPadre();
            }
        }

        //Recorreremos la celda seleccionada buscando a los padres hasta que este sea nulo (posición inicial del personaje)
        while (celdaSeleccionada.getPadre() != null) {
            celdaSeleccionada = celdaSeleccionada.getPadre();
            listaCeldas.Add(celdaSeleccionada);
        }

        /*Moveremos al personaje a su posición inicial. En caso de que no lo hubieramos movido antes no hará nada, pero si lo hemos movido, antes de moverlo otra vez de sitio
        habrá que restablecer su posición inicial porque no puedes mover a tu personaje 2 veces en un mismo turno*/
        restaurarPosPersonaje();

        //Si la lista de celdas es mayor o igual que 2, procederemos a mover al personaje (tiene que haber al menos 2 celdas paraque el personaje se mueva)
        if (listaCeldas.Count >= 2) {

            //Reproduciremos el sonido del personaje de caminar
            persoMoviendose.GetComponent<AudioSource>().Play();

            //Y empezaremos a mover al personaje con una corrutina
            if (!turnoEnemigo) {
                StartCoroutine(moverPersonaje(listaCeldas));
            }
            else {
                listaCeldasEnemigo = listaCeldas;
            }

        }
        //En caso contrario, no nos moveremos y el movimiento acabará aquí
        else {
            puntero.set_personajeEnMovimiento(false);
        }

    }

    //Restaura la posición del personaje en la escena
    public void restaurarPosPersonaje () {
        if (persoMoviendose != null) {
            persoMoviendose.transform.position = new Vector3(persoMoviendose.getX(), persoMoviendose.getY(), persoMoviendose.transform.position.z);
        }
    }

    /*Aplica el movimiento deseado por el usuario para el personaje en la matriz, cuando el usuario mueve un personaje en la escena, los cambios no se aplicarán hasta que
    pulse el botón "Esperar" o bien el botón de acción (Atacar/Curar)*/
    public void esperar () {

        //Destruiremos las celdas pintadas en pantalla
        destruirCeldas();

        //Destruiremos las listas de celdas que contenian las celdas anteriores
        destruirListasCeldas();

        //Indicaremos al puntero que ya no hay ningún personaje moviendose y que puede volver a moverse con normalidad
        personajeEstaMoviendose(false);
        setPersonajeMov(false);

        if (persoMoviendose != null && (!persoMoviendose.getMuerto())) {

            //Moveremos al personaje de la matriz a la posición en la que está en la escena (redondeando con round porque el movimiento del personaje puede ser algo impreciso)
            moverPersonajeEnMatriz(persoMoviendose, (int)System.Math.Round(persoMoviendose.transform.position.x), (int)System.Math.Round(persoMoviendose.transform.position.y));

            //Activaremos el booleano para que el controlador sepa que no tiene que mover a este personaje hastael siguiente turno
            persoMoviendose.setMovido(true);

            //Desactivar la interfaz de estadisticas
            controladorCanvas.gestionarCanvasEstadisticas(false);

            /*Desactivaremos la animación que se reproduce cuando tenemos al personaje seleccionado y activaremos la de cuando un personaje ya se ha movido en este turno para 
            que el usuario sepa que no podrá volver a mover a este personaje hasta el siguiente turno*/
            setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, false);
            setAnimacionPersonaje(persoMoviendose, Personaje.Movido, true);

            //Si no estamos en el turno del enemigo
            if (!turnoEnemigo) {

                //Comprobaremos si hay más personajes nuestros que no se hayan movido
                if (FinDeTurno()) {

                    if (!acabarJuego) {
                        //Si ya hemos movido a todas nuestras unidades, empezará el turno del enemigo
                        prepararTurnoEnemigo();
                    }

                }

            }

        }

    }

    //Corrutina que mueve al personaje por las celdas pasadas como parámetro
    private IEnumerator moverPersonaje (List <Celda> listaCeldas) {

        //Tiempo que dura el movimiento entre una celda y otra
        float tiempoMov = 0.5f;

        //Tiempo de inicio del movimiento
        float startTime;

        //Posición inicial del personaje
        Vector3 puntoDePartida = new Vector3(persoMoviendose.transform.position.x, persoMoviendose.transform.position.y, persoMoviendose.transform.position.z);

        //Siguiente celda
        Vector3 objetivo;

        //Guardaremos la referencia a la última animación usada
        string animacionAnterior = Personaje.Moviendose;

        //Bucle que recorrerá la lista de celdas (se hace a la inversa porque las celdas también se guardan a la inversa)
        for (int i = listaCeldas.Count - 2; i >= 0 ; i-- ) {

            //Guardamos el tiempo de inicio 
            startTime = Time.time;

            //Guardamos la siguiente celda en la lista
            objetivo = new Vector3(listaCeldas[i].getX(), listaCeldas[i].getY(), persoMoviendose.transform.position.z);

            //Desactivamos la animación anterior y pondremos la animación correspondiente según el movimiento
            if (objetivo.x > puntoDePartida.x) {
                if (animacionAnterior != Personaje.MovDerecha) {
                    setAnimacionPersonaje(persoMoviendose, animacionAnterior, false);
                    setAnimacionPersonaje(persoMoviendose, Personaje.MovDerecha, true);
                    
                    animacionAnterior = Personaje.MovDerecha;
                }
            }
            else if (objetivo.x < puntoDePartida.x) {
                if (animacionAnterior != Personaje.MovIzquierda) {
                    setAnimacionPersonaje(persoMoviendose, animacionAnterior, false);
                    setAnimacionPersonaje(persoMoviendose, Personaje.MovIzquierda, true);
                    
                    animacionAnterior = Personaje.MovIzquierda;
                }
            }
            else if (objetivo.y > puntoDePartida.y) {
                if (animacionAnterior != Personaje.MovArriba) {
                    setAnimacionPersonaje(persoMoviendose, animacionAnterior, false);
                    setAnimacionPersonaje(persoMoviendose, Personaje.MovArriba, true);
                    
                    animacionAnterior = Personaje.MovArriba;
                }
            }
            else if (objetivo.y < puntoDePartida.y) {
                if (animacionAnterior != Personaje.MovAbajo) {
                    setAnimacionPersonaje(persoMoviendose, animacionAnterior, false);
                    setAnimacionPersonaje(persoMoviendose, Personaje.MovAbajo, true);
                    
                    animacionAnterior = Personaje.MovAbajo;
                }
            }

            /*Empezaremos a mover al personaje a la siguiente celda mediante este bucle que, gracias al yield return null, en vez de ejecutar el movimiento de golpe, 
            lo hará progresivamente en cada frame*/
            while (Time.time < startTime + tiempoMov) {
                persoMoviendose.transform.position = Vector3.Lerp(puntoDePartida, objetivo, (Time.time - startTime) / tiempoMov);
                yield return null;
            }

            //Por si acaso, pondremos que la posición del personaje sea la misma que la del objetivo
            persoMoviendose.transform.position = objetivo;

            //Haremos que el nuevo punto de partida sea la celda a la que nos hemos movido para que en el proximo movimiento se mueva de esta a la siguiente
            puntoDePartida = new Vector3(listaCeldas[i].getX(), listaCeldas[i].getY(), persoMoviendose.transform.position.z);
        }

        //Al acabar el movimiento, desactivaremos la animación anterior y activaremos la del personaje seleccionado
        setAnimacionPersonaje(persoMoviendose, animacionAnterior, false);
        setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, true);

        //Le diremos al puntero que ya no está moviendose el personaje para que pueda volver a dar órdenes al controlador
        puntero.set_personajeEnMovimiento(false);

        //Pararemos el sonido de cuando el personaje se mueve
        persoMoviendose.GetComponent<AudioSource>().Stop();

        if (!turnoEnemigo) {
            //Una vez movido el personaje, activaremos la UI de los botones
            controladorCanvas.gestionarCanvasMov(true);
            //Y activaremos el botón de acción (atacar/curar) si tiene enemigos al alcance.
            comprobarPosiblesAcciones();
        }

    }

    //Comprueba si la unidad seleccionada puede atacar/curar a algún enemigo/aliado al alcance
    private void comprobarPosiblesAcciones () {

        //Declararemos una lista para guardar las celdas al alcance del personaje
        listaCeldasObjetivos = new List<Celda>();

        //Buscaremos las celdas al alcance
        buscarCeldasCercanas(listaCeldasObjetivos, persoMoviendose, (int)persoMoviendose.transform.position.x, (int)persoMoviendose.transform.position.y, 0);

        //Si el personaje es sanador, buscaremos aliados, si no lo es, buscaremos enemigos. También cambiaremos el texto del botón de acción dependiendo de la acciónque se vaya a ejecutar
        if (persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            controladorCanvas.cambiarTextoBotonAccion("Sanar");
            buscarAliadosCercanos();
        }
        else {
            controladorCanvas.cambiarTextoBotonAccion("Atacar");
            buscarEnemigosCercanos();
        }
        

    }

    //Busca todas las celdas que estén al alcance del personaje
    private void buscarCeldasCercanas(List<Celda> listaCeldas, Personaje personaje, int x, int y, int alcanceUsado) {

        //Comprobaremos que la celda no esta fuera del mapa
        if (x >= 0 && y >= 0 && x <= gestorDeMapas.limiteMapaX && y <= gestorDeMapas.limiteMapaY) {

            //Creamos la referencia a la celda con el alcance usado para llegar aquí
            Celda celda = new Celda(x, y, alcanceUsado);

            //Comprobamos que esta celda no está ya en la lista
            if (!listaCeldas.Contains(celda)) {

                //Si el alcance usado es mayor o igual al alcance mínimo del personaje, la celda se añadirá a la lista
                if (alcanceUsado >= personaje.getAlcanceMinimo()) {
                    listaCeldas.Add(celda);
                }

                //Si el alcance usado es menor que el alcance del personaje, seguiremos buscando buscando más celdas adyecentes
                if (alcanceUsado < personaje.getAlcance()) {
                    buscarCeldasCercanas(listaCeldas, personaje, x + 1, y, alcanceUsado + 1);
                    buscarCeldasCercanas(listaCeldas, personaje, x - 1, y, alcanceUsado + 1);
                    buscarCeldasCercanas(listaCeldas, personaje, x, y + 1, alcanceUsado + 1);
                    buscarCeldasCercanas(listaCeldas, personaje, x, y - 1, alcanceUsado + 1);
                }

            }
        }
        
    }

    //Buscaremos todos los enemigos al alcance del personaje dada una lista de las celdas adyecentes al alcance del personaje
    private void buscarEnemigosCercanos () {

        //En un principio diremos que no hay enemigos al alcance
        bool enemigosAlAlcance = false;

        //Comprobaremos a qué equipo pertenece el personaje que estamos moviendo
        //Equipo del jugador
        if (equipoJugador.Contains(persoMoviendose)) {

            //Recorremos la lista de objetivos
            for (int i = 0; i < listaCeldasObjetivos.Count; i++) {

                //Guardamos la referencia a la celda
                Celda celda = listaCeldasObjetivos[i];

                //Si esta unidad existe en el mapa
                if (mapa[celda.getX(), celda.getY()] != null) {

                    //Y esta unidad es enemiga
                    if (equipoEnemigo.Contains(mapa[celda.getX(), celda.getY()])) {

                        //Diremos que esta celda está ocupada por un enemigo
                        celda.setOcupada(true);

                        //Diremos que ahora si que hay enemigos al alcance
                        enemigosAlAlcance = true;

                    }
                }
            }

        }
        //Equipo del enemigo
        else {

            for (int i = 0; i < listaCeldasObjetivos.Count; i++) {
                Celda celda = listaCeldasObjetivos[i];
                if (mapa[celda.getX(), celda.getY()] != null) {
                    if (equipoJugador.Contains(mapa[celda.getX(), celda.getY()])) {
                        celda.setOcupada(true);
                        enemigosAlAlcance = true;
                    }
                }
            }

        }

        //Si hay enemigos al alcance, activaremos el botón de acción para que el usuario pueda atacar a estos enemigos
        if (enemigosAlAlcance) {
            controladorCanvas.setBotonAccion(true);
        }

    }

    /*Buscaremos todos los aliados al alcance del personaje no tengan la vida al máximo (ya que no debes poder curar a alguien que no ha sido dañado) 
    dada una lista de las celdas adyecentes al alcance del personaje*/
    private void buscarAliadosCercanos () {

        //En un principio diremos que no hay aliados al alcance
        bool aliadosAlAlcance = false;

        //En este caso sólo lo haremos para el equipo del jugador porque sólo habrá un sanador y este será del jugador
        if (equipoJugador.Contains(persoMoviendose)) {

            //Recorremos la lista de objetivos
            for (int i = 0; i < listaCeldasObjetivos.Count; i++) {

                //Guardamos la referencia a la celda
                Celda celda = listaCeldasObjetivos[i];

                //Si esta unidad existe en el mapa
                if (mapa[celda.getX(), celda.getY()] != null) {

                    //Esta unidad es aliada
                    if (equipoJugador.Contains(mapa[celda.getX(), celda.getY()])) {

                        //Y esta unidad no es él mismo y no tiene la vida al máximo
                        if (mapa[celda.getX(), celda.getY()] != persoMoviendose && mapa[celda.getX(), celda.getY()].getPV() < mapa[celda.getX(), celda.getY()].getPV_Maximos()) {

                            //Diremos que esta celda está ocupada por un aliado
                            celda.setOcupada(true);

                            //Diremos que ahora si que hay aliados al alcance
                            aliadosAlAlcance = true;

                        }
                    }
                }
            }

        }

        //Si hay aliados al alcance, activaremos el botón de acción para que el usuario pueda curar a estos aliados
        if (aliadosAlAlcance) {
            controladorCanvas.setBotonAccion(true);
        }
    }

    /*Este método será usado una vez se pulse el botón de acción para entrar en el modo de acción. 
    Este modo eliminará todos los cuadros azules y rojos (sólo de la escena, no borrará las listas que contienen las referencias a todas estas celdas porque si luego pulsamos cancelar
    tendremos que volver a pintarlas en la escena) del personaje seleccionado y luego mostrará por pantalla las celdas de la lista de objetivos de color rojo o verde dependiendo del 
    tipo del personaje.*/
    public void entrarEnModoAccion () {

        //Destruiremos todas las celdas azules y rojas/verdes
        destruirCeldas();

        //Desactivaremos los canvas de las opciones de movimiento y estadísticas
        controladorCanvas.gestionarCanvasMov(false);
        controladorCanvas.gestionarCanvasEstadisticas(false);

        //Activaremos los canvas de las opciones de ataque o curacion y previsiones
        controladorCanvas.gestionarCanvasAtaqueCuracion(true);
        controladorCanvas.gestionarCanvasPrevisiones(true);

        //Pinta por pantalla los objetivos sacados con los métodos de buscarEnemigosCercanos() o buscarAliadosCercanos()
        pintarObjetivos();

        //Cambiaremos el tipo de movimiento del puntero para que sólo pueda moverse por las casillas pintadas anteriormente y no por las que acabamos de borrar
        accionEnCurso = true;

        //Guardamos la referencia al primer objetivo en la lista
        objetivo = mapa[listaCeldasObjetivos[primerObjetivo].getX(), listaCeldasObjetivos[primerObjetivo].getY()];

        //Movemos el puntero "automaticamente" a la posición de ese objetivo
        puntero.moverPuntero(objetivo.getX(), objetivo.getY());

        //Mostraremos las previsiones ante un posible ataque ante estas dos unidades en caso de que el jugador quisiera atacar a esa unidad
        mostrarPrevisionesUnidadSeleccionada();
        mostrarPrevisionesObjetivo();

        //Desactivaremos la animación del personaje cuando está seleccionado
        setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, false);

    }

    //Devuelve true si la unidad objetivo podrá devolver el golpe al ser atacada por la unidad seleccionada
    private bool enemigoPuedeDevolverGolpe (Personaje personajeObjetivo) {

        //Creamos una lista  que contendrá todas las celdas al alcance del objetivo
        List<Celda> listaCeldasPersonajeObjetivo = new List<Celda>();

        //Buscamos todas las celdas cercanas al alcance del personaje
        buscarCeldasCercanas(listaCeldasPersonajeObjetivo, personajeObjetivo, personajeObjetivo.getX(), personajeObjetivo.getY(), 0);

        /*Comprobamos si la unidad seleccionada se encuentra dentro de esta lista, si esta quiere decir que el objetivo podrá devolver el golpe, en caso contrario no lo hará porque
        este no estará a su alcance*/
        if (listaCeldasPersonajeObjetivo.Contains(new Celda((int)persoMoviendose.transform.position.x, (int)persoMoviendose.transform.position.y))) {
            return true;
        }
        else {
            return false;
        }
    }

    //Muestra las previsiones de la unidad seleccionada en caso de que ataque al objetivo seleccionado
    public void mostrarPrevisionesUnidadSeleccionada () {

        //Inicializamos el vector donde guardaremos las previsiones
        previsionesUnidadSeleccionada = new int[tamanyoVectorPrevisiones];

        //Desactivaremos el texto que muestra cuando una unidad puede hacer un ataque doble (si está activado)
        controladorCanvas.AtaqueDobleUnidadSeleccionada(false);

        //Guardamos la vida y exp del personaje seleccionado
        previsionesUnidadSeleccionada[Vida] = persoMoviendose.getPV();
        previsionesUnidadSeleccionada[Exp] = persoMoviendose.getExp();

        //Las guardamos en un string temporal
        string vida = previsionesUnidadSeleccionada[Vida] + "";
        string danyo;
        string golpe;
        int exp = previsionesUnidadSeleccionada[Exp];

        //Comprobaremos si el personaje seleccionado es sanador
        if (persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            //Si es sanador, no hará falta hacer ninguna previsión porque sólo va a curar
            danyo = "-";
            golpe = "-";
        }
        else {
            //En el caso de que no sea sanador, calculará las previsiones ante un posible combate y las guardará
            calcularPrevisionesUnidadSeleccionada();
            danyo = previsionesUnidadSeleccionada[DanyoCausado] + "";
            golpe = previsionesUnidadSeleccionada[ProbGolpe] + "%";
        }

        //Por último, lo mostraremos por pantalla
        controladorCanvas.mostrarPrevisionesUnidadMoviendose(persoMoviendose, vida, danyo, golpe, exp);

    }

    //Calcula las previsiones de la unidad seleccionada en caso de que ataque al objetivo seleccionado
    private void calcularPrevisionesUnidadSeleccionada() {

        float totalDanyo = 0;

        //Cogeremos las bonificaciones de terreno que tiene la unidad objetivo
        int bonificacionDefensaTerreno = Mapa.terreno_bonificacionDefensa[caracteristicasCeldas[(int)objetivo.transform.position.x, (int)objetivo.transform.position.y]];
        int bonificacionEsquiveTerreno = Mapa.terreno_bonificacionGolpe[caracteristicasCeldas[(int)objetivo.transform.position.x, (int)objetivo.transform.position.y]];

        //Calcularemos el equive que tendrá el objetivo
        float esquive_personajeEnemigo = (2 * objetivo.getVelocidad()) + objetivo.getSuerte() + bonificacionEsquiveTerreno;

        //Calcularemos nuestra probabilidad de golpe a este objetivo
        float totalProbGolpe = (Personaje.Golpe + ((2 * persoMoviendose.getHabilidad()) + (0.5f * persoMoviendose.getSuerte()))) - esquive_personajeEnemigo;

        //Filtraremos por tipo de unidad para calcular el daño, si es mago atacará con magia, sino, atacará con fuerza
        if (persoMoviendose is Mago) {
            totalDanyo = Personaje.Ataque + persoMoviendose.getMagia() - objetivo.getResistencia() - bonificacionDefensaTerreno;
        }
        else {
            totalDanyo = Personaje.Ataque + persoMoviendose.getFuerza() - objetivo.getDefensa() - bonificacionDefensaTerreno;
        }

        //Si el golpe o el daño son menores que 0, los pondremos en 0
        if (totalDanyo < 0) totalDanyo = 0;
        if (totalProbGolpe < 0) totalProbGolpe = 0;

        //Redondearemos las previsiones antes de guardarlas
        previsionesUnidadSeleccionada[DanyoCausado] = Mathf.FloorToInt(totalDanyo);
        previsionesUnidadSeleccionada[ProbGolpe] = Mathf.FloorToInt(totalProbGolpe);

        //Si tenemos más velocidad que la de nuestro atacante + 4, le podremos hacer un ataque doble
        if (persoMoviendose.getVelocidad() >= objetivo.getVelocidad() + 4) {

            //Activa el texto que indica al usuario que puede hacer un ataque doble
            controladorCanvas.AtaqueDobleUnidadSeleccionada(true);
            previsionesUnidadSeleccionada[Ataques] = 2;
        }
        else {
            previsionesUnidadSeleccionada[Ataques] = 1;
        }

    }

    //Comprueba que la unidad que va a atacar es seleccionable filtrando posibles errores como atacar a alguien que no está en tu rango de ataque
    private bool esSeleccionable (Personaje personaje) {
        
        if (personaje != null) {

            if (persoMoviendose != personaje) {

                /*Si el personaje no es null ni es el mismo personaje que va a atacarle (no puedes atacarte a ti mismo), comprobaremos entre todas las celdas a tu alcance para ver
                si el personaje que has seleccionado está a tu alcance*/

                for (int i = 0; i < listaCeldasObjetivos.Count; i++) {
                    if (listaCeldasObjetivos[i].getOcupada()) {
                        if (personaje.getX() == listaCeldasObjetivos[i].getX() && personaje.getY() == listaCeldasObjetivos[i].getY()) {
                            return true;
                        }
                    }
                }

                return false;

            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    //Muestra las previsiones del objetivo en el caso de que este se defendiera ante un ataque de la unidad seleccionada
    public void mostrarPrevisionesObjetivo () {

        //Desactiva el texto que indica que puede hacer un ataque doble
        controladorCanvas.AtaqueDobleObjetivo(false);

        //Comprobaremos si el objetivo es seleccionable para prevenir algún posible error
        if (esSeleccionable(objetivo)) {

            //Reiniliciaremos el vector que guardará las previsiones para cuando el usuario vaya a atacar
            previsionesObjetivo = new int[tamanyoVectorPrevisiones];

            //Guardaremos la vida del objetivo
            previsionesObjetivo[Vida] = objetivo.getPV();
            previsionesObjetivo[Exp] = objetivo.getExp();

            //La convertiremos en un string temporal para pasarla a la UI y que se muestre por pantalla
            string vida = previsionesObjetivo[Vida] + "";
            int exp = previsionesObjetivo[Exp];

            //Si la unidad seleccionada o el objetivo es un sanador, no se hará ningún cálculo porque no se van a atacar
            if (persoMoviendose.gettipoUnidad() == Personaje.Sanador || objetivo.gettipoUnidad() == Personaje.Sanador) {
                previsionesObjetivo[DanyoCausado] = -1;
                previsionesObjetivo[ProbGolpe] = -1;
            }
            else {
                //Si ninguno de los dos es sanador, procederemos a calcular las previsiones
                calcularPrevisionesDelEnemigo();
            }

            /*Al igual que con la vida, crearemos unos string temporales para luego mostrar estas previsiones en la UI, el vector seguirá inalterado después de 
            calcular las previsiones hasta que el usuario ataque o hasta que el usuario cambie de objetivo (y con ello se vuelvan a calcular otras previsiones)*/
            string danyo;
            string golpe;
            if (previsionesObjetivo[DanyoCausado] == -1 && previsionesObjetivo[ProbGolpe] == -1) {
                danyo = "-";
                golpe = "-";
            }
            else {
                danyo = previsionesObjetivo[DanyoCausado] + "";
                golpe = previsionesObjetivo[ProbGolpe] + "%";
            }

            //Una vez calculadas las previsiones, se mostrarán por pantalla
            controladorCanvas.mostrarPrevisionesUnidadObjetivo(objetivo, vida, danyo, golpe, exp);

        } //END if

    }

    //Calcula las previsiones del daño y probabilidad de golpe del enemigo en caso de que este pueda defenderse de un ataque
    private void calcularPrevisionesDelEnemigo() {

        //Inicializaremos unas variables temporales para calcular las previsiones, así no tendremos que referenciar al vector cada vez
        float esquive_personajeSeleccionado = 0;
        float totalDanyo = 0;
        float totalProbGolpe = 0;

        //Si el objetivo tiene rango suficiente para defenderse, procederemos a calcular las previsiones
        if (enemigoPuedeDevolverGolpe(objetivo)) {

            //Cogeremos las bonificaciones de terreno que tiene la unidad que nos atacará
            int bonificacionDefensaTerreno = Mapa.terreno_bonificacionDefensa[caracteristicasCeldas[(int)persoMoviendose.transform.position.x, (int)persoMoviendose.transform.position.y]];
            int bonificacionEsquiveTerreno = Mapa.terreno_bonificacionGolpe[caracteristicasCeldas[(int)persoMoviendose.transform.position.x, (int)persoMoviendose.transform.position.y]];

            //Calcularemos el equive que tendrá el personaje que nos atacará
            esquive_personajeSeleccionado = (2 * persoMoviendose.getVelocidad()) + persoMoviendose.getSuerte() + bonificacionEsquiveTerreno;

            //Calcularemos nuestra probabilidad de golpe a ese personaje
            totalProbGolpe = Personaje.Golpe + ((2 * objetivo.getHabilidad()) + (0.5f * objetivo.getSuerte())) - esquive_personajeSeleccionado;

            //Filtraremos por tipo de unidad para calcular el daño, si es mago atacará con magia, sino, atacará con fuerza
            if (objetivo is Mago) {
                totalDanyo = Personaje.Ataque + objetivo.getMagia() - persoMoviendose.getResistencia() - bonificacionDefensaTerreno;
            }
            else {
                totalDanyo = Personaje.Ataque + objetivo.getFuerza() - persoMoviendose.getDefensa() - bonificacionDefensaTerreno;
            }

            //Si el golpe o el daño son menores que 0, los pondremos en 0
            if (totalDanyo < 0) totalDanyo = 0;
            if (totalProbGolpe < 0) totalProbGolpe = 0;

            //Redondearemos las previsiones antes de guardarlas
            previsionesObjetivo[DanyoCausado] = Mathf.FloorToInt(totalDanyo);
            previsionesObjetivo[ProbGolpe] = Mathf.FloorToInt(totalProbGolpe);

            //Si tenemos más velocidad que la de nuestro atacante + 4, le podremos hacer un ataque doble
            if (objetivo.getVelocidad() >= persoMoviendose.getVelocidad() + 4) {
                //Activa el texto que indica al usuario que puede hacer un ataque doble
                controladorCanvas.AtaqueDobleObjetivo(true);
                previsionesObjetivo[Ataques] = 2;
            }
            else {
                previsionesObjetivo[Ataques] = 1;
            }

        }
        //En caso de que no pueda devolver el golpe, dejaremos estas variables en -1 para indicar que no podemos devolver el golpe y el número de ataques en 0
        else {
            previsionesObjetivo[DanyoCausado] = -1;
            previsionesObjetivo[ProbGolpe] = -1;
            previsionesObjetivo[Ataques] = 0;
        }
    }

    //Pinta por pantalla los objetivos sacados con los métodos de buscarEnemigosCercanos() o buscarAliadosCercanos()
    private void pintarObjetivos () {

        //Guardaremos el index del primer objetivo de la lista en esta variable para luego mover el puntero a esta casilla "automaticamente"
        primerObjetivo = -1;

        //Aquí guardaremos todas las instancias de las celdas antes de añadirlas a la lista
        GameObject celda;

        //Cuadro que mostraremos por pantalla
        GameObject cuadro;

        //Dependiendo de si el personaje es sanador o no, los cuadros de los alrededores (según el alcance) serán verdes o rojos
        if (persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            cuadro = cuadroVerde;
        }
        else {
            cuadro = cuadroRojo;
        }

        /*Pasamos el cuadro que queramos pintar y pintaremos todas las casillas a las que el personaje podra atacar o curar con este cuadro*/
        for (int i = 0; i < listaCeldasObjetivos.Count; i++) {

            //Si la celda está ocupada, quiere decir que es un objetivo el cual hay que pintar
            if (listaCeldasObjetivos[i].getOcupada()) {

                //Instanciaremos la celda y guardaremos su referencia para añadirlo a la lista de celdas donde guardaremos todos nuestros objetivos
                celda = (GameObject) Instantiate(cuadro, new Vector3(listaCeldasObjetivos[i].getX(), listaCeldasObjetivos[i].getY(), 0), Quaternion.identity);
                listaCeldas.Add(celda);

                //Guardaremos el index del primer objetivo encontrado
                if (primerObjetivo == -1) {
                    primerObjetivo = i;
                }

            } //END IF
        } //END FOR

    } //END pintarObjetivos()

    /*Si una vez en el modo ataque el usuario pulsa "cancelar", volveremos a dejar al personaje donde estaba y volveremos a pintar todas las posibles rutas que este podrá tomar por
    si el jugador desea moverlo a otro sitio*/
    public void volverAlModoMoverse () {

        //Destruiremos las celdas de los objetivos disponibles
        destruirCeldas();

        //Restauraremos la posición del personaje a su posición original
        restaurarPosPersonaje();

        //Volveremos a pintar todas las rutas que el personaje podrá tomar
        pintarRutas();

        //Activaremos los canvas del modo "mover a un personaje"
        controladorCanvas.gestionarCanvasMov(true);
        controladorCanvas.gestionarCanvasEstadisticas(true);

        //Desactivaremos el botón de acción (Atacar/Curar) del canvas de las opciones de movimiento
        controladorCanvas.setBotonAccion(false);

        //Se volverán a comprobar las posibles acciones que puede ejecutar la unidad seleccionada en su posición actual
        comprobarPosiblesAcciones();

        /*Desactivaremos la bandera que indica si hay una acción en curso o no (esto se usaba para indicarle al puntero por qué celdas podía moverse, si por las celdas que
        estén dentro de las listas de las posibles rutas que puede tomar un personaje o por la de los objetivos.*/
        accionEnCurso = false;

        //Volveremos a activar la animación del personaje cuando lo seleccionas
        setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, true);

    }

    /*Una vez el usuario confirme la acción, el personaje seleccionado llevará a cabo el ataque o curación*/
    public void accion () {

        //Le diremos al puntero que no pueda moverse, de esta forma el usuario no podrá moverlo de ninguna forma hasta que acabe la acción
        puntero.set_esPosibleMoverPuntero(false);

        //Desactivaremos el canvas de confirmación de la acción
        controladorCanvas.gestionarCanvasAtaqueCuracion(false);

        //Destruiremos las celdas de los objetivos disponibles
        destruirCeldas();

        //Dependiendo de si la unidad seleccionada es sanador o no, atacaras o sanarás
        if (persoMoviendose.gettipoUnidad() == Personaje.Sanador) {
            StartCoroutine(curar());
        }
        else {
            StartCoroutine(atacar());
        }

    }

    //Hace que la unidad que estás moviendo curé al aliado seleccionado
    private IEnumerator curar () {

        //Cantidad de puntos de vida que sanarás
        int cantidadCurada = 10;

        //Recogemos la vida del objetivo aliado
        int vida_O = previsionesObjetivo[Vida];

        //La cantidad sanada no puede sobrepasar los puntos de vida máximos del personaje
        if (vida_O + cantidadCurada > objetivo.getPV_Maximos()) {
            vida_O = objetivo.getPV_Maximos();
        }
        else {
            vida_O += cantidadCurada;
        }

        //Activaremos la animación de curar
        setAnimacionPersonaje(persoMoviendose, Personaje.Curar, true);

        //Esperaremos un momento antes de curar para que no sea instantaneo
        yield return new WaitForSeconds(1.3f);

        //Curaremos al objetivo
        controladorSonidos.reproducirClip(ControladorSonidos.c_curar);
        controladorCanvas.modificarVidaObjetivo(vida_O + "");
        objetivo.setPV(vida_O);

        //Volveremos a esperar a que termine la animación y no quede tan brusco el cambio entre animaciones
        yield return new WaitForSeconds(2.0f);

        //Desactivaremos la animación de curar
        setAnimacionPersonaje(persoMoviendose, Personaje.Curar, false);

        //El curandero ganará 20 puntos de experiencia al curar
        yield return StartCoroutine(subirExpPersonaje(persoMoviendose, expAccion, true));

        //Esperaremos otro segundo para que al jugador le de tiempo a ver los cambios por pantalla
        yield return new WaitForSeconds(1.0f);

        //Desactivaremos el canvas de previsiones
        controladorCanvas.gestionarCanvasPrevisiones(false);

        //Bajaremos la bandera para indicar que la acción se ha acabado
        accionEnCurso = false;

        //Permitiremos que el usuario pueda volver a mover el puntero
        puntero.set_esPosibleMoverPuntero(true);

        //Ejecutaremos exactamente las mismas acciones que el botón esperar
        controladorCanvas.BotonEsperar();

        //Desactivaremos el botón de acción del canvas del movimiento para que si luego seleccionas otra unidad no aparezca ya activada
        controladorCanvas.setBotonAccion(false);

        //Borraremos las referencias a ambos personajes
        persoMoviendose = null;
        objetivo = null;

    }

    //Hace que la unidad que estás moviendo ataque al enemigo seleccionado
    private IEnumerator atacar () {

        //Recupera las previsiones calculadas anteriormente para ejecutar los ataques
        //Previsiones de la unidad seleccionada (US)
        int vida_US = previsionesUnidadSeleccionada[Vida];
        int danyo_US = previsionesUnidadSeleccionada[DanyoCausado];
        int golpe_US = previsionesUnidadSeleccionada[ProbGolpe];
        //Número de ataques que la unidad podrá ejecutar
        int ataques_US = previsionesUnidadSeleccionada[Ataques];

        //Previsiones del objetivo (O)
        int vida_O = previsionesObjetivo[Vida];
        int danyo_O = previsionesObjetivo[DanyoCausado];
        int golpe_O = previsionesObjetivo[ProbGolpe];
        //Número de ataques que la unidad podrá ejecutar
        int ataques_O = previsionesObjetivo[Ataques];
        bool objetivoSeHaDefendido = ataques_O > 0;

        //Calcula nuevas previsiones que no se calcularon antes porque no se muestran por pantalla (golpes críticos)
        //Evasión de críticos
        int evasionDeCriticos_US = persoMoviendose.getSuerte();
        int evasionDeCriticos_O = objetivo.getSuerte();

        //Probabilidad de críticos
        int critico_US = Mathf.FloorToInt(0.5f * persoMoviendose.getHabilidad() - evasionDeCriticos_O);
        int critico_O = Mathf.FloorToInt(0.5f * objetivo.getHabilidad() - evasionDeCriticos_US);

        //Número aleatorio que se utilizará para controlar las probabilidades
        int alea;
        //Variable que contendrá el daño que se inflingirán los personajes
        int danyo;
        //Variable que decidirá si el golpe dado da en el blanco o falla
        bool acertar;
        //Referenciaremos al sonido de ataque que queramos reproducir
        string sonido_a_reproducir;

        //Variables que controlarán el movimiento de los personajes durante los ataques
        //Tiempo que durará cada movimiento (ida y vuelta = 1 segundo)
        float tiempoMov = 0.5f;
        //Tiempo donde el personaje empezará a moverse
        float startTime;
        //Casilla de la unidad seleccionada
        Vector3 casilla_US = new Vector3(persoMoviendose.transform.position.x, persoMoviendose.transform.position.y, persoMoviendose.transform.position.z);
        //Casilla del objetivo
        Vector3 casilla_O = new Vector3(objetivo.transform.position.x, objetivo.transform.position.y, objetivo.transform.position.z);
        //Nombre de las animaciones que usaran las unidades
        string animacionJugador;
        string animacionEnemigo;

        //Elegimos las animaciones que usaran los personajes
        //Ataques verticales y horizontales
        if (persoMoviendose.transform.position.x > objetivo.transform.position.x && persoMoviendose.transform.position.y == objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueIzquierda;
            animacionEnemigo = Personaje.AtaqueDerecha;
        }
        else if (persoMoviendose.transform.position.x < objetivo.transform.position.x && persoMoviendose.transform.position.y == objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueDerecha;
            animacionEnemigo = Personaje.AtaqueIzquierda;
        }
        else if (persoMoviendose.transform.position.x == objetivo.transform.position.x && persoMoviendose.transform.position.y > objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueAbajo;
            animacionEnemigo = Personaje.AtaqueArriba;
        }
        else if (persoMoviendose.transform.position.x == objetivo.transform.position.x && persoMoviendose.transform.position.y < objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueArriba;
            animacionEnemigo = Personaje.AtaqueAbajo;
        }
        //Ataques verticales (sólo las unidades que tengan 2 de alcance o más podrán hacerlos)
        else if (persoMoviendose.transform.position.x < objetivo.transform.position.x && persoMoviendose.transform.position.y < objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueArriba;
            animacionEnemigo = Personaje.AtaqueAbajo;
        }
        else if (persoMoviendose.transform.position.x > objetivo.transform.position.x && persoMoviendose.transform.position.y > objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueAbajo;
            animacionEnemigo = Personaje.AtaqueArriba;
        }
        else if (persoMoviendose.transform.position.x > objetivo.transform.position.x && persoMoviendose.transform.position.y < objetivo.transform.position.y) {
            animacionJugador = Personaje.AtaqueIzquierda;
            animacionEnemigo = Personaje.AtaqueDerecha;
        }
        else {
            animacionJugador = Personaje.AtaqueDerecha;
            animacionEnemigo = Personaje.AtaqueIzquierda;
        }

        //Activaremos las animaciones seleccionadas arriba
        setAnimacionPersonaje(persoMoviendose, animacionJugador, true);
        setAnimacionPersonaje(objetivo, animacionEnemigo, true);

        //Una vez inicializadas las variables y activadas las animaciones correspondientes, comenzará el ataque.
        /*Mientras el número de ataques que las 2 unidades pueden ejecutar sea mayor de 0, se continuarán atacando primero la unidad seleccionada y luego el que se defiende*/
        while (ataques_US > 0 || ataques_O > 0) {

            //----------------------------------------------------------
            //UNIDAD SELECCIONADA
            //Comprobaremos que nuestra unidad puede atacar
            if (ataques_US > 0) {

                //Genera un número aleatorio entre 0 y 100 (0 y 100 incluidos)
                alea = Random.Range(0, 100);

                //Si el número aleatorio es menor o igual que la probabilidad de golpe, la unidad acertará el ataque, en caso contrario fallará y no le harás ningún daño a tu enemigo
                acertar = alea <= golpe_US;
                
                alea = Random.Range(0, 100);

                //En caso de que hayas acertado, inflingirás daño, en el caso contrario no harás ningún daño al enemigo
                if (acertar) {
                    //En caso de que el número aleatorio sea menor que la probabilidad de crítico, tu unidad hará un golpe crítico (daño x 3)
                    if (alea <= critico_US) {
                        danyo = danyo_US * danyo_criticos;
                        sonido_a_reproducir = ControladorSonidos.c_critico;
                        controladorSonidos.reproducirClip(ControladorSonidos.c_inicio_critico);
                    }
                    else {
                        danyo = danyo_US;
                        sonido_a_reproducir = ControladorSonidos.c_estandar;
                    }
                }
                else {
                    danyo = 0;
                    sonido_a_reproducir = ControladorSonidos.c_fallar;
                }

                startTime = Time.time;

                //Movemos al personaje que atacará hacia la posición del objetivo
                while (Time.time < startTime + tiempoMov) {
                    persoMoviendose.transform.position = Vector3.Lerp(casilla_US, casilla_O, (Time.time - startTime) / tiempoMov);
                    yield return null;
                }

                //Reproduciremos el sonido del ataque
                controladorSonidos.reproducirClip(sonido_a_reproducir);

                //Se le resta la cantidad total de daño a la vida del personaje enemigo
                vida_O = (vida_O - danyo) < 0 ? 0 : vida_O - danyo;

                //Mostraremos la vida en la UI para que el usuario vea los resultados del ataque
                controladorCanvas.modificarVidaObjetivo(vida_O+"");

                //Si la vida del objetivo es 0, el enemigo morirá
                if (vida_O == 0) {
                    objetivo.setMuerto(true);
                    StartCoroutine("matarPersonaje", objetivo);
                }
                //Si no ha muerto, le restaremos la cantidad calculada
                else {
                    objetivo.setPV(vida_O);
                }

                startTime = Time.time;

                //Movemos a nuestro personaje de nuevo a su celda
                while (Time.time < startTime + tiempoMov) {
                    persoMoviendose.transform.position = Vector3.Lerp(casilla_O, casilla_US, (Time.time - startTime) / tiempoMov);
                    yield return null;
                }

                //Haremos que la posición del personaje vuelva a casilla exacta, ya que el método anterior puede resultar algo innexacto y provocar errores
                persoMoviendose.transform.position = new Vector3((int)System.Math.Round(persoMoviendose.transform.position.x), (int)System.Math.Round(persoMoviendose.transform.position.y), persoMoviendose.transform.position.z);

                //Una vez haya atacado, le restaremos 1 al número de ataques que el personaje puede hacer durante este ataque
                ataques_US--;

                //En caso de que el objetivo haya muerto, interrumpiremos el bucle, ya que este no podrá devolver el golpe ni tú atacar una segunda vez (en el caso de que pudieras)
                if (vida_O == 0) {
                    break;
                }

            }
            //END UNIDAD SELECCIONADA
            //----------------------------------------------------------
            yield return new WaitForSeconds(0.3f);
            //----------------------------------------------------------
            //UNIDAD ENEMIGA
            //Comprobaremos que nuestra unidad puede atacar
            if (ataques_O > 0) {

                //Genera un número aleatorio entre 0 y 100 (0 y 100 incluidos)
                alea = Random.Range(0, 100);

                acertar = alea <= golpe_O;

                alea = Random.Range(0, 100);

                if (acertar) {
                    if (alea <= critico_O) {
                        danyo = danyo_O * danyo_criticos;
                        controladorSonidos.reproducirClip(ControladorSonidos.c_inicio_critico);
                        sonido_a_reproducir = ControladorSonidos.c_critico;
                    }
                    else {
                        danyo = danyo_O;
                        sonido_a_reproducir = ControladorSonidos.c_estandar;
                    }
                }
                else {
                    danyo = 0;
                    sonido_a_reproducir = ControladorSonidos.c_fallar;
                }

                startTime = Time.time;
                //Movemos al enemigo a la posición de nuestro personaje, para que se defienda
                while (Time.time < startTime + tiempoMov) {
                    objetivo.transform.position = Vector3.Lerp(casilla_O, casilla_US, (Time.time - startTime) / tiempoMov);
                    yield return null;
                }

                controladorSonidos.reproducirClip(sonido_a_reproducir);

                vida_US = (vida_US - danyo) < 0 ? 0 : vida_US - danyo;

                controladorCanvas.modificarVidaUnidadSeleccionada(vida_US + "");

                if (vida_US == 0) {
                    persoMoviendose.setMuerto(true);
                    StartCoroutine("matarPersonaje", persoMoviendose);
                }
                else {
                    persoMoviendose.setPV(vida_US);
                }

                startTime = Time.time;

                //El enemigo volverá a su posición después de defenderse
                while (Time.time < startTime + tiempoMov) {
                    objetivo.transform.position = Vector3.Lerp(casilla_US, casilla_O, (Time.time - startTime) / tiempoMov);
                    yield return null;
                }

                //Haremos que la posición del personaje vuelva a su punto inicial exacto, ya que el método anterior puede resultar algo innexacto y provocar errores
                objetivo.transform.position = new Vector3((int)System.Math.Round(objetivo.transform.position.x), (int)System.Math.Round(objetivo.transform.position.y), objetivo.transform.position.z);

                ataques_O--;

                if (vida_US == 0) {
                    break;
                }

            }

            //END UNIDAD ENEMIGA
            //----------------------------------------------------------


        } // END FOR

        //Una vez terminado el ataque, desactivaremos las animaciones del ataque
        setAnimacionPersonaje(persoMoviendose, animacionJugador, false);
        setAnimacionPersonaje(objetivo, animacionEnemigo, false);

        /*Al final de una batalla, el jugador ganará 20 de experiencia si es el atacante o 10 si es el que se defiende (si puede defenderse, si no tiene alcance suficiente 
        y no puede contraatacar no ganará experiencia).
        Nota: poniendo la corrutina en un return haces que esta corrutina no siga ejecutándose hasta que subirExpPersonaje acabe de ejecutarse*/
        if (equipoJugador.Contains(persoMoviendose)) {
            if (!persoMoviendose.getMuerto() && !acabarJuego) {
                yield return StartCoroutine(subirExpPersonaje(persoMoviendose, expAccion, true));
            }
        }
        else {
            if (!objetivo.getMuerto() && !acabarJuego) {
                if (objetivoSeHaDefendido) {
                    yield return StartCoroutine(subirExpPersonaje(objetivo, expDefensa, false));
                }
            }
        }

        //Esperaremos un momento para que el usuario pueda ver el resultado de la batalla
        yield return new WaitForSeconds(1.0f);

        //Y para finalizar haremos los cambios convenientes en la matriz y dejaremos que el puntero se pueda mover otra vez para que el usuario pueda mover a otros personajes
        //Desactivaremos el canvas de previsiones
        controladorCanvas.gestionarCanvasPrevisiones(false);

        //Bajaremos la bandera para indicar que la acción se ha acabado
        accionEnCurso = false;

        //Permitiremos que el usuario pueda volver a mover el puntero
        if (!turnoEnemigo) {
            puntero.set_esPosibleMoverPuntero(true);
        }

        //Desactivaremos el botón de acción del canvas del movimiento para que si luego seleccionas otra unidad no aparezca ya activada
        controladorCanvas.setBotonAccion(false);

        //Ejecutaremos exactamente las mismas acciones que el botón esperar
        controladorCanvas.BotonEsperar();
        
        //Borraremos las referencias a ambos personajes
        persoMoviendose = null;
        objetivo = null;

    } //END atacar

    //Mataremos al personaje pasado como parámetro
    public IEnumerator matarPersonaje (Personaje personaje) {

        bool victoria = false;

        //Si estamos en el turno enemigo, el puntero estará siguiendo al personaje. Para evitar que intente acceder a su transformcuando muera, le diremos que ya no es hijo de este personaje
        if (puntero.transform.parent == personaje.transform) {
            puntero.transform.parent = null;
        }

        //Lo borramos de la lista de equipos
        if (equipoJugador.Contains(personaje)) {
            borrarJugador(personaje);
            //Si nos han matado a todos nuestros personajes, perderemos
            if (equipoJugador.Count <= 0) {
                acabarJuego = true;
                victoria = false;
            }
        }
        else {
            //Si el personaje que hemos matado es el rey, habremos ganado
            if (personaje.GetType() == typeof(Gangrel)) {
                acabarJuego = true;
                victoria = true;
            }
            borrarEnemigo(personaje);
        }

        //Reproducimos la animación de muerte
        Animator anim = personaje.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Muerto");

        //Lo borramos de la matriz
        mapa[personaje.getX(), personaje.getY()] = null;

        //Esperamos 1 segundo y medio (que es lo que dura la animación) antes de continuar
        yield return new WaitForSeconds(1.3f);

        //Lo destruimos de la escena
        Destroy(personaje.gameObject);

        //Si se ha acabado el juego, comprobaremos si hemos ganado o hemos perdido y mostraremos el correspondiente mensaje
        if (acabarJuego) {

            StartCoroutine(acabarElJuego(victoria));

        }

    }

    //Hace que se acabe el juego y vuelva a la pantalla principal
    private IEnumerator acabarElJuego (bool victoria) {

        if (victoria) {
            controladorSonidos.parar_musica_de_fondo();
            controladorCanvas.gestionarCanvas_victoria(true);
            controladorSonidos.reproducirClip(ControladorSonidos.aj_victoria);
        }
        else {
            controladorSonidos.parar_musica_de_fondo();
            controladorCanvas.gestionarCanvas_derrota(true);
            controladorSonidos.reproducirClip(ControladorSonidos.aj_derrota);
        }

        yield return new WaitForSeconds(10.0f);

        SceneManager.LoadScene("Pantalla_principal");

    }

    //Vuelve a la pantalla de título, este método lo usará el puntero cuando el jugador pulse la tecla de escape
    public void salir_del_juego () {
        controladorSonidos.parar_musica_de_fondo();
        SceneManager.LoadScene("Pantalla_principal");
    }

    //Se usará despues de un ataque o curación, le dará experiencia al personaje pasado como parámetro
    private IEnumerator subirExpPersonaje (Personaje personaje, int expGanada, bool expDelJugador) {

        int expPersonaje;

        //Activamos el sonido que hará la barra al incrementarse
        controladorCanvas.gestionarSonidoExpJugador(true);

        //Un bucle de x número de iteraciones que irá subiendo la barra progresivamente de 1 en 1 para que esta no suba de golpe
        for (int i = 0 ; i < expGanada ; i++) {

            //Aquí modificaremos la barra del jugador o del enemigo dependiendo de si hemos pasado true (jugador) o false (enemigo)
            if (expDelJugador) {

                //Si la función de subir exp devuelve -1 quiere decir que esa unidad ha subido un nivel
                if (controladorCanvas.sumarExpJugador() == -1) {
                    personaje.subirDeNivel();
                    controladorSonidos.reproducirClip(ControladorSonidos.c_subir_nivel);
                }

            }
            else {

                if (controladorCanvas.sumarExpEnemigo() == -1) {
                    personaje.subirDeNivel();
                    controladorSonidos.reproducirClip(ControladorSonidos.c_subir_nivel);
                }

            }

            yield return new WaitForSeconds(0.08f);

        }

        //Obtenemos la experiencia total del personaje
        if (expDelJugador) {
            expPersonaje = controladorCanvas.getExpJugador();
        }
        else {
            expPersonaje = controladorCanvas.getExpObjetivo();
        }

        //La aplicamos al personaje
        personaje.setExp(expPersonaje);

        //Desactivamos el sonido de la barra de sonido
        controladorCanvas.gestionarSonidoExpJugador(false);

    }

    //Comprueba si nuestro turno se ha acabado
    private bool FinDeTurno () {

        for (int i = 0 ; i < equipoJugador.Count ; i++) {

            if (equipoJugador[i].getMovido() == false) {
                return false;
            }

        }

        return true;

    }

    //Hará los preparativos para que empiece el turno del enemigo
    private void prepararTurnoEnemigo () {
        
        //Evitaremos que el jugador pueda mover al puntero hasta que finalize el turno del enemigo
        puntero.set_esPosibleMoverPuntero(false);

        //Ocultaremos al puntero
        puntero.GetComponent<SpriteRenderer>().enabled = false;

        //Variable que señalará que estamos en el turno del enemigo
        turnoEnemigo = true;

        //Indicaremos al jugador que ha empezado la fase del enemigo
        StartCoroutine(empezarTurnoEnemigo());

    }

    
    private IEnumerator empezarTurnoEnemigo () {

        //Mostrará el canvas que indica al jugador que la fase del enemigo ha llegado
        controladorCanvas.gestionarCanvas_faseDelEnemigo(true);

        //Parará la música principal
        controladorSonidos.parar_musica_de_fondo();

        //Sonido que marcará el comienzo del turno enemigo
        controladorSonidos.reproducirClip(ControladorSonidos.f_enemigo);

        //Activaremos la música del turno enemigo
        controladorSonidos.reproducir_musica_de_fondo_enemigo();

        //Esperaremos 3 segundos para que se pueda apreciar la animación del canvas
        yield return new WaitForSeconds(3.0f);

        //Desactivaremos el canvas
        controladorCanvas.gestionarCanvas_faseDelEnemigo(false);

        //Empezará el turno del enemigo
        turnoDelEnemigo();

    }

    //Moverá a todos los enemigos que tengan a alguna unidad del jugador al alcance
    private void turnoDelEnemigo () {

        //Obtenemos una lista desordenada del equipo enemigo para que no siempre se muevan en el mismo órden en el turno enemigo
        List<Personaje> enemigos = desordenarListaEnemigos();

        /*Haremos que los personajes del jugador se puedan volver a mover (aunque el jugador no podrá volver a moverlos hasta que acabe el turno).
        Esto se hace para que estos personajes vuelvan a su animación por defecto y no haya errores en las animaciones de los ataques.*/
        for (int i = 0; i < equipoJugador.Count; i++) {
            equipoJugador[i].setMovido(false);
            setAnimacionPersonaje(equipoJugador[i], Personaje.Movido, false);
        }

        //Hace que los enemigos se muevan para atacar al jugador
        StartCoroutine(atacarAlJugador(enemigos));

    }

    //Desordena la lista del equipo enemigo
    private List<Personaje> desordenarListaEnemigos () {

        //Lista donde guardaremos una clonación de la lista de enemigos
        /*Nota: Lo único que se clonará será la lista, pero las referencias a los personajes serán las mismas, así que aunque borremos personajes de esta lista, seguirán estando en
        la lista original*/
        List<Personaje> enemigos_ordenados = new List<Personaje>(equipoEnemigo);
        //Lista donde guardaremos las referencias de los personajes de forma desordenada
        List<Personaje> enemigos_desordenados = new List<Personaje>();

        int alea;

        //Desordenaremos la lista
        while (enemigos_ordenados.Count > 0) {

            //Generamos un index aleatorio entre 0 y el count de la lista
            alea = Random.Range(0, enemigos_ordenados.Count);

            //Guardamos el personaje con el index dado en la lista
            enemigos_desordenados.Add(enemigos_ordenados[alea]);

            //Y lo borramos de la lista para que no vuelva a aparecer
            enemigos_ordenados.RemoveAt(alea);

        }

        return enemigos_desordenados;
    }

    /*Dada una lista de enemigos, hace que estos busquen al jugador y, si está a su alcance, atacarle. En caso de que haya más de una unidad del jugador al alcance, atacará a la que
    menos defensa o resistencia tenga (dependiendo del tipo de ataque que tenga) y si no hay ninguna unidad al alcance, sólo se quedarán en su sitio hasta que el jugador se acerque.*/
    public IEnumerator atacarAlJugador (List<Personaje> enemigos) {

        List<Personaje> personajesDelJugadorAlAlcance;
        Personaje personaje;

        //Recorreremos toda la lista de personajes
        for (int i = 0; i < enemigos.Count; i++) {

            //Le diremos al puntero que vaya a la posición de este personaje
            puntero.moverPuntero(enemigos[i].getX(), enemigos[i].getY());

            //Le diremos al puntero que sea hijo de este personaje para que le siga durante su trayectoria
            puntero.transform.parent = enemigos[i].transform;

            //Iniliciaremos una lista que contendrá todos los personajes al alcance del enemigo
            personajesDelJugadorAlAlcance = new List<Personaje>();

            //Inicializamos la lista de celdas donde guardaremos las rutas de cada personaje
            listaCeldas = new List<GameObject>();

            personaje = null;

            //Obtenemos todas las rutas que podrá tomar este personaje
            obtenerRutas(enemigos[i]);
            obtenerRutas(enemigos[i]);

            //Obtenemos a todos los personajes al alcance de este enemigo y los guardamos en la lista creada anteriormente
            buscarUnidadesJugadores(listaCeldasMov, personajesDelJugadorAlAlcance);
            buscarUnidadesJugadores(listaCeldasAtaq, personajesDelJugadorAlAlcance);

            /*Si hay algún personaje al alcance de este enemigo, procederemos a seleccionar a cual atacaremos. Si no hay ningún personaje del jugador al alcance, no nos moveremos del sitio.*/
            if (personajesDelJugadorAlAlcance.Count > 0) {

                //Guardaremos al primer personaje de la lista como objetivo al que atacar
                personaje = personajesDelJugadorAlAlcance[0];

                //Y dependiendo del tipo de unidad que sea el enemigo, buscará a los que menos defensas tengan contra su tipo de ataque
                if (enemigos[i].GetType() == typeof(Mago)) {

                    for (int j = 1; j < personajesDelJugadorAlAlcance.Count; j++) {

                        //Si la resistencia de alguno de los personajes es menor que la del ya guardado, lo cambiaremos por el que menos resistencia tenga
                        if (personajesDelJugadorAlAlcance[j].getResistencia() < personaje.getResistencia()) {
                            personaje = personajesDelJugadorAlAlcance[j];
                        }
                        //Si resulta que hay 2 personajes con la misma resistencia, tiraremos un dado: si sale 0 nos quedamos con el que ya teniamos y si sale 1 lo cambiaremos
                        else if(personajesDelJugadorAlAlcance[j].getResistencia() == personaje.getResistencia()) {

                            int alea = Random.Range(0, 1);
                            if (alea == 1) {
                                personaje = personajesDelJugadorAlAlcance[j];
                            }

                        }

                    }

                }
                else {

                    for (int j = 1; j < personajesDelJugadorAlAlcance.Count; j++) {

                        //Si la defensa de alguno de los personajes es menor que la del ya guardado, lo cambiaremos por el que menos defensa tenga
                        if (personajesDelJugadorAlAlcance[j].getDefensa() < personaje.getDefensa()) {
                            personaje = personajesDelJugadorAlAlcance[j];
                        }
                        //Si resulta que hay 2 personajes con la misma defensa, tiraremos un dado: si sale 0 nos quedamos con el que ya teniamos y si sale 1 lo cambiaremos
                        else if (personajesDelJugadorAlAlcance[j].getDefensa() == personaje.getDefensa()) {

                            int alea = Random.Range(0, 1);
                            if (alea == 1) {
                                personaje = personajesDelJugadorAlAlcance[j];
                            }

                        }

                    }

                }

                //Una vez ya tenemos a un objetivo en el punto de mira, guardamos una referencia suya en una variable global
                objetivo = personaje;

                //Activamos la animación de cuando se va a mover para que el funcionamiento de las animaciones sea correcto
                setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, true);

                //Preparamos la ruta más corta hasta él
                prepararMovimientoDePersonaje(new Celda(personaje.getX(), personaje.getY()));

                //Nos movemos hasta él
                if (listaCeldasEnemigo != null) {
                    yield return StartCoroutine(moverPersonaje(listaCeldasEnemigo));
                }

                //Desactivamos la animación de cuando se va a mover para que el funcionamiento de las animaciones sea correcto
                setAnimacionPersonaje(persoMoviendose, Personaje.Moviendose, false);

                //Comprobaremos las acciones que este personaje podrá hacer (buscando también a todos los objetivos a su alcance)
                comprobarPosiblesAcciones();

                //Activaremos el canvas de previsiones
                controladorCanvas.gestionarCanvasPrevisiones(true);

                //Hacemos las previsiones las previsiones
                mostrarPrevisionesUnidadSeleccionada();
                mostrarPrevisionesObjetivo();

                //Antes de atacar esperaremos unos milisegundos para que el movimiento no sea tan brusco
                yield return new WaitForSeconds(1.0f);

                //Y atacamos a este objetivo
                yield return StartCoroutine(atacar());

                //Activaremos el canvas de previsiones
                controladorCanvas.gestionarCanvasPrevisiones(false);

                //Una vez nos hayamos movido, dejaremos la lista celdas en null ya que no la necesitaremos más y puede ocasionar errores
                listaCeldasEnemigo = null;

                //Y haremos los cambios pertinentes en la matriz
                if (!enemigos[i].getMuerto()) {
                    controladorCanvas.BotonEsperar();
                }

                if (acabarJuego) {
                    break;
                }

            }

        }

        if (!acabarJuego) {
            //Una vez se hayan movido todas las unidades, volverá a ser el turno del jugador
            StartCoroutine(empezarturnoDelJugador());
        }

    }

    //Busca a todos los personajes del jugador que estén en el rango de alcance de un enemigo
    public List<Personaje> buscarUnidadesJugadores (List<Celda> celdas, List<Personaje> personajesDelJugadorAlAlcance) {
        
        //Recorremos la lista de celdas a las que este enemigo podrá moverse
        for (int i = 0 ; i < celdas.Count ; i++) {

            //Si hay un personaje en alguna de estas celdas
            if (mapa[celdas[i].getX(), celdas[i].getY()] != null) {

                //Y este personaje es del jugador
                if (equipoJugador.Contains(mapa[celdas[i].getX(), celdas[i].getY()])) {

                    //Lo añadiremos a la lista de personajes del jugador al alcance
                    personajesDelJugadorAlAlcance.Add(mapa[celdas[i].getX(), celdas[i].getY()]);

                }

            }

        }

        //Devolveremos la lista de personajes
        return personajesDelJugadorAlAlcance;

    }

    //Una vez acabado el turno enemigo, mostraremos al jugador que vuelve a ser su fase y que puede volver a mover a todos sus personajes
    private IEnumerator empezarturnoDelJugador() {

        //Mostrará el canvas que indica al jugador que vuelve a ser su turno
        controladorCanvas.gestionarCanvas_faseDelJugador(true);

        //Parará la música principal
        controladorSonidos.parar_musica_de_fondo();

        //Sonido que marcará el comienzo del turno del jugador
        controladorSonidos.reproducirClip(ControladorSonidos.f_jugador);

        //Activaremos la música del turno del jugador
        controladorSonidos.reproducir_musica_de_fondo_jugador();

        //Esperaremos 3 segundos para que se pueda apreciar la animación del canvas
        yield return new WaitForSeconds(3.0f);

        //Desactivaremos el canvas
        controladorCanvas.gestionarCanvas_faseDelJugador(false);

        //Cambiaremos las animaciones de los enemigos a "DePie" para que no haya errores en las animaciones
        for (int i = 0; i < equipoEnemigo.Count; i++) {
            equipoEnemigo[i].setMovido(false);
            setAnimacionPersonaje(equipoEnemigo[i], Personaje.Movido, false);
        }

        //Le diremos al puntero que deje de ser hijo de nadie, ya que ahora será el usuario quien lo mueva
        puntero.transform.parent = null;

        //Le diremos al puntero que vaya a la posición del primer personaje en la lista del jugador
        puntero.moverPuntero(equipoJugador[0].getX(), equipoJugador[0].getY());

        //Variable que señalará que ya no estamos en el turno del enemigo
        turnoEnemigo = false;

        //Volveremos a mostrar el puntero
        puntero.GetComponent<SpriteRenderer>().enabled = true;

        //Podremos volver a mover el puntero
        puntero.set_esPosibleMoverPuntero(true);

    }

    // ------------------------------------------------------------------------------------------------

    // Use this for initialization
    void Start () {

        //Inicializamos todos los componentes y referencias que vayamos a utilizar
        controladorCanvas = (ControladorCanvas)this.GetComponent<ControladorCanvas>();
        controladorSonidos = (ControladorSonidos)this.GetComponent<ControladorSonidos>();

        //Inicializamos los equipos de jugadores
        equipoJugador = new List <Personaje>();
        equipoEnemigo = new List <Personaje>();

        //Creamos las matrices que referencian a los mapas
        gestorDeMapas.inicializarMatrices();

        //Obtenemos las matrices que referencian a los mapas
        caracteristicasCeldas = gestorDeMapas.getCaracteristicasCeldas();
        mapa = gestorDeMapas.getMapa();

        //Crear y posicionar los personajes en la matriz del mapa
        creadorPersonajes.asignarEstadisticas();
        creadorPersonajes.posicionarPersonajes();
    }

}
