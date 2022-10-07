using System.Collections;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CharacterManager : MonoBehaviour
{
   public AudioSource adSource;
   public AudioClip nextAndBack;
   public AudioClip playsound;
   public AudioClip backsound;
   
   public SpriteRenderer sr;
   public CharacterDB ch;
   public GameObject playerSkin;
   public TextMeshProUGUI playerName;
   private int selected = 0;
   
   public void nextOption()
   {
      adSource.clip = nextAndBack;
      adSource.Play();
      
      EventSystem.current.SetSelectedGameObject(null);
      selected++;
      if (selected >=ch.arr.Length)
      {
         selected = 0;
      }

      sr.GetComponent<Image>().sprite = ch.arr[selected].chacarterSprite;
      playerName.text = ch.arr[selected].name;
     // playerSkin.GetComponent<Image>().sprite = sprites[selected];
   }
   
   public void prevOption()
   {
      adSource.clip = nextAndBack;
      adSource.Play();
      EventSystem.current.SetSelectedGameObject(null);
      selected--;
      if (selected <0)
      {
         selected = ch.arr.Length-1;
      }

      sr.GetComponent<Image>().sprite = ch.arr[selected].chacarterSprite;
      playerName.text = ch.arr[selected].name;
      //playerSkin.GetComponent<Image>().sprite = sprites[selected];
   }

   public void PlayGame()
   {
      StartCoroutine(goo("play"));

   }
   public void back()
   {
     
      StartCoroutine(goo("back"));
   }

   private IEnumerator goo(string s)
   {
      
      if (s == "play")
      {
         adSource.clip = playsound;
         adSource.Play();
         yield return new WaitForSeconds(2f);
         /*#if UNITY_EDITOR
         PrefabUtility.SaveAsPrefabAsset(playerSkin,"Assets/CharacterMenuSprites/Characters/player.prefab");
         #endif*/
         PlayerPrefs.SetInt("selected",selected);
         
         SceneManager.LoadScene("Game");
      }

      if (s == "back")
      {
         adSource.clip = backsound;
         adSource.Play();
         yield return new WaitForSeconds(0.5f);
         
         SceneManager.LoadScene("Menu");
         
      }
   }
   
   

}
