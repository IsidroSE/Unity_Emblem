using UnityEngine;
using System.Collections;

public class Cordelia : JinetePegaso {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Cordelia.Nombre");
    }

}
