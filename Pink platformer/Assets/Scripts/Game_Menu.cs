using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Game_Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        //Debug.Log("It works"); //it really works
        Application.Quit();
    }
}
