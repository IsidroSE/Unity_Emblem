using UnityEngine;
using System.Collections;

public class Gangrel : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Gangrel.Nombre");
        clase = PlayerPrefs.GetString("Gangrel.Clase");
        nivel = PlayerPrefs.GetInt("Gangrel.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Gangrel.Exp");
        pasos = PlayerPrefs.GetInt("Gangrel.Pasos");
        pv = PlayerPrefs.GetInt("Gangrel.PV");
        pv_maximos = PlayerPrefs.GetInt("Gangrel.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Gangrel.Tipo");
        alcance = PlayerPrefs.GetInt("Gangrel.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Gangrel.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Gangrel.Fuerza");
        magia = PlayerPrefs.GetInt("Gangrel.Magia");
        habilidad = PlayerPrefs.GetInt("Gangrel.Habilidad");
        velocidad = PlayerPrefs.GetInt("Gangrel.Velocidad");
        suerte = PlayerPrefs.GetInt("Gangrel.Suerte");
        defensa = PlayerPrefs.GetInt("Gangrel.Defensa");
        resistencia = PlayerPrefs.GetInt("Gangrel.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Gangrel.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Gangrel.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Gangrel.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Gangrel.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Gangrel.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Gangrel.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Gangrel.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Gangrel.c_Resistencia");

    }

}
