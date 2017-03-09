using UnityEngine;
using System.Collections;

public class InfPesada : Personaje {

    public override void inicializarPersonaje() {

        //Posición del personaje en el mapa
        x = (int)transform.position.x;
        y = (int)transform.position.y;

        //Estadísticas básicas del personaje
        nombre = PlayerPrefs.GetString("InfPesada.Nombre");
        clase = PlayerPrefs.GetString("InfPesada.Clase");
        nivel = PlayerPrefs.GetInt("InfPesada.Nivel");
        puntos_de_experiencia = PlayerPrefs.GetInt("InfPesada.Exp");
        pasos = PlayerPrefs.GetInt("InfPesada.Pasos");
        pv = PlayerPrefs.GetInt("InfPesada.PV");
        pv_maximos = PlayerPrefs.GetInt("InfPesada.PV_Maximos");
        tipoUnidad = PlayerPrefs.GetString("InfPesada.Tipo");
        alcance = PlayerPrefs.GetInt("InfPesada.Alcance");
        alcanceMinimo = PlayerPrefs.GetInt("Estandar.AlcanceMinimo");

        //Estadísticas del personaje
        fuerza = PlayerPrefs.GetInt("InfPesada.Fuerza");
        magia = PlayerPrefs.GetInt("InfPesada.Magia");
        habilidad = PlayerPrefs.GetInt("InfPesada.Habilidad");
        velocidad = PlayerPrefs.GetInt("InfPesada.Velocidad");
        suerte = PlayerPrefs.GetInt("InfPesada.Suerte");
        defensa = PlayerPrefs.GetInt("InfPesada.Defensa");
        resistencia = PlayerPrefs.GetInt("InfPesada.Resistencia");

        //Crecimientos de las estadísticas del personaje
        c_pv = PlayerPrefs.GetInt("InfPesada.c_PV");
        c_fuerza = PlayerPrefs.GetInt("InfPesada.c_Fuerza");
        c_magia = PlayerPrefs.GetInt("InfPesada.c_Magia");
        c_habilidad = PlayerPrefs.GetInt("InfPesada.c_Habilidad");
        c_velocidad = PlayerPrefs.GetInt("InfPesada.c_Velocidad");
        c_suerte = PlayerPrefs.GetInt("InfPesada.c_Suerte");
        c_defensa = PlayerPrefs.GetInt("InfPesada.c_Defensa");
        c_resistencia = PlayerPrefs.GetInt("InfPesada.c_Resistencia");

        /*Una vez inicializado, el personaje se insertará en la lista correspondiente dependiendo de si es una unidad del jugador
        o de la IA*/
        //ControladorJuego.insertarEnemigo(this);
    }

}
