using UnityEngine;
using System.Collections;

public abstract class Mapa : MonoBehaviour {

    // ------------------------------------------------------------------------------------------------
    //Estas matrices representarán al mapa, una contendrá las carácterísticas de cada celda y la otra a los personajes
    public int [,] caracteristicasCeldas;
    public Personaje [,] mapa;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Limite del mapa en las posiciones X e Y
    public int limiteMapaX;
    public int limiteMapaY;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    // Límite hasta el que se podrá mover la cámara
    public float limiteMovCamaraX1;
    public float limiteMovCamaraX2;
    public float limiteMovCamaraY1;
    public float limiteMovCamaraY2;
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Tipo de celdas en las matrices
    //Nota: readonly es como el "final" de java
    //Casilla estándar sin ningún tipo de ventajas ni desventajas
    public static int Llano = 0;
    //Permanecer en esta casilla te dará bonificación de defensa y probabilidad de esquivar, pero también tendrá un coste de pasos más alto
    public static int Bosque = 1;
    //Las puertas de los poblados otorgan una defensa y probabilidad de esquivar más altas que los bosques
    public static int Puerta = 2;
    //Los picos otorgan una defensa y probabilidad de esquivar igual de altas que las puertas, pero sólo podrán cruzarlos las unidades voladoras y las que van a pie
    public static int Pico = 3;
    //Solo las unidades voladoras o los bárbaros podrán cruzar o permanecer en estas casillas y además los bárbaros que permanezcan en estas casillas obtendrán una reducción de probabilidad de esquivar
    public static int Rio = 4;
    //Solo las unidades voladoras podrán cruzar o permanecer en estas casillas
    public static int Monte = 5;
    //No se podrá atravesar ni permanecer en ellas
    public static int Poblado = -1;

    //Bonificaciones que da cada tipo de terreno
    public static int[] terreno_bonificacionDefensa = new int[6] { 0, 1, 3, 2, 0, 0 };
    public static int[] terreno_bonificacionGolpe = new int[6] { 0, 10, 20, 20, -30, 0 };
    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Métodos

    // ------------------------------------------------------------------------------------------------
    //Dara la matriz de las carácterísticas de las celdas al controlador para que este las gestione
    public int [,] getCaracteristicasCeldas() {
        return caracteristicasCeldas;
    }

    //Dara la matriz de los personajes al controlador para que este las gestione
    public Personaje [,] getMapa() {
        return mapa;
    }

    //Método de inicialización que deberán implementar todos los mapas
    public abstract void inicializarMatrices();
    // ------------------------------------------------------------------------------------------------

}
