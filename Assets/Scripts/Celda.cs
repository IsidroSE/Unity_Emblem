using UnityEngine;
using System.Collections;

public class Celda {

    private int x;
    private int y;
    //Se utilizará en los algoritmos de búsqueda para encontrar las rutas a esta celda
    private Celda padre;
    //Número de pasos que tendrá que usar la unidad si quiere llegar a esta celda
    private int pasos;
    /*Variable que sirve para hacer saber al controlador si la casilla está ocupada por una unidad aliada (si la unidad que la ocupa es enemiga, directamente no se creará porque
    la unidad que irá a moverse no podrá atravesar casillas donde haya un enemigo.*/
    private bool ocupada;

    //-----------------------------------------------------------
    //CONSTRUCTORES
    public Celda(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public Celda(int x, int y, int pasos) {
        this.x = x;
        this.y = y;
        this.pasos = pasos;
        padre = null;
    }

    public Celda(int x, int y, int pasos, Celda padre) {
        this.x = x;
        this.y = y;
        this.pasos = pasos;
        this.padre = padre;
    }
    //-----------------------------------------------------------

    //-----------------------------------------------------------
    //GETTERS Y SETTERS
    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public int getPasos() {
        return pasos;
    }

    public Celda getPadre() {
        return padre;
    }

    public void setPadre(Celda padre) {
        this.padre = padre;
    }

    public bool getOcupada () {
        return this.ocupada;
    }

    public void setOcupada (bool ocupada) {
        this.ocupada = ocupada;
    }
    //-----------------------------------------------------------

    public override bool Equals(System.Object celda) {

        if (celda == null) {
            return false;
        }

        if (celda is Celda) {
            return (((Celda)celda).getX() == this.x) && (((Celda)celda).getY() == this.y) ? true : false;
        }
        else {
            return false;
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

}
