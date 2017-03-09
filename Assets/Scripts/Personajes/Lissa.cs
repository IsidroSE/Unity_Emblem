using UnityEngine;
using System.Collections;

public class Lissa : Personaje {

    public override void inicializarPersonaje() {
        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Lissa.Nombre");
        clase = PlayerPrefs.GetString("Lissa.Clase");
        nivel = PlayerPrefs.GetInt("Lissa.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Lissa.Exp");
        pasos = PlayerPrefs.GetInt("Lissa.Pasos");
        pv = PlayerPrefs.GetInt("Lissa.PV");
        pv_maximos = PlayerPrefs.GetInt("Lissa.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Lissa.Tipo");
        alcance = PlayerPrefs.GetInt("Lissa.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Lissa.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Lissa.Fuerza");
        magia = PlayerPrefs.GetInt("Lissa.Magia");
        habilidad = PlayerPrefs.GetInt("Lissa.Habilidad");
        velocidad = PlayerPrefs.GetInt("Lissa.Velocidad");
        suerte = PlayerPrefs.GetInt("Lissa.Suerte");
        defensa = PlayerPrefs.GetInt("Lissa.Defensa");
        resistencia = PlayerPrefs.GetInt("Lissa.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Lissa.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Lissa.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Lissa.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Lissa.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Lissa.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Lissa.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Lissa.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Lissa.c_Resistencia");
    }

}
