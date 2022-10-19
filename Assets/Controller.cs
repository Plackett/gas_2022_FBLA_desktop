using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private PieceGen pg;
    [SerializeField] private TextMeshProUGUI HeightIn;
    [SerializeField] private TextMeshProUGUI WordIn;
    [SerializeField] private GameObject wordtemp;
    [SerializeField] private GameObject wordfolder;
    private List<string> words;
    private Dictionary<string,List<string>> discoveredparts;
    private Dictionary<string, GameObject> UI;

    public void ObtainLetter(GameObject piece)
    {
        piece.GetComponent<PieceSelection>().IsOnTower = false;
        string letter = piece.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        for(int i = 0; i < words.Count; i++)
        {
            for(int w = 0; w < words[i].Length; w++)
            {
                if(letter == words[i][w] + string.Empty)
                {
                    if (discoveredparts.ContainsKey(words[i]))
                    {
                        discoveredparts[words[i]].Add(letter);
                    } else
                    {
                        List<string> wordnew = new List<string>();
                        wordnew.Add(letter);
                        discoveredparts.Add(words[i], wordnew);
                    }
                }
            }
            UpdateUI(words[i]);
        }
    }

    private void UpdateUI(string key)
    {
        List<string> temp = new List<string>();
        string formattedstring = new string(char.Parse("_"),key.Length);
        for(var x = 0; x < discoveredparts.Count; x++)
        {
            temp.Add(discoveredparts[key][x]);
            Debug.Log(temp[x]);
        }
        if (!UI.ContainsKey(key))
        {
            GameObject newUI = Instantiate(wordtemp, new Vector3(-259f, 120 - (40 * UI.Count), -249f), Quaternion.identity, wordfolder.transform) as GameObject;
            newUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-259f, 120 - (40 * UI.Count), -249f);
            newUI.name = key;
            UI.Add(key, newUI);
        }
        for (var o = 0; o < key.Length; o++)
        {
            for (var e = 0; e < temp.Count; e++)
            {
                if (temp[e] == key[o] + string.Empty)
                {
                    formattedstring = formattedstring.Remove(o, 1);
                    formattedstring = formattedstring.Insert(o, key[o] + string.Empty) ;
                    Debug.Log(key[o]);
                    Debug.Log(formattedstring[o]);
                    temp.RemoveAt(e);
                }
            }
        }
        UI[key].GetComponent<TextMeshProUGUI>().text = formattedstring;
        if(formattedstring == key)
        {
            pg.EndEffect(true);
        }
    }

    public void GenerateBut()
    {
        string[] wordlistarray = WordIn.text.ToUpper().Split(" ");
        List<string> wordlist = wordlistarray.ToList();
        Debug.Log(wordlist[0]);
        string height = string.Empty;
        var matches = Regex.Matches(HeightIn.text, @"\d+");
        foreach (var match in matches)
        {
            height += match;
        }
        if (int.TryParse(height, out int difficulty) == true)
        {
            Debug.Log(difficulty);
            pg.Generate(difficulty, wordlist);
        } else
        {
            Debug.Log("invalid height");
        }
        words = wordlist;
        discoveredparts = new Dictionary<string, List<string>>();
        clearUI();
        genUI(words);
    }

    void genUI(List<string> word)
    {
        foreach(string j in word)
        {
            GameObject newUI = Instantiate(wordtemp, new Vector3(-259f, 120 - (40 * UI.Count), -249f), Quaternion.identity, wordfolder.transform) as GameObject;
            newUI.name = j;
            newUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-259f, 120 - (40 * UI.Count), -249f);
            UI.Add(j, newUI);
            newUI.transform.GetComponent<TextMeshProUGUI>().text = new string(char.Parse("_"), j.Length);
        }
    }
    
    void clearUI()
    {
        UI = new Dictionary<string, GameObject>();
        for(var z = 0; z < wordfolder.transform.childCount; z++)
        {
            Destroy(wordfolder.transform.GetChild(z).gameObject);
        }
    }
}
