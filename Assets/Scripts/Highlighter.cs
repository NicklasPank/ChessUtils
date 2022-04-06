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
        highlight(20, Color.yellow);
        highlight(19, Color.yellow);
    }
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FFB66D", out light);
        ColorUtility.TryParseHtmlString("#9C501A", out dark);
    }

    public void highlight(int pos, Color hl)
    {
        SpriteRenderer rend = organizer.getSquare(pos).GetComponent<SpriteRenderer>();
        float tint = ((pos%8 + pos/8) % 2) == 1 ? 1 : 0.95f;
        rend.color = hl * tint;
    }
}
