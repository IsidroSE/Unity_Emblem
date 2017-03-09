using UnityEngine;
using System.Collections;

public class CreadorPersonajes : MonoBehaviour {

    // ------------------------------------------------------------------------------------------------
    //Controlador del juego (de esta referencia, tendremos acceso a las matrices que hacen referencia al mapa)
    public ControladorJuego controlador;
    //Mapa
    private Personaje[,] mapa;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Personajes
    //Jugador
    public Personaje priam;
    public Personaje cervantes;
    public Personaje lissa;
    public Personaje basilio;
    public Personaje cordelia;
    public Personaje miriel;
    public Personaje virion;
    public Personaje walhart;
    public Personaje lucina;
    //Enemigos
    public Personaje gangrel;
    public Personaje soldado1;
    public Personaje barbaro1;
    public Personaje arquero1;
    public Personaje infPesada1;
    public Personaje mago1;
    public Personaje jinete1;
    public Personaje jinetePegaso1;
    public Personaje jineteDragon1;
    public Personaje soldado2;
    public Personaje barbaro2;
    public Personaje arquero2;
    public Personaje infPesada2;
    public Personaje mago2;
    public Personaje jinete2;
    public Personaje jinetePegaso2;
    public Personaje jineteDragon2;
    // ------------------------------------------------------------------------------------------------

    //Asigna y guarda las estadísticas de todos los personajes para que luego estos accedan a ellas al crearse
    public void asignarEstadisticas () {

        //ALCANCE MÍNIMO POR TIPO DE UNIDAD
        PlayerPrefs.SetInt("Estandar.AlcanceMinimo", 1);
        PlayerPrefs.SetInt("Arquero.AlcanceMinimo", 2);

        //PERSONAJES
        //JUGADOR
        // ------------------------------------------------------------------------------------------------
        //Priam
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Priam.Nombre", "Priam");
        PlayerPrefs.SetString("Priam.Clase", "Mercenario");
        PlayerPrefs.SetInt("Priam.Nivel", 1);
        PlayerPrefs.SetInt("Priam.Exp", 0);
        PlayerPrefs.SetInt("Priam.Pasos", 5);
        PlayerPrefs.SetInt("Priam.PV", 20);
        PlayerPrefs.SetInt("Priam.PV_Maximos", 20);
        PlayerPrefs.SetString("Priam.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Priam.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Priam.Fuerza", 8);
        PlayerPrefs.SetInt("Priam.Magia", 0);
        PlayerPrefs.SetInt("Priam.Habilidad", 10);
        PlayerPrefs.SetInt("Priam.Velocidad", 8);
        PlayerPrefs.SetInt("Priam.Suerte", 3);
        PlayerPrefs.SetInt("Priam.Defensa", 7);
        PlayerPrefs.SetInt("Priam.Resistencia", 2);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Priam.c_PV", 45);
        PlayerPrefs.SetInt("Priam.c_Fuerza", 40);
        PlayerPrefs.SetInt("Priam.c_Magia", 10);
        PlayerPrefs.SetInt("Priam.c_Habilidad", 45);
        PlayerPrefs.SetInt("Priam.c_Velocidad", 35);
        PlayerPrefs.SetInt("Priam.c_Suerte", 35);
        PlayerPrefs.SetInt("Priam.c_Defensa", 40);
        PlayerPrefs.SetInt("Priam.c_Resistencia", 30);
        //End Priam

        //Cervantes
        //Estadísticas básicas del personaje
        //Nota: En este sólo pondremos el nombre porque el resto de estadísticas serán las mismas que las de la infantería pesada
        PlayerPrefs.SetString("Cervantes.Nombre", "Cervantes");
        //End Cervantes

        //Lissa
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Lissa.Nombre", "Lissa");
        PlayerPrefs.SetString("Lissa.Clase", "Clériga");
        PlayerPrefs.SetInt("Lissa.Nivel", 1);
        PlayerPrefs.SetInt("Lissa.Exp", 0);
        PlayerPrefs.SetInt("Lissa.Pasos", 5);
        PlayerPrefs.SetInt("Lissa.PV", 16);
        PlayerPrefs.SetInt("Lissa.PV_Maximos", 16);
        PlayerPrefs.SetString("Lissa.Tipo", Personaje.Sanador);
        PlayerPrefs.SetInt("Lissa.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Lissa.Fuerza", 0);
        PlayerPrefs.SetInt("Lissa.Magia", 7);
        PlayerPrefs.SetInt("Lissa.Habilidad", 6);
        PlayerPrefs.SetInt("Lissa.Velocidad", 7);
        PlayerPrefs.SetInt("Lissa.Suerte", 5);
        PlayerPrefs.SetInt("Lissa.Defensa", 2);
        PlayerPrefs.SetInt("Lissa.Resistencia", 7);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Lissa.c_PV", 35);
        PlayerPrefs.SetInt("Lissa.c_Fuerza", 25);
        PlayerPrefs.SetInt("Lissa.c_Magia", 35);
        PlayerPrefs.SetInt("Lissa.c_Habilidad", 30);
        PlayerPrefs.SetInt("Lissa.c_Velocidad", 35);
        PlayerPrefs.SetInt("Lissa.c_Suerte", 65);
        PlayerPrefs.SetInt("Lissa.c_Defensa", 15);
        PlayerPrefs.SetInt("Lissa.c_Resistencia", 35);
        //End Lissa

        //Basilio
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Basilio.Nombre", "Basilio");
        //End Basilio

        //Cordelia
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Cordelia.Nombre", "Cordelia");
        //End Cordelia

        //Miriel
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Miriel.Nombre", "Miriel");
        //End Miriel

        //Virion
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Virion.Nombre", "Virion");
        //End Virion

        //Walhart
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Walhart.Nombre", "Walhart");
        //End Walhart

        //Lucina
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Lucina.Nombre", "Lucina");
        PlayerPrefs.SetString("Lucina.Clase", "Lord");
        PlayerPrefs.SetInt("Lucina.Nivel", 1);
        PlayerPrefs.SetInt("Lucina.Exp", 0);
        PlayerPrefs.SetInt("Lucina.Pasos", 5);
        PlayerPrefs.SetInt("Lucina.PV", 20);
        PlayerPrefs.SetInt("Lucina.PV_Maximos", 20);
        PlayerPrefs.SetString("Lucina.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Lucina.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Lucina.Fuerza", 7);
        PlayerPrefs.SetInt("Lucina.Magia", 1);
        PlayerPrefs.SetInt("Lucina.Habilidad", 8);
        PlayerPrefs.SetInt("Lucina.Velocidad", 8);
        PlayerPrefs.SetInt("Lucina.Suerte", 5);
        PlayerPrefs.SetInt("Lucina.Defensa", 7);
        PlayerPrefs.SetInt("Lucina.Resistencia", 1);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Lucina.c_PV", 45);
        PlayerPrefs.SetInt("Lucina.c_Fuerza", 35);
        PlayerPrefs.SetInt("Lucina.c_Magia", 20);
        PlayerPrefs.SetInt("Lucina.c_Habilidad", 45);
        PlayerPrefs.SetInt("Lucina.c_Velocidad", 45);
        PlayerPrefs.SetInt("Lucina.c_Suerte", 80);
        PlayerPrefs.SetInt("Lucina.c_Defensa", 25);
        PlayerPrefs.SetInt("Lucina.c_Resistencia", 25);
        //End Lucina

        // ------------------------------------------------------------------------------------------------
        //Estadísticas
        //ENEMIGOS
        // ------------------------------------------------------------------------------------------------
        //Gangrel
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Gangrel.Nombre", "Gangrel");
        PlayerPrefs.SetString("Gangrel.Clase", "Rey");
        PlayerPrefs.SetInt("Gangrel.Nivel", 1);
        PlayerPrefs.SetInt("Gangrel.Exp", 0);
        PlayerPrefs.SetInt("Gangrel.Pasos", 0);
        PlayerPrefs.SetInt("Gangrel.PV", 20);
        PlayerPrefs.SetInt("Gangrel.PV_Maximos", 20);
        PlayerPrefs.SetString("Gangrel.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Gangrel.Alcance", 2);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Gangrel.Fuerza", 7);
        PlayerPrefs.SetInt("Gangrel.Magia", 3);
        PlayerPrefs.SetInt("Gangrel.Habilidad", 9);
        PlayerPrefs.SetInt("Gangrel.Velocidad", 9);
        PlayerPrefs.SetInt("Gangrel.Suerte", 7);
        PlayerPrefs.SetInt("Gangrel.Defensa", 5);
        PlayerPrefs.SetInt("Gangrel.Resistencia", 5);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Gangrel.c_PV", 40);
        PlayerPrefs.SetInt("Gangrel.c_Fuerza", 40);
        PlayerPrefs.SetInt("Gangrel.c_Magia", 30);
        PlayerPrefs.SetInt("Gangrel.c_Habilidad", 50);
        PlayerPrefs.SetInt("Gangrel.c_Velocidad", 50);
        PlayerPrefs.SetInt("Gangrel.c_Suerte", 30);
        PlayerPrefs.SetInt("Gangrel.c_Defensa", 30);
        PlayerPrefs.SetInt("Gangrel.c_Resistencia", 30);
        //End Gangrel

        //Soldado
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Soldado.Nombre", "Legionario");
        PlayerPrefs.SetString("Soldado.Clase", "Soldado");
        PlayerPrefs.SetInt("Soldado.Nivel", 1);
        PlayerPrefs.SetInt("Soldado.Exp", 0);
        PlayerPrefs.SetInt("Soldado.Pasos", 5);
        PlayerPrefs.SetInt("Soldado.PV", 18);
        PlayerPrefs.SetInt("Soldado.PV_Maximos", 18);
        PlayerPrefs.SetString("Soldado.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Soldado.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Soldado.Fuerza", 9);
        PlayerPrefs.SetInt("Soldado.Magia", 0);
        PlayerPrefs.SetInt("Soldado.Habilidad", 8);
        PlayerPrefs.SetInt("Soldado.Velocidad", 8);
        PlayerPrefs.SetInt("Soldado.Suerte", 3);
        PlayerPrefs.SetInt("Soldado.Defensa", 7);
        PlayerPrefs.SetInt("Soldado.Resistencia", 3);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Soldado.c_PV", 45);
        PlayerPrefs.SetInt("Soldado.c_Fuerza", 35);
        PlayerPrefs.SetInt("Soldado.c_Magia", 20);
        PlayerPrefs.SetInt("Soldado.c_Habilidad", 45);
        PlayerPrefs.SetInt("Soldado.c_Velocidad", 45);
        PlayerPrefs.SetInt("Soldado.c_Suerte", 80);
        PlayerPrefs.SetInt("Soldado.c_Defensa", 25);
        PlayerPrefs.SetInt("Soldado.c_Resistencia", 25);
        //End Soldado

        //Barbaro
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Barbaro.Nombre", "Legionario");
        PlayerPrefs.SetString("Barbaro.Clase", "Barbaro");
        PlayerPrefs.SetInt("Barbaro.Nivel", 1);
        PlayerPrefs.SetInt("Barbaro.Exp", 0);
        PlayerPrefs.SetInt("Barbaro.Pasos", 5);
        PlayerPrefs.SetInt("Barbaro.PV", 24);
        PlayerPrefs.SetInt("Barbaro.PV_Maximos", 24);
        PlayerPrefs.SetString("Barbaro.Tipo", Personaje.Barbaro);
        PlayerPrefs.SetInt("Barbaro.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Barbaro.Fuerza", 12);
        PlayerPrefs.SetInt("Barbaro.Magia", 0);
        PlayerPrefs.SetInt("Barbaro.Habilidad", 8);
        PlayerPrefs.SetInt("Barbaro.Velocidad", 9);
        PlayerPrefs.SetInt("Barbaro.Suerte", 0);
        PlayerPrefs.SetInt("Barbaro.Defensa", 5);
        PlayerPrefs.SetInt("Barbaro.Resistencia", 0);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Barbaro.c_PV", 60);
        PlayerPrefs.SetInt("Barbaro.c_Fuerza", 50);
        PlayerPrefs.SetInt("Barbaro.c_Magia", 10);
        PlayerPrefs.SetInt("Barbaro.c_Habilidad", 45);
        PlayerPrefs.SetInt("Barbaro.c_Velocidad", 35);
        PlayerPrefs.SetInt("Barbaro.c_Suerte", 45);
        PlayerPrefs.SetInt("Barbaro.c_Defensa", 40);
        PlayerPrefs.SetInt("Barbaro.c_Resistencia", 5);
        //End Barbaro

        //Arquero
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Arquero.Nombre", "Legionario");
        PlayerPrefs.SetString("Arquero.Clase", "Arquero");
        PlayerPrefs.SetInt("Arquero.Nivel", 1);
        PlayerPrefs.SetInt("Arquero.Exp", 0);
        PlayerPrefs.SetInt("Arquero.Pasos", 5);
        PlayerPrefs.SetInt("Arquero.PV", 19);
        PlayerPrefs.SetInt("Arquero.PV_Maximos", 19);
        PlayerPrefs.SetString("Arquero.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Arquero.Alcance", 2);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Arquero.Fuerza", 7);
        PlayerPrefs.SetInt("Arquero.Magia", 0);
        PlayerPrefs.SetInt("Arquero.Habilidad", 10);
        PlayerPrefs.SetInt("Arquero.Velocidad", 9);
        PlayerPrefs.SetInt("Arquero.Suerte", 3);
        PlayerPrefs.SetInt("Arquero.Defensa", 6);
        PlayerPrefs.SetInt("Arquero.Resistencia", 2);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Arquero.c_PV", 35);
        PlayerPrefs.SetInt("Arquero.c_Fuerza", 40);
        PlayerPrefs.SetInt("Arquero.c_Magia", 30);
        PlayerPrefs.SetInt("Arquero.c_Habilidad", 40);
        PlayerPrefs.SetInt("Arquero.c_Velocidad", 45);
        PlayerPrefs.SetInt("Arquero.c_Suerte", 40);
        PlayerPrefs.SetInt("Arquero.c_Defensa", 25);
        PlayerPrefs.SetInt("Arquero.c_Resistencia", 25);
        //End Arquero

        //InfPesada
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("InfPesada.Nombre", "Legionario");
        PlayerPrefs.SetString("InfPesada.Clase", "Infanteria Pesada");
        PlayerPrefs.SetInt("InfPesada.Nivel", 1);
        PlayerPrefs.SetInt("InfPesada.Exp", 0);
        PlayerPrefs.SetInt("InfPesada.Pasos", 4);
        PlayerPrefs.SetInt("InfPesada.PV", 22);
        PlayerPrefs.SetInt("InfPesada.PV_Maximos", 22);
        PlayerPrefs.SetString("InfPesada.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("InfPesada.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("InfPesada.Fuerza", 11);
        PlayerPrefs.SetInt("InfPesada.Magia", 0);
        PlayerPrefs.SetInt("InfPesada.Habilidad", 7);
        PlayerPrefs.SetInt("InfPesada.Velocidad", 3);
        PlayerPrefs.SetInt("InfPesada.Suerte", 4);
        PlayerPrefs.SetInt("InfPesada.Defensa", 12);
        PlayerPrefs.SetInt("InfPesada.Resistencia", 3);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("InfPesada.c_PV", 50);
        PlayerPrefs.SetInt("InfPesada.c_Fuerza", 40);
        PlayerPrefs.SetInt("InfPesada.c_Magia", 0);
        PlayerPrefs.SetInt("InfPesada.c_Habilidad", 50);
        PlayerPrefs.SetInt("InfPesada.c_Velocidad", 10);
        PlayerPrefs.SetInt("InfPesada.c_Suerte", 35);
        PlayerPrefs.SetInt("InfPesada.c_Defensa", 55);
        PlayerPrefs.SetInt("InfPesada.c_Resistencia", 45);
        //End InfPesada

        //Mago
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Mago.Nombre", "Legionario");
        PlayerPrefs.SetString("Mago.Clase", "Mago");
        PlayerPrefs.SetInt("Mago.Nivel", 1);
        PlayerPrefs.SetInt("Mago.Exp", 0);
        PlayerPrefs.SetInt("Mago.Pasos", 5);
        PlayerPrefs.SetInt("Mago.PV", 17);
        PlayerPrefs.SetInt("Mago.PV_Maximos", 17);
        PlayerPrefs.SetString("Mago.Tipo", Personaje.Estandar);
        PlayerPrefs.SetInt("Mago.Alcance", 2);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Mago.Fuerza", 0);
        PlayerPrefs.SetInt("Mago.Magia", 9);
        PlayerPrefs.SetInt("Mago.Habilidad", 4);
        PlayerPrefs.SetInt("Mago.Velocidad", 6);
        PlayerPrefs.SetInt("Mago.Suerte", 1);
        PlayerPrefs.SetInt("Mago.Defensa", 5);
        PlayerPrefs.SetInt("Mago.Resistencia", 8);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Mago.c_PV", 30);
        PlayerPrefs.SetInt("Mago.c_Fuerza", 5);
        PlayerPrefs.SetInt("Mago.c_Magia", 50);
        PlayerPrefs.SetInt("Mago.c_Habilidad", 35);
        PlayerPrefs.SetInt("Mago.c_Velocidad", 50);
        PlayerPrefs.SetInt("Mago.c_Suerte", 20);
        PlayerPrefs.SetInt("Mago.c_Defensa", 15);
        PlayerPrefs.SetInt("Mago.c_Resistencia", 30);
        //End Mago

        //Jinete
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("Jinete.Nombre", "Legionario");
        PlayerPrefs.SetString("Jinete.Clase", "Jinete");
        PlayerPrefs.SetInt("Jinete.Nivel", 1);
        PlayerPrefs.SetInt("Jinete.Exp", 0);
        PlayerPrefs.SetInt("Jinete.Pasos", 7);
        PlayerPrefs.SetInt("Jinete.PV", 19);
        PlayerPrefs.SetInt("Jinete.PV_Maximos", 19);
        PlayerPrefs.SetString("Jinete.Tipo", Personaje.Caballo);
        PlayerPrefs.SetInt("Jinete.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("Jinete.Fuerza", 8);
        PlayerPrefs.SetInt("Jinete.Magia", 1);
        PlayerPrefs.SetInt("Jinete.Habilidad", 7);
        PlayerPrefs.SetInt("Jinete.Velocidad", 7);
        PlayerPrefs.SetInt("Jinete.Suerte", 4);
        PlayerPrefs.SetInt("Jinete.Defensa", 7);
        PlayerPrefs.SetInt("Jinete.Resistencia", 6);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("Jinete.c_PV", 45);
        PlayerPrefs.SetInt("Jinete.c_Fuerza", 50);
        PlayerPrefs.SetInt("Jinete.c_Magia", 5);
        PlayerPrefs.SetInt("Jinete.c_Habilidad", 40);
        PlayerPrefs.SetInt("Jinete.c_Velocidad", 35);
        PlayerPrefs.SetInt("Jinete.c_Suerte", 60);
        PlayerPrefs.SetInt("Jinete.c_Defensa", 40);
        PlayerPrefs.SetInt("Jinete.c_Resistencia", 15);
        //End Jinete

        //Jinete de Pegaso
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("JinetePegaso.Nombre", "Legionario");
        PlayerPrefs.SetString("JinetePegaso.Clase", "Jinete de Pegaso");
        PlayerPrefs.SetInt("JinetePegaso.Nivel", 1);
        PlayerPrefs.SetInt("JinetePegaso.Exp", 0);
        PlayerPrefs.SetInt("JinetePegaso.Pasos", 7);
        PlayerPrefs.SetInt("JinetePegaso.PV", 18);
        PlayerPrefs.SetInt("JinetePegaso.PV_Maximos", 18);
        PlayerPrefs.SetString("JinetePegaso.Tipo", Personaje.Volador);
        PlayerPrefs.SetInt("JinetePegaso.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("JinetePegaso.Fuerza", 5);
        PlayerPrefs.SetInt("JinetePegaso.Magia", 4);
        PlayerPrefs.SetInt("JinetePegaso.Habilidad", 6);
        PlayerPrefs.SetInt("JinetePegaso.Velocidad", 10);
        PlayerPrefs.SetInt("JinetePegaso.Suerte", 5);
        PlayerPrefs.SetInt("JinetePegaso.Defensa", 5);
        PlayerPrefs.SetInt("JinetePegaso.Resistencia", 9);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("JinetePegaso.c_PV", 45);
        PlayerPrefs.SetInt("JinetePegaso.c_Fuerza", 45);
        PlayerPrefs.SetInt("JinetePegaso.c_Magia", 15);
        PlayerPrefs.SetInt("JinetePegaso.c_Habilidad", 40);
        PlayerPrefs.SetInt("JinetePegaso.c_Velocidad", 45);
        PlayerPrefs.SetInt("JinetePegaso.c_Suerte", 40);
        PlayerPrefs.SetInt("JinetePegaso.c_Defensa", 35);
        PlayerPrefs.SetInt("JinetePegaso.c_Resistencia", 40);
        //End Jinete de Pegaso

        //Jinete de Dragón
        //Estadísticas básicas del personaje
        PlayerPrefs.SetString("JineteDragon.Nombre", "Legionario");
        PlayerPrefs.SetString("JineteDragon.Clase", "Jinete de Dragón");
        PlayerPrefs.SetInt("JineteDragon.Nivel", 1);
        PlayerPrefs.SetInt("JineteDragon.Exp", 0);
        PlayerPrefs.SetInt("JineteDragon.Pasos", 7);
        PlayerPrefs.SetInt("JineteDragon.PV", 19);
        PlayerPrefs.SetInt("JineteDragon.PV_Maximos", 19);
        PlayerPrefs.SetString("JineteDragon.Tipo", Personaje.Volador);
        PlayerPrefs.SetInt("JineteDragon.Alcance", 1);

        //Estadísticas del personaje
        PlayerPrefs.SetInt("JineteDragon.Fuerza", 8);
        PlayerPrefs.SetInt("JineteDragon.Magia", 0);
        PlayerPrefs.SetInt("JineteDragon.Habilidad", 9);
        PlayerPrefs.SetInt("JineteDragon.Velocidad", 6);
        PlayerPrefs.SetInt("JineteDragon.Suerte", 3);
        PlayerPrefs.SetInt("JineteDragon.Defensa", 10);
        PlayerPrefs.SetInt("JineteDragon.Resistencia", 1);

        //Crecimientos de las estadísticas del personaje
        PlayerPrefs.SetInt("JineteDragon.c_PV", 55);
        PlayerPrefs.SetInt("JineteDragon.c_Fuerza", 40);
        PlayerPrefs.SetInt("JineteDragon.c_Magia", 20);
        PlayerPrefs.SetInt("JineteDragon.c_Habilidad", 40);
        PlayerPrefs.SetInt("JineteDragon.c_Velocidad", 35);
        PlayerPrefs.SetInt("JineteDragon.c_Suerte", 50);
        PlayerPrefs.SetInt("JineteDragon.c_Defensa", 45);
        PlayerPrefs.SetInt("JineteDragon.c_Resistencia", 10);
        //End Jinete de Dragón

        // ------------------------------------------------------------------------------------------------
    }

    //Posicionar personajes en la matriz
    public void posicionarPersonajes () {
        //Obtendremos el mapa del controlador
        mapa = controlador.getMapa();

        // ------------------------------------------------------------------------------------------------
        //Creamos a los personajes

        //Jugador
        //Priam
        priam.inicializarPersonaje();
        //Cervantes
        cervantes.inicializarPersonaje();
        //Lissa
        lissa.inicializarPersonaje();
        //Basilio
        basilio.inicializarPersonaje();
        //Cordelia
        cordelia.inicializarPersonaje();
        //Miriel
        miriel.inicializarPersonaje();
        //Virion
        virion.inicializarPersonaje();
        //Walhart
        walhart.inicializarPersonaje();
        //Lucina
        lucina.inicializarPersonaje();

        //Enemigos
        //Rey Gangrel
        gangrel.inicializarPersonaje();
        //Soldado 1
        soldado1.inicializarPersonaje();
        //Barbaro 1
        barbaro1.inicializarPersonaje();
        //Arquero 1
        arquero1.inicializarPersonaje();
        //Infanteria Pesada 1
        infPesada1.inicializarPersonaje();
        //Mago 1
        mago1.inicializarPersonaje();
        //Jinete 1
        jinete1.inicializarPersonaje();
        //Jinete de Pegaso 1
        jinetePegaso1.inicializarPersonaje();
        //Jinete de Dragón 1
        jineteDragon1.inicializarPersonaje();
        //Soldado 2
        soldado2.inicializarPersonaje();
        //Barbaro 2
        barbaro2.inicializarPersonaje();
        //Arquero 2
        arquero2.inicializarPersonaje();
        //Infanteria Pesada 2
        infPesada2.inicializarPersonaje();
        //Mago 2
        mago2.inicializarPersonaje();
        //Jinete 2
        jinete2.inicializarPersonaje();
        //Jinete de Pegaso 2
        jinetePegaso2.inicializarPersonaje();
        //Jinete de Dragón 2
        jineteDragon2.inicializarPersonaje();
        // ------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------
        //Los insertamos en la matriz, en sus posiciones correspondientes

        //Jugador
        //Priam
        mapa[priam.getX(), priam.getY()] = priam;
        ControladorJuego.insertarJugador(priam);
        //Cervantes
        mapa[cervantes.getX(), cervantes.getY()] = cervantes;
        ControladorJuego.insertarJugador(cervantes);
        //Lissa
        mapa[lissa.getX(), lissa.getY()] = lissa;
        ControladorJuego.insertarJugador(lissa);
        //Basilio
        mapa[basilio.getX(), basilio.getY()] = basilio;
        ControladorJuego.insertarJugador(basilio);
        //Cordelia
        mapa[cordelia.getX(), cordelia.getY()] = cordelia;
        ControladorJuego.insertarJugador(cordelia);
        //Miriel
        mapa[miriel.getX(), miriel.getY()] = miriel;
        ControladorJuego.insertarJugador(miriel);
        //Virion
        mapa[virion.getX(), virion.getY()] = virion;
        ControladorJuego.insertarJugador(virion);
        //Walhart
        mapa[walhart.getX(), walhart.getY()] = walhart;
        ControladorJuego.insertarJugador(walhart);
        //Lucina
        mapa[lucina.getX(), lucina.getY()] = lucina;
        ControladorJuego.insertarJugador(lucina);

        //Enemigos
        //Rey Gangrel
        mapa[gangrel.getX(), gangrel.getY()] = gangrel;
        ControladorJuego.insertarEnemigo(gangrel);
        //Soldado 1
        mapa[soldado1.getX(), soldado1.getY()] = soldado1;
        ControladorJuego.insertarEnemigo(soldado1);
        //Barbaro 1
        mapa[barbaro1.getX(), barbaro1.getY()] = barbaro1;
        ControladorJuego.insertarEnemigo(barbaro1);
        //Arquero 1
        mapa[arquero1.getX(), arquero1.getY()] = arquero1;
        ControladorJuego.insertarEnemigo(arquero1);
        //Infanteria Pesada 1
        mapa[infPesada1.getX(), infPesada1.getY()] = infPesada1;
        ControladorJuego.insertarEnemigo(infPesada1);
        //Mago 1
        mapa[mago1.getX(), mago1.getY()] = mago1;
        ControladorJuego.insertarEnemigo(mago1);
        //Jinete 1
        mapa[jinete1.getX(), jinete1.getY()] = jinete1;
        ControladorJuego.insertarEnemigo(jinete1);
        //Jinete de Pegaso 1
        mapa[jinetePegaso1.getX(), jinetePegaso1.getY()] = jinetePegaso1;
        ControladorJuego.insertarEnemigo(jinetePegaso1);
        //Jinete de Dragón 1
        mapa[jineteDragon1.getX(), jineteDragon1.getY()] = jineteDragon1;
        ControladorJuego.insertarEnemigo(jineteDragon1);
        //Soldado 2
        mapa[soldado2.getX(), soldado2.getY()] = soldado2;
        ControladorJuego.insertarEnemigo(soldado2);
        //Barbaro 2
        mapa[barbaro2.getX(), barbaro2.getY()] = barbaro2;
        ControladorJuego.insertarEnemigo(barbaro2);
        //Arquero 2
        mapa[arquero2.getX(), arquero2.getY()] = arquero2;
        ControladorJuego.insertarEnemigo(arquero2);
        //Infanteria Pesada 2
        mapa[infPesada2.getX(), infPesada2.getY()] = infPesada2;
        ControladorJuego.insertarEnemigo(infPesada2);
        //Mago 2
        mapa[mago2.getX(), mago2.getY()] = mago2;
        ControladorJuego.insertarEnemigo(mago2);
        //Jinete 2
        mapa[jinete2.getX(), jinete2.getY()] = jinete2;
        ControladorJuego.insertarEnemigo(jinete2);
        //Jinete de Pegaso 2
        mapa[jinetePegaso2.getX(), jinetePegaso2.getY()] = jinetePegaso2;
        ControladorJuego.insertarEnemigo(jinetePegaso2);
        //Jinete de Dragón 2
        mapa[jineteDragon2.getX(), jineteDragon2.getY()] = jineteDragon2;
        ControladorJuego.insertarEnemigo(jineteDragon2);
        // ------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------
        //Subimos a unos cuantos enemigos de nivel
        /*Nota: de normal, cuando subimos a un personaje de nivel, si le aumentan los puntos de vida, son sólo los máximos, no los actuales. Asi que en este caso como estamos inicilizando
        a los personajes, también pondremos sus puntos de vida al máximo.*/
        //Soldado 2
        subirNivelPersonaje(soldado2, 1);
        soldado2.setPV(soldado2.getPV_Maximos());
        //Jinete de Pegaso 2
        subirNivelPersonaje(jinetePegaso2, 1);
        jinetePegaso2.setPV(jinetePegaso2.getPV_Maximos());
        //Infanteria pesada 2
        subirNivelPersonaje(infPesada2, 2);
        infPesada2.setPV(infPesada2.getPV_Maximos());
        //Arquero 2
        subirNivelPersonaje(arquero2, 3);
        arquero2.setPV(arquero2.getPV_Maximos());
        //Jinete 1
        subirNivelPersonaje(jinete1, 1);
        jinete1.setPV(jinete1.getPV_Maximos());
        //Jinete 2
        subirNivelPersonaje(jinete2, 2);
        jinete2.setPV(jinete2.getPV_Maximos());
        //Mago 2
        subirNivelPersonaje(mago2, 2);
        mago2.setPV(mago2.getPV_Maximos());
        //Barbaro 2
        subirNivelPersonaje(barbaro2, 1);
        barbaro2.setPV(barbaro2.getPV_Maximos());
        //Jinete de Dragón 1
        subirNivelPersonaje(jineteDragon1, 1);
        jineteDragon1.setPV(jineteDragon1.getPV_Maximos());
        //Jinete de Dragón 2
        subirNivelPersonaje(jineteDragon2, 3);
        jineteDragon2.setPV(jineteDragon2.getPV_Maximos());
        // ------------------------------------------------------------------------------------------------

        //Una vez insertadas todas las unidades, devolveremos la matriz al controlador para que pueda empezar el juego
        controlador.setMapa(mapa);
    }

    public void subirNivelPersonaje (Personaje personaje, int niveles) {

        for (int i = 0 ; i < niveles ; i++) {
            personaje.subirDeNivel();
        }

    }

}
