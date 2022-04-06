using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organizer : MonoBehaviour
{
    public GameObject board;
    GameObject[] squares;

    private void Awake()
    {
        string files = "abcdefgh";
        string ranks = "12345678";
        squares = new GameObject[64];
        for (int pos = 0; pos < squares.Length; pos++)
        {
            string name = files[pos % 8] + "" + ranks[pos / 8];
            squares[pos] = board.transform.Find(name).gameObject;
        }
    }

    public GameObject getSquare(int pos)
    {
        return squares[pos];
    }
}
