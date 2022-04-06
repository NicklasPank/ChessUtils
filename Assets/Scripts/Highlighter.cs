using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Organizer organizer;
    Color light, dark;
    // Start is called before the first frame update
    void Start()
    {
        //tint(20, Color.yellow);
        //tint(19, Color.yellow);
    }
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FFB66D", out light);
        ColorUtility.TryParseHtmlString("#9C501A", out dark);
    }

    public void tint(int pos, Color hl)
    {
        SpriteRenderer rend = organizer.getSquare(pos).GetComponent<SpriteRenderer>();
        Color prev = rend.color;
        //float avg = (prev.r + prev.g + prev.b) / 3;
        Color tint = new Color(hl.r * prev.r, hl.g * prev.g, hl.b * prev.b, prev.a);
        rend.color = hl;
        Debug.Log(prev.r);
        Debug.Log(prev.g);
        Debug.Log(prev.b);
    }
}
