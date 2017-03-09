using UnityEngine;
using System.Collections;

public class Cervantes : InfPesada {

    public override void inicializarPersonaje() {
        base.inicializarPersonaje();
        nombre = PlayerPrefs.GetString("Cervantes.Nombre");
    }

}