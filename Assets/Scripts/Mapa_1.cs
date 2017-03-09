using UnityEngine;
using System.Collections;

public class Mapa_1 : Mapa{

    //Iniciará las matrices
    public override void inicializarMatrices() {

        caracteristicasCeldas = new int[15, 15];
        mapa = new Personaje[15, 15];

        // ------------------------------------------------------------------------------------------------
        //Inicializando la matriz de las características de las celdas
        /*Cada vez que una unidad vaya a moverse, tendrá que mirar en esta matriz las caracteristicas de las celdas adyecentes a la unidad
        para saber el número de casillas que se podrá mover*/
        // Fila 14
        caracteristicasCeldas[0, 14] = Bosque; caracteristicasCeldas[1, 14] = Llano; caracteristicasCeldas[2, 14] = Llano;
        caracteristicasCeldas[3, 14] = Bosque; caracteristicasCeldas[4, 14] = Llano; caracteristicasCeldas[5, 14] = Bosque;
        caracteristicasCeldas[6, 14] = Llano; caracteristicasCeldas[7, 14] = Poblado; caracteristicasCeldas[8, 14] = Poblado;
        caracteristicasCeldas[9, 14] = Poblado; caracteristicasCeldas[10, 14] = Monte; caracteristicasCeldas[11, 14] = Monte;
        caracteristicasCeldas[12, 14] = Llano; caracteristicasCeldas[13, 14] = Bosque; caracteristicasCeldas[14, 14] = Llano;
        // Fila 13
        caracteristicasCeldas[0, 13] = Bosque; caracteristicasCeldas[1, 13] = Bosque; caracteristicasCeldas[2, 13] = Llano;
        caracteristicasCeldas[3, 13] = Llano; caracteristicasCeldas[4, 13] = Llano; caracteristicasCeldas[5, 13] = Llano;
        caracteristicasCeldas[6, 13] = Bosque; caracteristicasCeldas[7, 13] = Poblado; caracteristicasCeldas[8, 13] = Poblado;
        caracteristicasCeldas[9, 13] = Poblado; caracteristicasCeldas[10, 13] = Monte; caracteristicasCeldas[11, 13] = Monte;
        caracteristicasCeldas[12, 13] = Bosque; caracteristicasCeldas[13, 13] = Llano; caracteristicasCeldas[14, 13] = Llano;
        // Fila 12
        caracteristicasCeldas[0, 12] = Llano; caracteristicasCeldas[1, 12] = Llano; caracteristicasCeldas[2, 12] = Llano;
        caracteristicasCeldas[3, 12] = Llano; caracteristicasCeldas[4, 12] = Llano; caracteristicasCeldas[5, 12] = Bosque;
        caracteristicasCeldas[6, 12] = Llano; caracteristicasCeldas[7, 12] = Poblado; caracteristicasCeldas[8, 12] = Puerta;
        caracteristicasCeldas[9, 12] = Poblado; caracteristicasCeldas[10, 12] = Monte; caracteristicasCeldas[11, 12] = Llano;
        caracteristicasCeldas[12, 12] = Monte; caracteristicasCeldas[13, 12] = Llano; caracteristicasCeldas[14, 12] = Bosque;
        // Fila 11
        caracteristicasCeldas[0, 11] = Llano; caracteristicasCeldas[1, 11] = Bosque; caracteristicasCeldas[2, 11] = Llano;
        caracteristicasCeldas[3, 11] = Llano; caracteristicasCeldas[4, 11] = Llano; caracteristicasCeldas[5, 11] = Llano;
        caracteristicasCeldas[6, 11] = Bosque; caracteristicasCeldas[7, 11] = Llano; caracteristicasCeldas[8, 11] = Llano;
        caracteristicasCeldas[9, 11] = Llano; caracteristicasCeldas[10, 11] = Llano; caracteristicasCeldas[11, 11] = Monte;
        caracteristicasCeldas[12, 11] = Bosque; caracteristicasCeldas[13, 11] = Monte; caracteristicasCeldas[14, 11] = Monte;
        // Fila 10
        caracteristicasCeldas[0, 10] = Pico; caracteristicasCeldas[1, 10] = Llano; caracteristicasCeldas[2, 10] = Llano;
        caracteristicasCeldas[3, 10] = Bosque; caracteristicasCeldas[4, 10] = Llano; caracteristicasCeldas[5, 10] = Llano;
        caracteristicasCeldas[6, 10] = Llano; caracteristicasCeldas[7, 10] = Bosque; caracteristicasCeldas[8, 10] = Llano;
        caracteristicasCeldas[9, 10] = Llano; caracteristicasCeldas[10, 10] = Bosque; caracteristicasCeldas[11, 10] = Llano;
        caracteristicasCeldas[12, 10] = Monte; caracteristicasCeldas[13, 10] = Monte; caracteristicasCeldas[14, 10] = Monte;
        // Fila 9
        caracteristicasCeldas[0, 9] = Pico; caracteristicasCeldas[1, 9] = Pico; caracteristicasCeldas[2, 9] = Llano;
        caracteristicasCeldas[3, 9] = Bosque; caracteristicasCeldas[4, 9] = Bosque; caracteristicasCeldas[5, 9] = Llano;
        caracteristicasCeldas[6, 9] = Llano; caracteristicasCeldas[7, 9] = Llano; caracteristicasCeldas[8, 9] = Llano;
        caracteristicasCeldas[9, 9] = Llano; caracteristicasCeldas[10, 9] = Llano; caracteristicasCeldas[11, 9] = Llano;
        caracteristicasCeldas[12, 9] = Llano; caracteristicasCeldas[13, 9] = Bosque; caracteristicasCeldas[14, 9] = Llano;
        // Fila 8
        caracteristicasCeldas[0, 8] = Pico; caracteristicasCeldas[1, 8] = Pico; caracteristicasCeldas[2, 8] = Pico;
        caracteristicasCeldas[3, 8] = Llano; caracteristicasCeldas[4, 8] = Llano; caracteristicasCeldas[5, 8] = Llano;
        caracteristicasCeldas[6, 8] = Llano; caracteristicasCeldas[7, 8] = Llano; caracteristicasCeldas[8, 8] = Llano;
        caracteristicasCeldas[9, 8] = Llano; caracteristicasCeldas[10, 8] = Bosque; caracteristicasCeldas[11, 8] = Llano;
        caracteristicasCeldas[12, 8] = Llano; caracteristicasCeldas[13, 8] = Llano; caracteristicasCeldas[14, 8] = Llano;
        // Fila 7
        caracteristicasCeldas[0, 7] = Pico; caracteristicasCeldas[1, 7] = Pico; caracteristicasCeldas[2, 7] = Bosque;
        caracteristicasCeldas[3, 7] = Llano; caracteristicasCeldas[4, 7] = Bosque; caracteristicasCeldas[5, 7] = Llano;
        caracteristicasCeldas[6, 7] = Llano; caracteristicasCeldas[7, 7] = Bosque; caracteristicasCeldas[8, 7] = Rio;
        caracteristicasCeldas[9, 7] = Llano; caracteristicasCeldas[10, 7] = Rio; caracteristicasCeldas[11, 7] = Llano;
        caracteristicasCeldas[12, 7] = Llano; caracteristicasCeldas[13, 7] = Llano; caracteristicasCeldas[14, 7] = Llano;
        // Fila 6
        caracteristicasCeldas[0, 6] = Rio; caracteristicasCeldas[1, 6] = Rio; caracteristicasCeldas[2, 6] = Rio;
        caracteristicasCeldas[3, 6] = Rio; caracteristicasCeldas[4, 6] = Bosque; caracteristicasCeldas[5, 6] = Llano;
        caracteristicasCeldas[6, 6] = Rio; caracteristicasCeldas[7, 6] = Rio; caracteristicasCeldas[8, 6] = Rio;
        caracteristicasCeldas[9, 6] = Llano; caracteristicasCeldas[10, 6] = Rio; caracteristicasCeldas[11, 6] = Rio;
        caracteristicasCeldas[12, 6] = Bosque; caracteristicasCeldas[13, 6] = Llano; caracteristicasCeldas[14, 6] = Llano;
        // Fila 5
        caracteristicasCeldas[0, 5] = Poblado; caracteristicasCeldas[1, 5] = Poblado; caracteristicasCeldas[2, 5] = Poblado;
        caracteristicasCeldas[3, 5] = Rio; caracteristicasCeldas[4, 5] = Rio; caracteristicasCeldas[5, 5] = Rio;
        caracteristicasCeldas[6, 5] = Rio; caracteristicasCeldas[7, 5] = Bosque; caracteristicasCeldas[8, 5] = Llano;
        caracteristicasCeldas[9, 5] = Llano; caracteristicasCeldas[10, 5] = Llano; caracteristicasCeldas[11, 5] = Rio;
        caracteristicasCeldas[12, 5] = Rio; caracteristicasCeldas[13, 5] = Bosque; caracteristicasCeldas[14, 5] = Llano;
        // Fila 4
        caracteristicasCeldas[0, 4] = Poblado; caracteristicasCeldas[1, 4] = Poblado; caracteristicasCeldas[2, 4] = Poblado;
        caracteristicasCeldas[3, 4] = Bosque; caracteristicasCeldas[4, 4] = Llano; caracteristicasCeldas[5, 4] = Rio;
        caracteristicasCeldas[6, 4] = Rio; caracteristicasCeldas[7, 4] = Llano; caracteristicasCeldas[8, 4] = Llano;
        caracteristicasCeldas[9, 4] = Bosque; caracteristicasCeldas[10, 4] = Llano; caracteristicasCeldas[11, 4] = Llano;
        caracteristicasCeldas[12, 4] = Rio; caracteristicasCeldas[13, 4] = Rio; caracteristicasCeldas[14, 4] = Rio;
        // Fila 3
        caracteristicasCeldas[0, 3] = Poblado; caracteristicasCeldas[1, 3] = Puerta; caracteristicasCeldas[2, 3] = Poblado;
        caracteristicasCeldas[3, 3] = Llano; caracteristicasCeldas[4, 3] = Rio; caracteristicasCeldas[5, 3] = Rio;
        caracteristicasCeldas[6, 3] = Llano; caracteristicasCeldas[7, 3] = Bosque; caracteristicasCeldas[8, 3] = Llano;
        caracteristicasCeldas[9, 3] = Llano; caracteristicasCeldas[10, 3] = Bosque; caracteristicasCeldas[11, 3] = Bosque;
        caracteristicasCeldas[12, 3] = Llano; caracteristicasCeldas[13, 3] = Llano; caracteristicasCeldas[14, 3] = Bosque;
        // Fila 2
        caracteristicasCeldas[0, 2] = Llano; caracteristicasCeldas[1, 2] = Llano; caracteristicasCeldas[2, 2] = Llano;
        caracteristicasCeldas[3, 2] = Llano; caracteristicasCeldas[4, 2] = Llano; caracteristicasCeldas[5, 2] = Llano;
        caracteristicasCeldas[6, 2] = Llano; caracteristicasCeldas[7, 2] = Llano; caracteristicasCeldas[8, 2] = Bosque;
        caracteristicasCeldas[9, 2] = Bosque; caracteristicasCeldas[10, 2] = Llano; caracteristicasCeldas[11, 2] = Llano;
        caracteristicasCeldas[12, 2] = Llano; caracteristicasCeldas[13, 2] = Bosque; caracteristicasCeldas[14, 2] = Llano;
        // Fila 1
        caracteristicasCeldas[0, 1] = Bosque; caracteristicasCeldas[1, 1] = Llano; caracteristicasCeldas[2, 1] = Llano;
        caracteristicasCeldas[3, 1] = Rio; caracteristicasCeldas[4, 1] = Rio; caracteristicasCeldas[5, 1] = Llano;
        caracteristicasCeldas[6, 1] = Llano; caracteristicasCeldas[7, 1] = Llano; caracteristicasCeldas[8, 1] = Llano;
        caracteristicasCeldas[9, 1] = Llano; caracteristicasCeldas[10, 1] = Bosque; caracteristicasCeldas[11, 1] = Bosque;
        caracteristicasCeldas[12, 1] = Llano; caracteristicasCeldas[13, 1] = Bosque; caracteristicasCeldas[14, 1] = Llano;
        // Fila 0
        caracteristicasCeldas[0, 0] = Llano; caracteristicasCeldas[1, 0] = Llano; caracteristicasCeldas[2, 0] = Rio;
        caracteristicasCeldas[3, 0] = Rio; caracteristicasCeldas[4, 0] = Llano; caracteristicasCeldas[5, 0] = Bosque;
        caracteristicasCeldas[6, 0] = Llano; caracteristicasCeldas[7, 0] = Llano; caracteristicasCeldas[8, 0] = Llano;
        caracteristicasCeldas[9, 0] = Bosque; caracteristicasCeldas[10, 0] = Llano; caracteristicasCeldas[11, 0] = Llano;
        caracteristicasCeldas[12, 0] = Bosque; caracteristicasCeldas[13, 0] = Llano; caracteristicasCeldas[14, 0] = Bosque;
        // ------------------------------------------------------------------------------------------------
    }

}
