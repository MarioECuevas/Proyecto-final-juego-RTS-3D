using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa; // El objeto del menú de pausa que queremos desactivar/activar
    private bool juegoPausado = false; // Variable para rastrear si el juego está pausado

    void Update()
    {
        // Si se presiona la tecla Escape, alternar la pausa del juego y mostrar/ocultar el menú de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    // Método para pausar el juego
    void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f; // Detiene el tiempo del juego para pausarlo
        menuPausa.SetActive(true); // Muestra el menú de pausa
    }

    // Método para reanudar el juego
    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        menuPausa.SetActive(false); // Oculta el menú de pausa
    }

    // Método para volver al menú principal (escena 0)
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Asegúrate de que el tiempo del juego se reanude
        SceneManager.LoadScene(0); // Cambia a la escena 0 (o la escena principal)
    }
}
