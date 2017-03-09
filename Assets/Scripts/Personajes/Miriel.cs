using UnityEngine;
using System.Collections;

public class Miriel : Mago {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Miriel.Nombre");
    }

}
