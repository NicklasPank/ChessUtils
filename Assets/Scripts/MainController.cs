using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject board;

    BitBoards bitboards;

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
    }
}
