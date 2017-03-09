using UnityEngine;
using System.Collections;

public class Lucina : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Lucina.Nombre");
        clase = PlayerPrefs.GetString("Lucina.Clase");
        nivel = PlayerPrefs.GetInt("Lucina.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Lucina.Exp");
        pasos = PlayerPrefs.GetInt("Lucina.Pasos");
        pv = PlayerPrefs.GetInt("Lucina.PV");
        pv_maximos = PlayerPrefs.GetInt("Lucina.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Lucina.Tipo");
        alcance = PlayerPrefs.GetInt("Lucina.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Lucina.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Lucina.Fuerza");
        magia = PlayerPrefs.GetInt("Lucina.Magia");
        habilidad = PlayerPrefs.GetInt("Lucina.Habilidad");
        velocidad = PlayerPrefs.GetInt("Lucina.Velocidad");
        suerte = PlayerPrefs.GetInt("Lucina.Suerte");
        defensa = PlayerPrefs.GetInt("Lucina.Defensa");
        resistencia = PlayerPrefs.GetInt("Lucina.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Lucina.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Lucina.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Lucina.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Lucina.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Lucina.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Lucina.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Lucina.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Lucina.c_Resistencia");

    }

}
