using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorCanvas : MonoBehaviour {

    //Referencia al controlador
    private ControladorJuego controlador;

    //----------------------------------------------------
    //Canvas que mostrará las estadisticas de un personaje por pantalla
    public Canvas estadisticas;
    //Estadísticas básicas
    public Text nombreUnidad;
    public Text clase;
    public RawImage retrato;
    public Text pv_maximos;
    public Text pv;
    public Text nivel;
    public Text exp;
    public Text pasos;
    //Estadísticas
    public Text fuerza;
    public Text magia;
    public Text habilidad;
    public Text velocidad;
    public Text suerte;
    public Text defensa;
    public Text resistencia;
    //----------------------------------------------------

    //----------------------------------------------------
    //Canvas que mostrará las posibles acciones que podrá efectuar el personaje que se vaya a mover
    public Canvas canvasMovimiento;
    //Botón de acción
    public Button accion;
    //----------------------------------------------------

    //----------------------------------------------------
    //Canvas donde nos dará las opciones de atacar/curar o cancelar
    public Canvas canvasAtaqueCuracion;
    //----------------------------------------------------

    //----------------------------------------------------
    //Canvas que mostrará las previsiones para un posible ataque o curación sobre la unidad que selecciones
    public Canvas canvasPrevisiones;
    //Jugador
    //Retrato del jugador
    public RawImage retratoJugador;
    public Slider expJugador;
    public Text vidaUnidadSeleccionada;
    public Text danyo_curacionUnidadSeleccionada;
    public Text ataqueDobleUnidadSeleccionada;
    public Text probGolpeUnidadSeleccionada;
    //Enemigo
    //Retrato del objetivo
    public RawImage retratoObjetivo;
    public Slider expObjetivo;
    public Text vidaObjetivo;
    public Text danyoObjetivo;
    public Text ataqueDobleObjetivo;
    public Text probGolpeObjetivo;
    //----------------------------------------------------

    //----------------------------------------------------
    //Paneles que indicaran al jugador las fases
    public Canvas canvas_faseDelEnemigo;
    public Canvas canvas_faseDelJugador;
    //----------------------------------------------------

    //----------------------------------------------------
    //Canvas que mostrarán al jugador que ha ganado o ha perdido
    public Canvas victoria;
    public Canvas derrota;
    //----------------------------------------------------

    //Inicializaremos una variable que referenciará al controlador, para no tener que buscarlo cada vez que queramos acceder a él
    void Start() {
        controlador = this.GetComponent<ControladorJuego>();
    }

    //Activa o desactiva el canvas de estadísticas
    public void gestionarCanvasEstadisticas (bool activar) {
        estadisticas.enabled = activar;
    }

    //Devuelve true si el canvas de estadísticas es está activado y false si no lo está
    public bool isEstadisticasActive () {
        return estadisticas.enabled;
    }

    //Activa o desactiva el canvas de movimiento
    public void gestionarCanvasMov (bool activar) {
        canvasMovimiento.enabled = activar;
    }

    //Activa o desactiva el canvas de previsiones
    public void gestionarCanvasPrevisiones(bool activar) {
        canvasPrevisiones.enabled = activar;
    }

    //Activa o desactiva el canvas de atacar/curar
    public void gestionarCanvasAtaqueCuracion(bool activar) {
        canvasAtaqueCuracion.enabled = activar;
    }

    //Gestiona el canvas que indicará que el turno del enemigo ha empezado
    public void gestionarCanvas_faseDelEnemigo (bool activar) {
        canvas_faseDelEnemigo.gameObject.SetActive(activar);
    }

    //Gestiona el canvas que indicará que vuelve a serel turno del jugador
    public void gestionarCanvas_faseDelJugador(bool activar) {
        canvas_faseDelJugador.gameObject.SetActive(activar);
    }

    //Gestiona el canvas que indicará que has ganado
    public void gestionarCanvas_victoria(bool activar) {
        victoria.gameObject.SetActive(activar);
    }

    //Gestiona el canvas que indicará que has perdido
    public void gestionarCanvas_derrota(bool activar) {
        derrota.gameObject.SetActive(activar);
    }

    //onClick() del botón cancelar del canvas de movimiento
    /*Destruye todas las celdas utilizadas para mostrar las posibles rutas que podrá tomar un personaje.
    También ocultará la IA de las acciones que puede tomar un personaje al moverse*/
    public void BotonCancelarMov() {

        //Destruiremos las celdas que se muestran en la escena
        controlador.destruirCeldas();

        //Destruiremos las listas que contienen todas las rutas que el personaje puede tomar
        controlador.destruirListasCeldas();

        //Le diremos al controlador que le diga al puntero que el personaje ya no está moviendose
        controlador.personajeEstaMoviendose(false);

        //Si el canvas que muestra las estadísticas del personaje que vas a mover esta activado, se desactivará
        if (estadisticas.enabled == true) {
            estadisticas.enabled = false;
        }

        //Le dice al controlador que ya puede crear más rutas en el caso de que el usuario se lo pida
        controlador.setPersonajeMov(false);

        //Cambia la animación del personaje de Moviendose a DePie
        controlador.setAnimacionPersonaje(null, Personaje.Moviendose, false);

        //Restauraremos la posición del personaje
        controlador.restaurarPosPersonaje();

        //Desactivaremos el botón de acción
        setBotonAccion(false);

    }

    //onClick() del botón esperar del canvas de movimiento
    //Haremos que el personaje espere en la casilla actual hasta el siguiente turno
    public void BotonEsperar() {

        //Le diremos al controlador que mueva al personaje seleccionado de su posición en la matriz a su posición actual en la escena
        controlador.esperar();

        //Desactivaremos el botón de acción
        setBotonAccion(false);

    }

    //onClick() del botón accion del canvas de movimiento
    //Entra en el modo ataque/curar dependiendo del personaje seleccionado
    public void BotonAccion() {
        controlador.entrarEnModoAccion();
    }

    //onClick() del botón cancelar del canvas de atacar/curar
    //Cancela la acción que se estaba llevando a cabo y vuelve a darte opción a mover el personaje a otro sitio
    public void BotonCancelarAccion() {

        //desactivamos el canvas de curación
        gestionarCanvasAtaqueCuracion(false);

        //Desactivaremos la UI de previsiones
        if (canvasPrevisiones.enabled) gestionarCanvasPrevisiones(false);

        //Desactivaremos (si están activados) los textos que indican si una unidad puede ejecutar un ataque doble
        if (ataqueDobleUnidadSeleccionada.gameObject.activeSelf == true) AtaqueDobleUnidadSeleccionada(false);
        if (ataqueDobleObjetivo.gameObject.activeSelf == true) AtaqueDobleObjetivo(false);

        //Volveremos a poder mover a un personaje
        controlador.volverAlModoMoverse();
    }

    //onClick() del botón confirmar del canvas de atacar/curar
    //Confirma al controlador que el usuario quiere atacar o curar al objetivo seleccionado con la unidad seleccionada
    public void BotonConfirmar() {
        controlador.accion();
    }

    //Actualiza la información del personaje seleccionado en el canvas de estadísticas
    public void mostrarEstadisticas (Personaje personaje) {
        nombreUnidad.text = personaje.getNombre();
        clase.text = personaje.getClase();
        retrato.texture = personaje.getRetrato();

        pv_maximos.text = personaje.getPV_Maximos().ToString();
        pv.text = personaje.getPV().ToString();
        nivel.text = personaje.getNivel().ToString();
        exp.text = personaje.getExp().ToString();
        pasos.text = personaje.getPasos().ToString();

        fuerza.text = personaje.getFuerza().ToString();
        magia.text = personaje.getMagia().ToString();
        habilidad.text = personaje.getHabilidad().ToString();
        velocidad.text = personaje.getVelocidad().ToString();
        suerte.text = personaje.getSuerte().ToString();
        defensa.text = personaje.getDefensa().ToString();
        resistencia.text = personaje.getResistencia().ToString();
    }

    //Activa o desactiva el botón de accion
    public void setBotonAccion(bool activar) {
        accion.gameObject.SetActive(activar);
    }

    //Cambia el texto del botón de acción del canvas de movimiento
    public void cambiarTextoBotonAccion(string texto) {
        accion.GetComponentInChildren<Text>().text = texto;
    }

    //Muestra las previsiones de la unidad que se va a mover en el canvas de previsiones, parte izquierda
    public void mostrarPrevisionesUnidadMoviendose(Personaje personaje, string vida, string danyoCausado, string probGolpe, int exp) {
        retratoJugador.texture = personaje.getRetrato();
        vidaUnidadSeleccionada.text = vida;
        danyo_curacionUnidadSeleccionada.text = danyoCausado;
        probGolpeUnidadSeleccionada.text = probGolpe;
        expJugador.value = exp;
    }

    //Modifica el atributo vida del panel de previsiones del enemigo
    public void modificarVidaUnidadSeleccionada(string vida) {
        vidaUnidadSeleccionada.text = vida;
    }

    //Muestra las previsiones de la unidad sobre la que va a interactuar la unidad que el jugador seleccione en el canvas de previsiones, parte izquierda
    public void mostrarPrevisionesUnidadObjetivo(Personaje personaje, string vida, string danyoCausado, string probGolpe, int exp) {
        retratoObjetivo.texture = personaje.getRetrato();
        vidaObjetivo.text = vida;
        danyoObjetivo.text = danyoCausado;
        probGolpeObjetivo.text = probGolpe;
        expObjetivo.value = exp;
    }

    //Modifica el atributo vida del panel de previsiones del enemigo
    public void modificarVidaObjetivo (string vida) {
        vidaObjetivo.text = vida;
    }

    //Activa o desactiva el texto del ataque doble de nuestro personaje
    public void AtaqueDobleUnidadSeleccionada (bool activar) {
        ataqueDobleUnidadSeleccionada.gameObject.SetActive(activar);
    }

    //Activa o desactiva el texto del ataque doble del objetivo
    public void AtaqueDobleObjetivo (bool activar) {
        ataqueDobleObjetivo.gameObject.SetActive(activar);
    }

    //Suma 1 punto al valor del slider del jugador
    public int sumarExpJugador () {

        //Si la experiencia llega a 100, haremos que la experiencia vuelva a ser 0 y se lo notificaremos al controlador para que suba de nivel al personaje
        if (expJugador.value + 1 > 99) {
            expJugador.value = 0;
            return -1;
        }
        else {
            expJugador.value++;
            return (int)expJugador.value;
        }

    }

    //Suma 1 punto al valordel slider del enemigo
    public int sumarExpEnemigo () {

        //Si la experiencia llega a 100, haremos que la experiencia vuelva a ser 0 y se lo notificaremos al controlador para que suba de nivel al personaje
        if (expObjetivo.value + 1 > 99) {
            expObjetivo.value = 0;
            return -1;
        }
        else {
            expObjetivo.value++;
            return (int)expObjetivo.value;
        }

    }

    //Activa o desactiva el sonido que suena al subir experiencia
    public void gestionarSonidoExpJugador (bool activar) {

        if (activar) {
            expJugador.GetComponent<AudioSource>().Play();
        }
        else {
            expJugador.GetComponent<AudioSource>().Stop();
        }
        
    }

    //Obtiene la esperiencia del jugador
    public int getExpJugador () {
        return (int)expJugador.value;
    }

    //obtiene la experiencia del enemigo
    public int getExpObjetivo () {
        return (int)expObjetivo.value;
    }

}
