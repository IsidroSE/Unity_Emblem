using UnityEngine;
using System.Collections;

public class Basilio : Barbaro {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Basilio.Nombre");
    }

}
