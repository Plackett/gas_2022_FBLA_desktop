using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public List<GameObject> Options;
    [SerializeField] private GameObject Selector;
    [SerializeField] private List<AudioClip> SFX;
    [SerializeField] private AudioSource gamesound;
    private bool cool = true;
    private bool cursornah = false;
    [SerializeField] private float cooldelay = 0.2f;
    private int selected = 0;
    private int FrameCounter = 0;

    void Start(){
        Selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(93,-150); 
        selected = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(cool == true){
            if(Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0){
                if(selected > 0){
                    Options[selected].GetComponent<TextMeshProUGUI>().text = Options[selected].name;
                    selected--;
                    PlayAudio(0);
                    StartCoroutine(Cooldown(cooldelay));
                }
            } else {
                if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0){
                    if(selected < 5){
                        Options[selected].GetComponent<TextMeshProUGUI>().text = Options[selected].name;
                        selected++;
                        PlayAudio(0);
                        StartCoroutine(Cooldown(cooldelay));
                    }
                }
            }
        }
    }

    private void PlayAudio(int num){
        gamesound.clip = SFX[num];
        gamesound.Play();
    }
    
    void FixedUpdate(){
        Vector2 expectedlocation = new Vector2(93,-150-40*selected);
        if(Selector.GetComponent<RectTransform>().anchoredPosition != expectedlocation){
            Selector.GetComponent<RectTransform>().anchoredPosition = expectedlocation;
        }
        FrameCounter++;
        if(FrameCounter >= 30){
            cursornah = !cursornah;
            if(cursornah == true){
                Options[selected].GetComponent<TextMeshProUGUI>().text += "|";
            } else {
                Options[selected].GetComponent<TextMeshProUGUI>().text = Options[selected].name;
            }
            FrameCounter = 0;
        }
    }

    IEnumerator Cooldown(float time){
        cool = false;
        yield return new WaitForSeconds(time);
        cool = true;
    }
}
