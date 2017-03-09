using UnityEngine;
using System.Collections;

public class Virion : Arquero {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Virion.Nombre");
    }

}
