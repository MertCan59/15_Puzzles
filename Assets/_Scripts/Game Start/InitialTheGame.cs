using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialTheGame : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
