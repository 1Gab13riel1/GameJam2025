using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); //replace with first level name
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}   

