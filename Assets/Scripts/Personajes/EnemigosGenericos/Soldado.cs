using UnityEngine;
using System.Collections;

public class Soldado : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Soldado.Nombre");
        clase = PlayerPrefs.GetString("Soldado.Clase");
        nivel = PlayerPrefs.GetInt("Soldado.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Soldado.Exp");
        pasos = PlayerPrefs.GetInt("Soldado.Pasos");
        pv = PlayerPrefs.GetInt("Soldado.PV");
        pv_maximos = PlayerPrefs.GetInt("Soldado.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Soldado.Tipo");
        alcance = PlayerPrefs.GetInt("Soldado.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Soldado.Fuerza");
        magia = PlayerPrefs.GetInt("Soldado.Magia");
        habilidad = PlayerPrefs.GetInt("Soldado.Habilidad");
        velocidad = PlayerPrefs.GetInt("Soldado.Velocidad");
        suerte = PlayerPrefs.GetInt("Soldado.Suerte");
        defensa = PlayerPrefs.GetInt("Soldado.Defensa");
        resistencia = PlayerPrefs.GetInt("Soldado.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Soldado.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Soldado.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Soldado.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Soldado.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Soldado.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Soldado.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Soldado.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Soldado.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
