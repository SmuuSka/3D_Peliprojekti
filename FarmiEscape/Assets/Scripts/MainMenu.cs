using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void Playgame ()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame ()
   {
       Debug.Log ("QUIT");
       Application.Quit();
   }
    public void BackToMenu ()
    {
        StatePatternEnemy.withDog = false;
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        var scene = SceneManager.GetActiveScene().buildIndex;
        if(scene != 6)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        
    }

}
