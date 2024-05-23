using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa; // El objeto del men� de pausa que queremos desactivar/activar
    private bool juegoPausado = false; // Variable para rastrear si el juego est� pausado

    void Update()
    {
        // Si se presiona la tecla Escape, alternar la pausa del juego y mostrar/ocultar el men� de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    // M�todo para pausar el juego
    void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f; // Detiene el tiempo del juego para pausarlo
        menuPausa.SetActive(true); // Muestra el men� de pausa
    }

    // M�todo para reanudar el juego
    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        menuPausa.SetActive(false); // Oculta el men� de pausa
    }

    // M�todo para volver al men� principal (escena 0)
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Aseg�rate de que el tiempo del juego se reanude
        SceneManager.LoadScene(0); // Cambia a la escena 0 (o la escena principal)
    }
}
