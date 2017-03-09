using UnityEngine;
using System.Collections;

public class Mago : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Mago.Nombre");
        clase = PlayerPrefs.GetString("Mago.Clase");
        nivel = PlayerPrefs.GetInt("Mago.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Mago.Exp");
        pasos = PlayerPrefs.GetInt("Mago.Pasos");
        pv = PlayerPrefs.GetInt("Mago.PV");
        pv_maximos = PlayerPrefs.GetInt("Mago.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Mago.Tipo");
        alcance = PlayerPrefs.GetInt("Mago.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Mago.Fuerza");
        magia = PlayerPrefs.GetInt("Mago.Magia");
        habilidad = PlayerPrefs.GetInt("Mago.Habilidad");
        velocidad = PlayerPrefs.GetInt("Mago.Velocidad");
        suerte = PlayerPrefs.GetInt("Mago.Suerte");
        defensa = PlayerPrefs.GetInt("Mago.Defensa");
        resistencia = PlayerPrefs.GetInt("Mago.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Mago.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Mago.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Mago.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Mago.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Mago.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Mago.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Mago.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Mago.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
