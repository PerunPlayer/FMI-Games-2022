using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject player;
    private PlayerBehaviour playerBehaviour;
    private bool isInitialized;

    private void Start()
    {
        player = null;
        playerBehaviour = null;
        isInitialized = false;
    }

    private void LateUpdate()
    {
        if (!isInitialized)
        {
            player = GameObject.Find("Player");
            isInitialized = true;
            playerBehaviour = player.GetComponent<PlayerBehaviour>();
        }
    }

    public void Resume()
    {
        playerBehaviour.UnpauseGame();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
