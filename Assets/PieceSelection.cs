using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PieceSelection : MonoBehaviour
{
    public PieceGen p;
    public Controller Control;
    Outline outline;
    public bool IsOnTower;

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "LosePlane")
        {
            p.EndEffect(false);
        }
    }

    void OnMouseOver()
    {
        if(outline == null && IsOnTower == true)
        {
            outline = gameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.blue;
            outline.OutlineWidth = 10f;
        }
    }

    void OnMouseDown()
    {
        if(outline != null && p.cooldown == true)
        {
            p.CooldownTimer();
            Control.ObtainLetter(this.gameObject);
            this.gameObject.SetActive(false);
            
        }
    }

    void OnMouseExit()
    {
        Destroy(outline);
    }
}
