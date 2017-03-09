using UnityEngine;
using System.Collections;

//LLeva a cabo el seguimiento de la cámara hacia el puntero
public class SeguimientoCamara : MonoBehaviour {

    //El objetivo que seguirá la cámara allá a donde vaya, es decir el puntero
    public Transform objetivo_a_seguir;
    //Velocidad a la que la cámara seguirá a su objetivo
    public float velocidad = 5f;
    //Indica la separación entre el puntero y la cámara
    Vector3 separacion;
    //Referencia al mapa
    public Mapa mapa;

    // Use this for initialization
    void Start () {
        //Con esto conseguiremos que cuando el puntero se mueva, mantenga siempre la misma separación inicial que teniamos cuando empezamos
        separacion = transform.position - objetivo_a_seguir.position;
    }
	
	// Update is called once per frame
	void Update () {
        //Aquí pondremos la posición a la que se moverá la cámara, que basicamente es la posición del puntero + la separación inicial
        Vector3 nuevaPosCamara = objetivo_a_seguir.position + separacion;

        //Para que la cámara no se salga del mapa, hemos puesto unos limites en este los cuales la cámara no deberá pasar
        //X
        if (nuevaPosCamara.x < mapa.limiteMovCamaraX1) nuevaPosCamara.x = mapa.limiteMovCamaraX1;
        else if (nuevaPosCamara.x > mapa.limiteMovCamaraX2) nuevaPosCamara.x = mapa.limiteMovCamaraX2;
        //Y
        if (nuevaPosCamara.y < mapa.limiteMovCamaraY1) nuevaPosCamara.y = mapa.limiteMovCamaraY1;
        else if (nuevaPosCamara.y > mapa.limiteMovCamaraY2) nuevaPosCamara.y = mapa.limiteMovCamaraY2;

        //Ahora, le diriamos que avanzará de su posición actual a la nueva posición 5 unidades por segundo
        transform.position = Vector3.Lerp(transform.position, nuevaPosCamara, velocidad * Time.deltaTime);
    }
}
