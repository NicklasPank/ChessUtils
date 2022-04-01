using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPiece : MonoBehaviour
{
    Sprite[] pieces;
    // Start is called before the first frame update
    void Start()
    {
        pieces = Resources.LoadAll<Sprite>("pieces");
    }

    void add(bool white, int type)
    {
        //0 = pawn, 1 = knight, 2 = bishop, 3 = rook, 4 = king, 5 = queen
        int ind = 0;
        if (!white) { ind += 6; }

        switch (type)
        {
            case 0:
                ind += 5;
                break;
            case 1:
                ind += 3;
                break;
            case 2:
                ind += 2;
                break;
            case 3:
                ind += 4;
                break;
            case 4:
                ind += 0;
                break;
            case 5:
                ind += 1;
                break;
            default:
                ind = -1;
                break;

        }

        SpriteRenderer rend = gameObject.transform.Find("e1").GetChild(0).GetComponent<SpriteRenderer>();
        rend.sprite = pieces[ind];
    }
}
