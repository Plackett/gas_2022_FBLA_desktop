using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private PieceGen pg;
    [SerializeField] private GameObject wordtemp;
    public GameObject PieceCounter;
    [SerializeField] private GameObject wordfolder;
    private List<string> words;
    private string letters = string.Empty;
    public bool menubool;
    private List<GameObject> UI = new List<GameObject>();

    public void Start(){
        if(!menubool){
            SceneManager.LoadScene("How2Play",LoadSceneMode.Additive);
        }
    }
    public void ObtainLetter(GameObject piece)
    {
        PieceCounter.GetComponent<TextMeshProUGUI>().text = string.Empty + (pg.wordlist[0].Length*2 - letters.Length-1);
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
        while(pos > 5){
            iters++;
            pos = pos - 6;
        }
        uitemp.GetComponent<RectTransform>().anchoredPosition = new Vector3(220 + (60*iters), 140-(pos*60),-100);
        uitemp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = letter + string.Empty;
        UI.Add(uitemp);
    }
    public void wincondition(){
        string winstring = string.Empty;
        int wincrit = 0;
        for(var i = 0; i < UI.Count;i++){
            if(UI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color == Color.green){
                winstring += UI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
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
                        UI[e].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
                        lettersclone = lettersclone.Remove(e,1);
                        lettersclone = lettersclone.Insert(e," ");
                    }
                }
            }
        }
        for(var i = 0;i<lettersclone.Length;i++){
            if(lettersclone[i] != ' '){
                UI[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
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
