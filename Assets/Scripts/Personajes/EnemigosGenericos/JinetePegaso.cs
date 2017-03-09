using UnityEngine;
using System.Collections;

public class JinetePegaso : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("JinetePegaso.Nombre");
        clase = PlayerPrefs.GetString("JinetePegaso.Clase");
        nivel = PlayerPrefs.GetInt("JinetePegaso.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("JinetePegaso.Exp");
        pasos = PlayerPrefs.GetInt("JinetePegaso.Pasos");
        pv = PlayerPrefs.GetInt("JinetePegaso.PV");
        pv_maximos = PlayerPrefs.GetInt("JinetePegaso.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("JinetePegaso.Tipo");
        alcance = PlayerPrefs.GetInt("JinetePegaso.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("JinetePegaso.Fuerza");
        magia = PlayerPrefs.GetInt("JinetePegaso.Magia");
        habilidad = PlayerPrefs.GetInt("JinetePegaso.Habilidad");
        velocidad = PlayerPrefs.GetInt("JinetePegaso.Velocidad");
        suerte = PlayerPrefs.GetInt("JinetePegaso.Suerte");
        defensa = PlayerPrefs.GetInt("JinetePegaso.Defensa");
        resistencia = PlayerPrefs.GetInt("JinetePegaso.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("JinetePegaso.c_PV");
        c_fuerza = PlayerPrefs.GetInt("JinetePegaso.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("JinetePegaso.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("JinetePegaso.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("JinetePegaso.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("JinetePegaso.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("JinetePegaso.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("JinetePegaso.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
