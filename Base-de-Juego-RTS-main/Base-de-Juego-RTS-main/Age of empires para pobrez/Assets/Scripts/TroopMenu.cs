using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMenu : MonoBehaviour
{
    public GameObject troopMenu;

    void Start()
    {
        // Al inicio, el menú de tropas no estará activo
        troopMenu.SetActive(false);
    }

    void Update()
    {
        // Activar/desactivar el menú de tropas cuando se presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleTroopMenu();
        }
    }

    void ToggleTroopMenu()
    {
        // Activar o desactivar el menú de tropas
        troopMenu.SetActive(!troopMenu.activeSelf);
    }
}
