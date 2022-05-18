using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Kalle lis‰si 12.5
public class GameEndingTriggers : MonoBehaviour
{
    public GameObject endingPanel;
    public Text endingText;

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndTrigger") && StatePatternEnemy.withDog == true)
        {
            SceneManager.LoadScene(8);


            ////n‰ytet‰‰n lopputeksti            
            //endingText.enabled = true;

            ////estet‰‰n pelaajaa liikkumasta pelin loppumisen j‰lkeen:
            //GetComponent<MoveScripts>().enabled = false;
            ////estet‰‰n pelaajan k‰‰ntyminen yms. 
            //GetComponent<MoveUserInput>().enabled = false;
            ////asetetaan peli "pauselle" ettei ufo /viholliset en‰‰ liiku
            //Time.timeScale = 0;
            
        }

    }


}
