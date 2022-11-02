using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private PieceGen pg;
    [SerializeField] private GameObject wordtemp;
    public GameObject PieceCounter;
    [SerializeField] private GameObject wordfolder;
    private List<string> words;
    private string letters = string.Empty;
    private List<GameObject> UI = new List<GameObject>();

    public void ObtainLetter(GameObject piece)
    {
        PieceCounter.GetComponent<TextMeshProUGUI>().text = string.Empty + (pg.wordlist[0].Length - letters.Length);
        if(pg.wordlist[0].Length - letters.Length <= 10){
            PieceCounter.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        piece.GetComponent<PieceSelection>().IsOnTower = false;
        string letter = piece.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        letters = letters + letter;
        checkLetters();
        wincondition();
    }

    public void addUI(char letter, int pos){
        int iters = 0;
        GameObject uitemp = Instantiate(wordtemp, wordfolder.transform);
        while(pos > 10){
            iters++;
            pos = pos - 11;
        }
        uitemp.GetComponent<RectTransform>().anchoredPosition = new Vector3(300 + (30*iters), 120-(pos*30),-100);
        uitemp.GetComponent<TextMeshProUGUI>().text = letter + string.Empty;
        UI.Add(uitemp);
    }
    public void wincondition(){
        string winstring = string.Empty;
        int wincrit = 0;
        for(var i = 0; i < UI.Count;i++){
            if(UI[i].GetComponent<TextMeshProUGUI>().color == Color.green){
                winstring += UI[i].GetComponent<TextMeshProUGUI>().text;
            }
        }
        if(letters.Length >= pg.wordlist[0].Length*2){
            pg.EndEffect(false);
        }
        if(winstring.Length >= pg.wordlist[0].Length){
            for(var v = 0; v < pg.wordlist[0].Length;v++){
                int index = 0;
                foreach(char c in winstring){
                    index++;
                    if(c == pg.wordlist[0][v]){
                        if(index > 0){
                            winstring.Remove(index-1,1);
                        } else {
                            winstring.Remove(0,1);
                        }
                        wincrit++;
                    }
                }
            }
        }
        if(wincrit >= pg.wordlist[0].Length){
            pg.EndEffect(true);
        } else {
            Debug.Log(winstring);
            Debug.Log(wincrit);
        }
    }

    public void checkLetters(){
        clearUI();
        string lettersclone = letters + string.Empty;
        for(var e = 0; e < letters.Length;e++){
            addUI(letters[e],e);
            for(int i = 0;i < pg.wordlist.Count;i++){
                foreach(char v in pg.wordlist[i]){
                    if(v == lettersclone[e]){
                        UI[e].GetComponent<TextMeshProUGUI>().color = Color.green;
                        lettersclone.Remove(e,1);
                    }
                }
            }
        }
    }
    
    void clearUI()
    {
        UI = new List<GameObject>();
        for(var z = 0; z < wordfolder.transform.childCount; z++)
        {
            Destroy(wordfolder.transform.GetChild(z).gameObject);
        }
    }
}
