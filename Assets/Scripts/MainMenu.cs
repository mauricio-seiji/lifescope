using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartSimulation()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LifeSimulation");
    }
}
