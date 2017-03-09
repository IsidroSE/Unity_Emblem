using UnityEngine;
using System.Collections;

public class Jinete : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Jinete.Nombre");
        clase = PlayerPrefs.GetString("Jinete.Clase");
        nivel = PlayerPrefs.GetInt("Jinete.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Jinete.Exp");
        pasos = PlayerPrefs.GetInt("Jinete.Pasos");
        pv = PlayerPrefs.GetInt("Jinete.PV");
        pv_maximos = PlayerPrefs.GetInt("Jinete.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Jinete.Tipo");
        alcance = PlayerPrefs.GetInt("Jinete.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Jinete.Fuerza");
        magia = PlayerPrefs.GetInt("Jinete.Magia");
        habilidad = PlayerPrefs.GetInt("Jinete.Habilidad");
        velocidad = PlayerPrefs.GetInt("Jinete.Velocidad");
        suerte = PlayerPrefs.GetInt("Jinete.Suerte");
        defensa = PlayerPrefs.GetInt("Jinete.Defensa");
        resistencia = PlayerPrefs.GetInt("Jinete.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Jinete.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Jinete.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Jinete.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Jinete.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Jinete.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Jinete.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Jinete.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Jinete.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
