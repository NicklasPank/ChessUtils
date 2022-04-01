using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBoard : MonoBehaviour
{
    public GameObject square;
    // Start is called before the first frame update
    void Start()
    {
        string files = "abcdefgh";
        string ranks = "12345678";
        Color light, dark;
        ColorUtility.TryParseHtmlString("#FFB66D", out light);
        ColorUtility.TryParseHtmlString("#9C501A", out dark);
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                GameObject inst = Instantiate(square);
                inst.transform.parent = gameObject.transform;
                inst.transform.position = new Vector3(file, rank, 0);
                inst.name = files[file] + "" + ranks[rank];

                Color col = (rank + file) % 2 == 0 ? dark : light;
                inst.GetComponent<SpriteRenderer>().color = col;

                SpriteRenderer pieceRenderer = new GameObject("Piece").AddComponent<SpriteRenderer>();
                pieceRenderer.transform.parent = inst.transform;
                pieceRenderer.transform.localPosition = new Vector3(0, 0, -0.1f);
                pieceRenderer.transform.localScale = new Vector3(0.34f, 0.34f, 0.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
