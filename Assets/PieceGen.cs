using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Randomsys = System.Random;
using UnityEngine;
using TMPro;

public class PieceGen : MonoBehaviour
{
    [SerializeField] private int difficulty;
    public List<string> wordlist;
    [SerializeField] private GameObject Vrow;
    [SerializeField] private GameObject Vpos;
    [SerializeField] private Material WinMat;
    [SerializeField] private Material LoseMat;
    public bool menubool;
    private bool gen;
    public bool cooldown;
    public bool verhor = false;
    static Randomsys num = new Randomsys();
    private List<GameObject> Tower = new List<GameObject>();
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
    void Start()
    {
        cooldown = true;
        Generate(difficulty, wordlist);
    }

    void Destroy(List<GameObject> Tower)
    {
        for (int i = 0; i < Tower.Count; i++)
        {
            Destroy(Tower[i]);
        }
    }

    public void EndEffect(bool winlose)
    {
        if(winlose == true)
        {
            for(var f = 0; f < Tower.Count(); f++)
            {
                Tower[f].GetComponent<MeshRenderer>().material = WinMat;
                Tower[f].GetComponent<PieceSelection>().IsOnTower = false;
            }
        } else
        {
            for (var f = 0; f < Tower.Count(); f++)
            {
                Tower[f].GetComponent<MeshRenderer>().material = LoseMat;
                Tower[f].GetComponent<PieceSelection>().IsOnTower = false;
            }
        }
    }

    public IEnumerator CooldownTimer()
    {
        cooldown = false;
        yield return new WaitForSeconds(0.75f);
        cooldown = true;
    }

    public void Generate(int diff, List<string> wordsraw)
    {
        this.gameObject.GetComponent<Controller>().PieceCounter.GetComponent<TextMeshProUGUI>().text = string.Empty + wordsraw[0].Length;
        GameObject currow;
        string wlout = string.Empty;
        StringBuilder tempwords = new StringBuilder(diff);
        if (gen == true)
        {
            Destroy(Tower);
        }
        for (var p = 0; p < wordsraw.Count; p++)
        {
            wlout = wlout + wordsraw[p];
        }
        wlout = Regex.Replace(wlout, "[^A-Z]", string.Empty);
        string wordsr = new string(wlout.ToCharArray().
        OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
        for(int d = 0; d < diff; d++){
            int randletter = Random.Range(0, 25);
            tempwords.Append(string.Empty + alphabet[randletter]);
        }
        for(int c = 0; c < wordsr.Length; c++)
        {
            tempwords.Remove(c, 1);
            tempwords.Insert(c, wordsr[c]);
        }
        string words = tempwords.ToString();
        for(var i = 0; i < diff; i++)
        {
            verhor = !verhor;
            if (verhor == true)
            {
                currow = Instantiate(Vrow);
                currow.transform.position = new Vector3(Vpos.transform.position.x, Vpos.transform.position.y+(2 * i), Vpos.transform.position.z);
            }
            else
            {
                currow = Instantiate(Vrow);
                currow.transform.Rotate(new Vector3(0,-90f,0),Space.Self);
                currow.transform.position = new Vector3(Vpos.transform.position.x-2, Vpos.transform.position.y+(2 * i), Vpos.transform.position.z-2);
            }
            int seedresult = Random.Range(0,2);
            for(int v = 0; v < 3; v++){
                if(v == seedresult){
                    if(i >= words.Length){
                        int randletter = Random.Range(0,25);
                        currow.transform.GetChild(v).GetChild(0).GetComponent<TextMeshPro>().text = string.Empty + alphabet[randletter];
                    } else {
                        currow.transform.GetChild(v).GetChild(0).GetComponent<TextMeshPro>().text = string.Empty + words[i];
                    }
                } else {
                    int randletter = Random.Range(0,25);
                    currow.transform.GetChild(v).GetChild(0).GetComponent<TextMeshPro>().text = string.Empty + alphabet[randletter];
                }
                Tower.Add(currow.transform.GetChild(v).gameObject);
                currow.transform.GetChild(v).GetComponent<PieceSelection>().p = this;
                currow.transform.GetChild(v).GetComponent<PieceSelection>().IsOnTower = !menubool;
                currow.transform.GetChild(v).GetComponent<PieceSelection>().Control = this.gameObject.GetComponent<Controller>();
            }
        }
        gen = true;
    }
}
