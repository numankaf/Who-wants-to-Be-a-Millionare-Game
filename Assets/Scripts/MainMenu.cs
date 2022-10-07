using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void play()
    {
        StartCoroutine(playyyy());
    }

    // Update is called once per frame
    public void quit()
    {
        Debug.Log("Quited");
        Application.Quit();
    }

    private IEnumerator playyyy()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CharacterMenu");
    }
    
}
