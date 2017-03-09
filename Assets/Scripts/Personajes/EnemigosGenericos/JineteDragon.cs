using UnityEngine;
using System.Collections;

public class JineteDragon : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("JineteDragon.Nombre");
        clase = PlayerPrefs.GetString("JineteDragon.Clase");
        nivel = PlayerPrefs.GetInt("JineteDragon.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("JineteDragon.Exp");
        pasos = PlayerPrefs.GetInt("JineteDragon.Pasos");
        pv = PlayerPrefs.GetInt("JineteDragon.PV");
        pv_maximos = PlayerPrefs.GetInt("JineteDragon.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("JineteDragon.Tipo");
        alcance = PlayerPrefs.GetInt("JineteDragon.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("JineteDragon.Fuerza");
        magia = PlayerPrefs.GetInt("JineteDragon.Magia");
        habilidad = PlayerPrefs.GetInt("JineteDragon.Habilidad");
        velocidad = PlayerPrefs.GetInt("JineteDragon.Velocidad");
        suerte = PlayerPrefs.GetInt("JineteDragon.Suerte");
        defensa = PlayerPrefs.GetInt("JineteDragon.Defensa");
        resistencia = PlayerPrefs.GetInt("JineteDragon.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("JineteDragon.c_PV");
        c_fuerza = PlayerPrefs.GetInt("JineteDragon.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("JineteDragon.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("JineteDragon.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("JineteDragon.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("JineteDragon.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("JineteDragon.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("JineteDragon.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
