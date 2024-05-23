using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJuego : MonoBehaviour
{
    // M�todo para iniciar el juego
    public void Jugar()
    {
        // Cambiar a la escena especificada por n�mero (aseg�rate de tener la escena cargada en el Build Settings)
        SceneManager.LoadScene(1); // Cambia el n�mero de la escena seg�n tu configuraci�n
    }

    // M�todo para salir del juego
    public void Salir()
    {
        // Salir de la aplicaci�n
        Application.Quit();
    }
}
