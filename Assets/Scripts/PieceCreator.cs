using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCreator : MonoBehaviour
{
    Sprite[] pieces;
    string files;
    string ranks;
    void Start()
    {
    }

    private void Awake()
    {
        pieces = Resources.LoadAll<Sprite>("pieces");
        files = "abcdefgh";
        ranks = "12345678";
    }

    public void add(bool white, int type, int file, int rank)
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
        
        string sqName = files[file] + "" + ranks[rank];

        SpriteRenderer rend = gameObject.transform.Find(sqName).GetChild(0).GetComponent<SpriteRenderer>();
        rend.sprite = pieces[ind];
    }
    public void clear()
    {
        Transform board = gameObject.transform;
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                string name = files[file] + "" + ranks[rank];
                SpriteRenderer rend = board.Find(name).GetChild(0).GetComponent<SpriteRenderer>();
                rend.sprite = null;
            }
        }
    }
}
