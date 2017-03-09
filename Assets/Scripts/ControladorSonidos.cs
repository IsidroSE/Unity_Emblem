using UnityEngine;
using System.Collections;

public class ControladorSonidos : MonoBehaviour {

    //Variables que se utilizarán para decidir qué audio se reproduce
    public const string c_curar = "Curar";
    public const string c_critico = "Critico";
    public const string c_inicio_critico = "Inicio_Critico";
    public const string c_fallar = "c_fallar";
    public const string c_estandar = "c_estandar";
    public const string c_subir_nivel = "c_nivel";
    public const string f_jugador = "f_jugador";
    public const string f_enemigo = "f_enemigo";
    public const string aj_victoria = "victoria";
    public const string aj_derrota = "derrota";

    //Referencia al componente que reproducirá los sonidos
    private AudioSource sonidos;
    //Referencia al componente que reproducirá la música del juego
    public AudioSource musica_de_fondo;

    //--------------------------
    //Audios de ataques
    //Curar
    public AudioClip curar;
    //Tipos de golpe
    public AudioClip critico;
    public AudioClip inicio_critico;
    public AudioClip fallar;
    public AudioClip estandar;
    public AudioClip subirNivel;
    //--------------------------

    //--------------------------
    //Audio de fases
    public AudioClip fase_jugador;
    public AudioClip fase_enemigo;
    //--------------------------

    //--------------------------
    //Vectores donde se guardarán los clips que sonarán durante el transcurso de la partida
    public AudioClip[] clips_faseJugador;
    public AudioClip[] clips_faseEnemigo;
    //Números que decidirán qué música sonará
    int numClip_jugador = 1;
    int numClip_enemigo = 0;
    //--------------------------

    //--------------------------
    public AudioClip victoria;
    public AudioClip derrota;
    //--------------------------

    // Use this for initialization
    void Start () {
        //Obtendremos la referencia al audio source
        sonidos = this.GetComponent<AudioSource>();
    }
	
    //Reproduce un audio
	public void reproducirClip (string clip) {

        switch (clip) {
            case c_estandar: sonidos.clip = estandar; break;
            case c_fallar: sonidos.clip = fallar;  break;
            case c_critico: sonidos.clip = critico;  break;
            case c_inicio_critico: sonidos.clip = inicio_critico;  break;
            case c_curar: sonidos.clip = curar;  break;
            case f_jugador: sonidos.clip = fase_jugador; break;
            case f_enemigo: sonidos.clip = fase_enemigo; break;
            case c_subir_nivel: sonidos.clip = subirNivel; break;
            case aj_victoria: sonidos.clip = victoria; break;
            case aj_derrota: sonidos.clip = derrota; break;
        }

        sonidos.Play();

    }

    //Para la música de fondo que esté tocando en estos momentos
    public void parar_musica_de_fondo () {
        musica_de_fondo.Stop();
    }

    //Reproduce la siguiente música de fondo del vector del jugador (la que sonará durante el turno del jugador)
    public void reproducir_musica_de_fondo_jugador () {

        musica_de_fondo.clip = clips_faseJugador[numClip_jugador];

        musica_de_fondo.Play();

        if (numClip_jugador + 1 > clips_faseJugador.Length - 1) {
            numClip_jugador = 0;
        }
        else {
            numClip_jugador++;
        }

    }

    //Reproduce la siguiente música de fondo del vector del enemigo (la que sonará durante el turno del enemigo)
    public void reproducir_musica_de_fondo_enemigo () {

        musica_de_fondo.clip = clips_faseEnemigo[numClip_enemigo];

        musica_de_fondo.Play();

        if (numClip_enemigo + 1 > clips_faseEnemigo.Length - 1) {
            numClip_enemigo = 0;
        }
        else {
            numClip_enemigo++;
        }

    }

}
