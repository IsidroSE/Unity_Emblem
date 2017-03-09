using UnityEngine;
using System.Collections;

public class Priam : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Priam.Nombre");
        clase = PlayerPrefs.GetString("Priam.Clase");
        nivel = PlayerPrefs.GetInt("Priam.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Priam.Exp");
        pasos = PlayerPrefs.GetInt("Priam.Pasos");
        pv = PlayerPrefs.GetInt("Priam.PV");
        pv_maximos = PlayerPrefs.GetInt("Priam.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Priam.Tipo");
        alcance = PlayerPrefs.GetInt("Priam.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Priam.Fuerza");
        magia = PlayerPrefs.GetInt("Priam.Magia");
        habilidad = PlayerPrefs.GetInt("Priam.Habilidad");
        velocidad = PlayerPrefs.GetInt("Priam.Velocidad");
        suerte = PlayerPrefs.GetInt("Priam.Suerte");
        defensa = PlayerPrefs.GetInt("Priam.Defensa");
        resistencia = PlayerPrefs.GetInt("Priam.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Priam.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Priam.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Priam.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Priam.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Priam.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Priam.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Priam.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Priam.c_Resistencia");

    }

}
