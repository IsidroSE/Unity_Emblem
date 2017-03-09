using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/*Puntero que representará la posición del jugador en el mapa para poder tener una referencia al movernos por el mapa o seleccionar
una unidad*/
public class Puntero : MonoBehaviour {

    //La distancia que se moverá en la escena el sprite
    private float distancia = 1f;
    //Estas variables controlarán el tiempo que pasa desde que el puntero se mueve hasta que se vuelve a mover (teclado)
    private float contador;
    public float tiempoHastaProximaPulsacion;
    //Mapa del cual sacaremos variables como los limites del mapa para que el puntero no pueda salirse de este
    public Mapa mapa;
    //Posición del ratón o del dedo cuando toques la pantalla en un móvil
    private Vector3 mousePosition;
    //"Rayo" que se moverá desde la posición del ratón hasta la cámara
    Ray ray;
    //Posición del ratón al hacer click en la cámara
    RaycastHit posDelClick;
    //Controlador del juego
    public ControladorJuego controlador;
    //Variable que controlará el movimiento del puntero si este está moviendo a un personaje
    private bool personajeMov = false;
    //Variable que evitará que se llame al método de mover al personaje más de 1 vez seguida
    private bool personajeEnMovimiento = false;
    //Variable que evitará que el jugador pueda mover el puntero en ciertas circumstancias, cuando el controlador lo ordene en casos como cuando se ejecuta una acción o en el turno del enemigo
    private bool esPosibleMoverPuntero = true;

    // Update is called once per frame
    void Update() {

        if (esPosibleMoverPuntero) {
            //Mover el puntero mediante el teclado
            contador += Time.deltaTime;
            if (!controlador.getAccionEnCurso()) {
                if (contador >= tiempoHastaProximaPulsacion) {
                    moverPunteroConTeclado();
                }
            }

            //Mover el puntero mediante click del ratón o touch de las pantallas táctiles
            moverPunteroConRaton();
        }

        if (Input.GetKey("escape"))
            controlador.salir_del_juego();

    }

    private void moverPunteroConTeclado () {

        //Movimiento Horizontal (X)
        /*Este método puede devolver 3 valores: -1, 0 y 1:
            -1 lo devolverá cuando se pulse la flecha izquierda del teclado
            1 lo devolverá cuando se pulse la flecha derecha del teclado
            0 lo devolverá cuando no se pulse ninguno de los dos
        */
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        if (movimientoHorizontal != 0) {

            if (movimientoHorizontal > 0) {
                //Antes de mover el puntero hacia la derecha, comprobaremos que no se va a mover fuera del mapa
                if (transform.position.x + distancia <= mapa.limiteMapaX) {
                    moverse(distancia, 0);
                }
            }
            else {
                if (transform.position.x - distancia >= 0) {
                    moverse(-distancia, 0);
                }
            }
        }

        //Movimiento Vertical (Y)
        /*Este método puede devolver 3 valores: -1, 0 y 1:
            -1 lo devolverá cuando se pulse la flecha abajo del teclado
            1 lo devolverá cuando se pulse la flecha arriba del teclado
            0 lo devolverá cuando no se pulse ninguno de los dos
        */
        float movimientoVertical = Input.GetAxis("Vertical");

        if (movimientoVertical != 0) {
            if (movimientoVertical > 0) {
                if (transform.position.y + distancia <= mapa.limiteMapaY) {
                    moverse(0, distancia);
                }
            }
            else {
                if (transform.position.y - distancia >= 0) {
                    moverse(0, -distancia);
                }
            }
        }

            //Reestablecemos el contador a 0 para que no se pueda volver a mover hasta pasados X milesimas de segundos
            contador = 0f;
    }

    //Mueve el puntero usando el ratón
    private void moverPunteroConRaton () {

        //Si haces click con el ratón en la pantalla, el puntero se moverá a la posición que hayas indicado
        //Sólo hará las siguientes órdenes cuando se pulse el botón izquierdo del ratón o la pantalla táctil en el móvil
        if (Input.GetMouseButtonDown(0)) {

            //Esta comprobación evitará que, cuando hagas click encima de la UI (interfaz) de acción del personaje se mueva el puntero
            if (!EventSystem.current.IsPointerOverGameObject(-1)) {

                //Generamos el rayo que ira desde la cámara hasta la posición del ratón
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                /*Comprobaremos si ha chocado con algo (en este caso deberá chocar contra el único elemento que hay en la escena que 
                contenga un collider, el mapa en este caso) y si lo ha hecho, guardaremos las credenciales de la dirección en "posDelClick"
                Nota: el 100 define la distancia que recorrerá el rayo hasta desaparecer si no ha colisionado con nada, pero como el mapa
                ocupa toda la pantalla, nunca se dará este caso, asi que igual da que pongamos 100 que 200*/
                if (Physics.Raycast(ray, out posDelClick, 100)) {

                    /*Ahora guardaremos el vector3 (posición) del click en esta variable, que utilizaremos para sacar los valores x e y
                    (la z no hará falta al ser un juego 2D*/
                    mousePosition = posDelClick.point;

                    /*Luego restaremos la posición obtenida a la posición actual del vector para obtener sólo la diferencia (ya que
                    vamos a utilizar el translate y a este no hace falta pasarle la posición absoluta)*/
                    int x = (int)Mathf.Round(mousePosition.x);
                    int y = (int)Mathf.Round(mousePosition.y);

                    //Comprobaremos si la posición seleccionada es la misma que la actual
                    if (x == transform.position.x && y == transform.position.y) {

                        //Si es la misma, el puntero no se "moverá" porque estariamos moviéndonos a la misma posición en la que ya estamos
                        //Comprobaremos si no hay ya un personaje moviéndose
                        if (!personajeMov) {

                            /*En el caso de que no haya ya un personaje moviéndose, comprobaremos si en esta casilla en la que ya estabamos hay un personaje, si es así, entonces
                            procederemos a crear las rutas por las que podremos mover a este personaje (y por lo tanto ahora si que habrá un personaje moviéndose, por lo que
                            no podremos volver a entrar por aquí hasta que el usuario cancele el movimiento del personaje desde la UI*/
                            Personaje personaje = controlador.getPosMapa((int)transform.position.x, (int)transform.position.y);
                            if (personaje != null && ControladorJuego.esDelJugador(personaje)) {

                                if (!personaje.getMovido()) {
                                    crearRutas(personaje);
                                }

                            }
                        }
                        else {
                            /*En el caso de que ya haya un personaje moviéndose, quiere decir que el usuario quiere mover al personaje seleccionado a esta casilla, por lo tanto
                            simularemos el movimiento del personaje a esta casilla, el usuario verá como el personaje toma la ruta más corta a la celda seleccionada, pero este
                            no se moverá de la matriz hasta que el usuario pulse los comandos de esperar o atacar/curar, de esta forma, el usuario podrá rectificar en el caso 
                            de que ya no quiera mover el personaje a esta posición.*/
                            if (!controlador.getAccionEnCurso()) {

                                Personaje personaje = controlador.getPersoMoviendose();

                                /*Para evitar que el usuario pueda hacer esto 2 veces seguidas y el sistema de movimiento enloquezca, sólo haremos esta acción 1 vez al mismo tiempo
                                y hasta que esta no acabe no podrá volverlo a mover.*/
                                if (!personajeEnMovimiento) {

                                    //También comprobaremos que la posición a la que queremos mover el personaje no es la misma en la que ya esta.
                                    if (x != personaje.getX() || y != personaje.getY()) {

                                        set_personajeEnMovimiento(true);
                                        Celda celda = new Celda(x, y);
                                        controlador.prepararMovimientoDePersonaje(celda);

                                    }

                                }

                            }

                        }

                    }
                    else {

                        //Por último, nos moveremos
                        /*Luego restaremos la posición obtenida a la posición actual del vector para obtener sólo la diferencia (ya que
                        vamos a utilizar el translate y a este no hace falta pasarle la posición absoluta)*/
                        moverse(Mathf.Round(x - transform.position.x), Mathf.Round(y - transform.position.y));

                    }
                }
            }
        }
    }

    //Dadas una posición x e y, hará que el puntero se mueva por la escena de 1 unidad en 1
    private void moverse(float x, float y) {

        if (!personajeMov) {

            //Nos moveremos
            transform.Translate(new Vector3(x, y, 0));

            //Nota: otra forma de moverse poniendo la posición absoluta
            //transform.position = new Vector3(x, y, transform.position.z);

            //Y luego cargaremos las estadísticas de la unidad seleccionada por pantalla (si la hay)
            CargarDatos();

        }
        else {

            /* En el caso de que haya un personaje moviéndose, el puntero sólo podrá moverse por las celdas que esten al alcance del personaje y nunca fuera de estas*/
            float xAbsoluta = transform.position.x;
            float yAbsoluta = transform.position.y;

            //Sumaremos la x e y absolutas a sus contrapartes relativas para obtener la posición absoluta de la celda
            Celda celda = new Celda((int)(xAbsoluta + x), (int)(yAbsoluta + y));

            //Comprobaremos si esta celda está dentro de la lista
            if (controlador.dentroLaLista(celda)) {

                //Nos moveremos a esta celda
                transform.Translate(new Vector3(x, y, 0));

            }

            //Si hay una acción en curso
            if (controlador.getAccionEnCurso()) {

                //Obtendremos la posición del objetivo seleccionado en la matriz del mapa
                Personaje objetivo = controlador.getPosMapa((int)(xAbsoluta + x), (int)(yAbsoluta + y));

                //Pasaremos al objetivo al controlador para que guarde su referencia
                controlador.set_Objetivo(objetivo);

                //Y mostraremos las previsiones del objetivo y de la unidad seleccionada
                controlador.mostrarPrevisionesObjetivo();
                controlador.mostrarPrevisionesUnidadSeleccionada();
            }
        }
        
    }

    //Función que utilizará el controlador para mover al puntero a una casilla instantaniamente en ciertas ocasiones
    public void moverPuntero (int x, int y) {
        transform.position = new Vector3(x, y, transform.position.z);
    }

    //Obtener estadisticas de la unidad (si la hay) de la casilla actual
    private void CargarDatos () {

        if (!personajeMov || !isActiveCanvasEstadisticas()) {

            //Obtenemos la posición del personaje de la celda donde se ha posicionado el puntero (si lo hay)
            Personaje personaje = controlador.getPosMapa((int)transform.position.x, (int)transform.position.y);

            //Si hay un personaje, actualizará el panel con sus estadísticas y luego lo hará visible
            if (personaje != null) {
                controlador.mostrarInformacionPersonaje(personaje);
                controlador.gestorCanvasEstadisticas(true);
            }
            //En caso contrario no hará nada
            else {
                //Si el panel esta activado, lo desactivará
                if (isActiveCanvasEstadisticas()) {
                    controlador.gestorCanvasEstadisticas(false);
                }
            }
        }
    }

    //Se usará para saber si el canvas de estadísticas esta activo o no
    public bool isActiveCanvasEstadisticas () {
        return controlador.isCanvasEstadisticasActive();
    }

    //Haremos la llamada al controlador para que este cree las rutas
    private void crearRutas (Personaje personaje) {
        controlador.crearRutas(personaje);
    }

    //Avisará al puntero de que vas a mover a un personaje
    public void personajeMoviendose (bool mov) {
        personajeMov = mov;
    }

    //Evitará que se llame al método de mover al personaje más de 1 vez seguida
    public void set_personajeEnMovimiento(bool mov) {
        personajeEnMovimiento = mov;
    }

    //Clase que usará el controlador para denegar o no el movimiento del puntero
    public void set_esPosibleMoverPuntero (bool activar) {
        esPosibleMoverPuntero = activar;
    }

}
