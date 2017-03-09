using UnityEngine;
using System.Collections;

public class Arquero : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Arquero.Nombre");
        clase = PlayerPrefs.GetString("Arquero.Clase");
        nivel = PlayerPrefs.GetInt("Arquero.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Arquero.Exp");
        pasos = PlayerPrefs.GetInt("Arquero.Pasos");
        pv = PlayerPrefs.GetInt("Arquero.PV");
        pv_maximos = PlayerPrefs.GetInt("Arquero.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Arquero.Tipo");
        alcance = PlayerPrefs.GetInt("Arquero.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Arquero.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Arquero.Fuerza");
        magia = PlayerPrefs.GetInt("Arquero.Magia");
        habilidad = PlayerPrefs.GetInt("Arquero.Habilidad");
        velocidad = PlayerPrefs.GetInt("Arquero.Velocidad");
        suerte = PlayerPrefs.GetInt("Arquero.Suerte");
        defensa = PlayerPrefs.GetInt("Arquero.Defensa");
        resistencia = PlayerPrefs.GetInt("Arquero.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Arquero.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Arquero.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Arquero.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Arquero.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Arquero.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Arquero.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Arquero.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Arquero.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
