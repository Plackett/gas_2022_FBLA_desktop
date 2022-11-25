using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimize : MonoBehaviour
{
    private bool Closed;
    [SerializeField] private List<GameObject> Menuset;
    [SerializeField] private GameObject MiniBut;
    [SerializeField] private Text MiniButText;

    public void Mini(){
        if(Closed == false){
            foreach(GameObject g in Menuset){
                g.SetActive(false);
                if(g.transform.childCount > 0){
                    foreach(Transform child in g.transform){
                        child.gameObject.SetActive(false);
                    }
                }
            }
            MiniBut.GetComponent<RectTransform>().anchoredPosition = new Vector3(459,26,0);
            MiniButText.text = "?";
            MiniBut.GetComponent<RectTransform>().localScale = new Vector2(0.3f,0.3f);
            Closed = true;
        } else {
            foreach(GameObject g in Menuset){
                g.SetActive(true);
                if(g.transform.childCount > 0){
                    foreach(Transform child in g.transform){
                        child.gameObject.SetActive(true);
                    }
                }
            }
            MiniBut.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,33,0);
            MiniButText.text = "Got it";
            MiniBut.GetComponent<RectTransform>().localScale = new Vector2(0.5f,0.5f);
            Closed = false;
        }
    }
}
