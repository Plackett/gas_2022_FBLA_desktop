using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    WaitForSeconds _delayBetweenCharactersYieldInstruction;

    public void StartTypeWriterOnText(Text textComponent, string stringToDisplay, float delayBetweenCharacters = 0.2f)
    {
        StartCoroutine(TypeWriterCoroutine(textComponent, stringToDisplay, delayBetweenCharacters));
    }
    
    IEnumerator TypeWriterCoroutine(Text textComponent, string stringToDisplay, float delayBetweenCharacters)
    {
        _delayBetweenCharactersYieldInstruction = new WaitForSeconds(delayBetweenCharacters);
        for(int i = 0; i < stringToDisplay.Length; i++)
        {
            textComponent.text = stringToDisplay.Substring(0, i);
            yield return _delayBetweenCharactersYieldInstruction;
        }
    }
}
