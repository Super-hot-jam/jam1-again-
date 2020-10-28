 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
 using UnityEngine.EventSystems;
 
 public class flashingTextScript : MonoBehaviour {
 
  Text flashingText;

  void Start()
  {
   flashingText = GetComponent<Text>();

   StartCoroutine(FlashText());
  }

  public IEnumerator FlashText()
  {
    while(true)
    {
    flashingText.text= "";
    yield return new WaitForSeconds(.5f);
    flashingText.text= "Press [A] to launch         file commander";
    yield return new WaitForSeconds(.5f);
    }
  }
  
 }