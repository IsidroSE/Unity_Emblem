using UnityEngine;
using System.Collections;

public class Barbaro : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("Barbaro.Nombre");
        clase = PlayerPrefs.GetString("Barbaro.Clase");
        nivel = PlayerPrefs.GetInt("Barbaro.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("Barbaro.Exp");
        pasos = PlayerPrefs.GetInt("Barbaro.Pasos");
        pv = PlayerPrefs.GetInt("Barbaro.PV");
        pv_maximos = PlayerPrefs.GetInt("Barbaro.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("Barbaro.Tipo");
        alcance = PlayerPrefs.GetInt("Barbaro.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("Barbaro.Fuerza");
        magia = PlayerPrefs.GetInt("Barbaro.Magia");
        habilidad = PlayerPrefs.GetInt("Barbaro.Habilidad");
        velocidad = PlayerPrefs.GetInt("Barbaro.Velocidad");
        suerte = PlayerPrefs.GetInt("Barbaro.Suerte");
        defensa = PlayerPrefs.GetInt("Barbaro.Defensa");
        resistencia = PlayerPrefs.GetInt("Barbaro.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("Barbaro.c_PV");
        c_fuerza = PlayerPrefs.GetInt("Barbaro.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("Barbaro.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("Barbaro.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("Barbaro.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("Barbaro.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("Barbaro.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("Barbaro.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
