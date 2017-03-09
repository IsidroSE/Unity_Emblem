using UnityEngine;
using System.Collections;

public class Walhart : Jinete {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Walhart.Nombre");
    }

}
