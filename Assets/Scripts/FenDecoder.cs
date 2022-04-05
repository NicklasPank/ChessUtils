using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenDecoder : MonoBehaviour
{
    PieceCreator pc;
    public string startingFen;
    void Start()
    {
        
    }
    private void Awake()
    {
        pc = gameObject.GetComponent<PieceCreator>();
        startingFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
    }
    public void translate(int[] decoded)
    {
        pc.clear();

        for (int i = 0; i < decoded.Length; i++)
        {
            if (decoded[i] == -1) { continue; }
            int file = i % 8;
            int rank = i / 8;
            bool isWhite = decoded[i] < 8;
            int type = decoded[i] & 7;
            pc.add(isWhite, type, file, rank);
        }
    }
    public int[] decode(string fen)
    {
        int[] decoded = new int[64];
        string wpieces = "PNBRKQ";
        string bpieces = "pnbrkq";
        string nums = "12345678";
        int ind = 56;
        for (int i = 0; i < fen.Length; i++)
        {
            string c = fen[i] + "";
            if (wpieces.Contains(c))
            {
                decoded[ind] = wpieces.IndexOf(c);
                ind++;
            }
            else if (bpieces.Contains(c))
            {
                decoded[ind] = bpieces.IndexOf(c) + 8;
                ind++;
            }
            else if (nums.Contains(c))
            {
                for (int j = 0; j < nums.IndexOf(c) + 1; j++)
                {
                    decoded[ind] = -1;
                    ind++;
                }
            }
            else if (c == "/")
            {
                ind -= 16;
            }
            else
            {
                Debug.Log("Invalid fen of: " + fen);
                return null;
            }
        }

        return decoded;
    }
}
