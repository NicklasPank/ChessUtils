using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject board;

    Organizer organizer;
    BitBoards bitboards;

    public int toggle;

    void Start()
    {
        FenDecoder fd = board.GetComponent<FenDecoder>();
        int[] starting = fd.decode(fd.startingFen);
        fd.translate(starting);
        bitboards.setBitBoards(starting);

    }
    private void Awake()
    {
        bitboards = new BitBoards();
        organizer = gameObject.GetComponent<Organizer>();
        toggle = 0;
    }
    void Update()
    {
        if (toggle == 1)
        {
            strictMove(5, 20);
            toggle = 0;
        }
    }
    public void strictMove(int from, int to)
    {
        if (from == to) { return; }
        SpriteRenderer prev = organizer.getPiece(from);
        SpriteRenderer target = organizer.getPiece(to);
        target.sprite = prev.sprite;
        prev.sprite = null;
    }
}
