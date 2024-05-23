using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJuego : MonoBehaviour
{
    // Método para iniciar el juego
    public void Jugar()
    {
        // Cambiar a la escena especificada por número (asegúrate de tener la escena cargada en el Build Settings)
        SceneManager.LoadScene(1); // Cambia el número de la escena según tu configuración
    }

    // Método para salir del juego
    public void Salir()
    {
        // Salir de la aplicación
        Application.Quit();
    }
}
