using UnityEngine;
using System.Collections;

public abstract class Personaje : MonoBehaviour {

    //Ataque estándar para todos los personajes
    public static int Ataque = 6;
    //Probabilidad de golpe estándar para todos los personajes
    public static int Golpe = 90;

    //Puntos de experiencia mínimos
    public static int Min_Exp = 0;
    //Puntos de experiencia máximos
    public static int Max_Exp = 99;

    //Tipo de unidades que hay en el juego (esto determinará el tipo de movimiento que podrán tener en el juego)
    //Unidad estándar
    public static string Estandar = "Estandar";
    //Las relantizaciones de las casillas se duplican
    public static string Caballo = "Caballo";
    //Igual que la estándar, pero esta puede cruzar ríos
    public static string Barbaro = "Barbaro";
    //Unidad voladora (no se ve afectado por ningún tipo de ralentización ni por bosques ni por montañas)
    public static string Volador = "Volador";
    /*Sanador es exactamente igual que la estándar, pero esta unidad no puede atacar y por lo tanto los cuadros que saldrán
    serán verdes en vez de rojos*/
    public static string Sanador = "Sanador";

    //Nombre de las animaciones de los personajes
    public static string Moviendose = "Moviendose";
    public static string MovIzquierda = "MovIzquierda";
    public static string MovDerecha = "MovDerecha";
    public static string MovAbajo = "MovAbajo";
    public static string MovArriba = "MovArriba";
    public static string Movido = "Movido";
    public static string AtaqueIzquierda = "AtaqueIzquierda";
    public static string AtaqueDerecha = "AtaqueDerecha";
    public static string AtaqueAbajo = "AtaqueAbajo";
    public static string AtaqueArriba = "AtaqueArriba";
    public static string Curar = "Curar";

    //ID del personaje
    int ID;

    //Posición del personaje en la matriz (mapa)
    protected int x;
    protected int y;

    //Imagen del personaje
    public Texture retrato;

    //Estadísticas básicas del personaje
    protected string nombre;
    protected string clase;
    protected int nivel;
    protected int puntos_de_experiencia;
    protected int pasos;
    protected int pv;
    protected int pv_maximos;
    protected string tipoUnidad;
    protected int alcance;
    protected int alcanceMinimo;
    protected bool movido;
    protected bool muerto = false;


    //Estadísticas del personaje
    protected int fuerza;
    protected int magia;
    protected int habilidad;
    protected int velocidad;
    protected int suerte;
    protected int defensa;
    protected int resistencia;

    //Crecimientos de las estadísticas del personaje
    protected int c_pv;
    protected int c_fuerza;
    protected int c_magia;
    protected int c_habilidad;
    protected int c_velocidad;
    protected int c_suerte;
    protected int c_defensa;
    protected int c_resistencia;
    // ------------------------------------------------------------------------------------------------
    //Getters y Setters
    public int getID () {
        return ID;
    }

    public void setID (int ID) {
        this.ID = ID;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }

    public string getNombre() {
        return nombre;
    }

    public string getClase() {
        return clase;
    }

    public void setNivel(int nivel) {
        this.nivel = nivel;
    }

    public int getNivel() {
        return nivel;
    }

    public void setExp(int exp) {
        puntos_de_experiencia = exp;
    }

    public int getExp() {
        return puntos_de_experiencia;
    }

    public int getPasos() {
        return pasos;
    }

    public int getPV_Maximos() {
        return pv_maximos;
    }

    public int getPV() {
        return pv;
    }

    public void setPV(int pv) {
        this.pv = pv;
    }

    public int getFuerza() {
        return fuerza;
    }

    public int getMagia() {
        return magia;
    }

    public int getHabilidad() {
        return habilidad;
    }

    public int getVelocidad() {
        return velocidad;
    }

    public int getSuerte() {
        return suerte;
    }

    public int getDefensa() {
        return defensa;
    }

    public int getResistencia() {
        return resistencia;
    }

    public Texture getRetrato() {
        return retrato;
    }

    public string gettipoUnidad() {
        return tipoUnidad;
    }

    public int getAlcance() {
        return alcance;
    }

    public int getAlcanceMinimo () {
        return alcanceMinimo;
    }

    public bool getMovido() {
        return movido;
    }

    public void setMovido(bool movido) {
        this.movido = movido;
    }

    public bool getMuerto() {
        return muerto;
    }

    public void setMuerto(bool muerto) {
        this.muerto = muerto;
    }

    // ------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------
    //Crear la unidad

    public abstract void inicializarPersonaje();
    // ------------------------------------------------------------------------------------------------

    //Sube de nivel al personaje
    public void subirDeNivel () {
        //Nivel
        nivel++;
        //Puntos de vida
        int numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_pv) pv_maximos++;
        //Fuerza
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_fuerza) fuerza++;
        //Magia
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_magia) magia++;
        //Habilidad
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_habilidad) habilidad++;
        //Velocidad
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_velocidad) velocidad++;
        //Suerte
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_suerte) suerte++;
        //Defensa
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_defensa) defensa++;
        //Resistencia
        numeroAleatorio = Random.Range(0, 100);
        if (numeroAleatorio <= c_resistencia) resistencia++;
    }

}
