using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitBoardHelper
{
    //Directions are in this order, ind 0 -> 7:
    //          {1, 8, 7, 9, -1, -8, -7, -9}
    ulong[,] rays;  // rays[pos, dir]
    
    //private HashSet<>

    private int[] bsf64;
    private int[] bsr64;
    private ulong debruijn;

    public ulong[] knightAttacks;
    public ulong[,] pawnAttacks;
    public ulong[] kingAttacks;

    public BitBoardHelper()
    {
        fillBitscans();
        fillKnightAttacks();
        fillPawnAttacks();
        fillKingAttacks();
        fillRays();
    }
    public void printBitBoard(ulong bb)
    {
        string str = System.Convert.ToString((long)bb, 2);
        for (int i = str.Length; i < 64; i++)
        {
            str = str.Insert(0, "0");
        }
        char[] temp = str.ToCharArray();
        str = "";
        for (int i = 0; i < 8; i++)
        {
            for (int j = 7; j >= 0; j--)
            {
                str += temp[(i * 8) + j];
            }
            str += "\n";
        }
        Debug.Log(str);
    }
    public ulong getBishopAttacks(ulong occupied, int pos)
    {
        ulong attacks = 0;
        /*
        ulong attackLine = rays[pos, 2];
        ulong blocker = attackLine & occupied;
        int blockerPos = bitScanForward(blocker | 0x8000000000000000);
        attacks |= attackLine ^ rays[blockerPos, 2];

        attackLine = rays[pos, 3];
        blocker = attackLine & occupied;
        blockerPos = bitScanForward(blocker | 0x8000000000000000);
        attacks |= attackLine ^ rays[blockerPos, 3];

        attackLine = rays[pos, 6];
        blocker = attackLine & occupied;
        blockerPos = bitScanReverse(blocker | 1);
        attacks |= attackLine ^ rays[blockerPos, 3];

        attackLine = rays[pos, 7];
        blocker = attackLine & occupied;
        blockerPos = bitScanReverse(blocker | 1);
        attacks |= attackLine ^ rays[blockerPos, 3];
         */
        int[] ind = new int[] { 2, 3, 6, 7 };
        for (int i = 0; i < ind.Length; i++)
        {
            attacks |= getRayAttacks(occupied, ind[i], pos);
        }

        return attacks;
    }
    public ulong getRookAttacks(ulong occupied, int pos)
    {
        ulong attacks = 0;
        int[] ind = new int[] { 0, 1, 4, 5 };
        for (int i = 0; i < ind.Length; i++)
        {
            attacks |= getRayAttacks(occupied, ind[i], pos);
        }

        return attacks;
    }
    public ulong getRayAttacks(ulong occupied, int dir8, int pos)
    {
        ulong attacks = rays[pos, dir8];
        ulong blocker = attacks & occupied;

        pos = (dir8 < 4) ? bitScanForward(blocker | 0x8000000000000000) :
                            bitScanReverse(blocker | 1);
        return attacks ^ rays[pos, dir8];
    }
    public int[] positionsIn(ulong bb)
    {
        return null;
    }
    private int bitScanForward(ulong bb)
    {
        return bsf64[((bb & (0 - bb)) * debruijn) >> 58];
    }
    private int bitScanReverse(ulong bb)
    {
        bb |= bb >> 1;
        bb |= bb >> 2;
        bb |= bb >> 4;
        bb |= bb >> 8;
        bb |= bb >> 16;
        bb |= bb >> 32;
        return bsr64[(bb * debruijn) >> 58];
    }
    private void fillRays()
    {
        rays = new ulong[64, 8];
        int[] dir = new int[] { 1, 8, 7, 9, -1, -8, -7, -9 };
        for (int pos = 0; pos < 64; pos++)
        {
            for (int i = 0; i < 8; i++)
            {
                int spot1 = pos + dir[i];
                ulong ray1 = 0;
                int curFile = pos % 8;
                int curRank = pos / 8;

                while (spot1 < 64 && spot1 >= 0
                    && Mathf.Abs((spot1 % 8) - curFile) <= 1
                    && Mathf.Abs((spot1 / 8) - curRank) <= 1)
                {
                    ray1 += (ulong)Mathf.Pow(2, spot1);
                    curFile = spot1 % 8;
                    curRank = spot1 / 8;
                    spot1 += dir[i];
                }

                rays[pos, i] = ray1;
            }
        }
    }
    private void fillBitscans()
    {
        bsf64 = new int[64] {
            0,  1, 48,  2, 57, 49, 28,  3,
            61, 58, 50, 42, 38, 29, 17,  4,
            62, 55, 59, 36, 53, 51, 43, 22,
            45, 39, 33, 30, 24, 18, 12,  5,
            63, 47, 56, 27, 60, 41, 37, 16,
            54, 35, 52, 21, 44, 32, 23, 11,
            46, 26, 40, 15, 34, 20, 31, 10,
            25, 14, 19,  9, 13,  8,  7,  6
        };
        bsr64 = new int[64] {
            0, 47,  1, 56, 48, 27,  2, 60,
            57, 49, 41, 37, 28, 16,  3, 61,
            54, 58, 35, 52, 50, 42, 21, 44,
            38, 32, 29, 23, 17, 11,  4, 62,
            46, 55, 26, 59, 40, 36, 15, 53,
            34, 51, 20, 43, 31, 22, 10, 45,
            25, 39, 14, 33, 19, 30,  9, 24,
            13, 18,  8, 12,  7,  6,  5, 63
        };
        debruijn = 0x03f79d71b4cb0a89;
    }
    private void fillKnightAttacks()
    {
        knightAttacks = new ulong[64];
        int[] moves = new int[8] { 6, 15, 17, 10, -6, -15, -17, -10 };
        int[] fileChange = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
        int[] rankChange = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

        for (int pos = 0; pos < 64; pos++)
        {
            ulong atts = 0;
            int file = pos % 8;
            int rank = pos / 8;
            for (int dir = 0; dir < 8; dir++)
            {
                int tempFile = file + fileChange[dir];
                int tempRank = rank + rankChange[dir];
                if (tempFile >= 0 && tempFile < 8
                    && tempRank >= 0 && tempRank < 8)
                {
                    atts += (ulong)Mathf.Pow(2, pos + moves[dir]);
                }
            }
            knightAttacks[pos] = atts;
        }
    }
    private void fillPawnAttacks()
    {
        pawnAttacks = new ulong[64, 2];
        for (int pos = 0; pos < 64; pos++)
        {
            ulong whiteAtt = 0;
            ulong blackAtt = 0;
            if (pos % 8 != 0) //Not on A file
            {
                if (pos / 8 < 7) //Not on 8th rank
                {
                    whiteAtt += (ulong)(Mathf.Pow(2, pos + 7));
                }
                if (pos / 8 > 0) //Not on 1st rank
                {
                    blackAtt += (ulong)(Mathf.Pow(2, pos - 9));
                }
            }
            if (pos % 8 != 7) //Not on H file
            {
                if (pos / 8 < 7) //Not on 8th rank
                {
                    whiteAtt += (ulong)(Mathf.Pow(2, pos + 9));
                }
                if (pos / 8 > 0) //Not on 1st rank
                {
                    blackAtt += (ulong)(Mathf.Pow(2, pos - 7));
                }
            }

            pawnAttacks[pos, 0] = whiteAtt;
            pawnAttacks[pos, 1] = blackAtt;
        }
    }
    private void fillKingAttacks()
    {
        //Brute force ftw
        kingAttacks = new ulong[64];
        for (int pos = 0; pos < 64; pos++)
        {
            ulong att = 0;
            int file = pos % 8;
            int rank = pos / 8;
            if (file != 0)
            {
                att += (ulong)(Mathf.Pow(2, pos - 1));
                if (rank != 0)
                {
                    att += (ulong)(Mathf.Pow(2, pos - 9));
                }
                if (rank != 7)
                {
                    att += (ulong)(Mathf.Pow(2, pos + 7));
                }
            }
            if (file != 7)
            {
                att += (ulong)(Mathf.Pow(2, pos + 1));
                if (rank != 0)
                {
                    att += (ulong)(Mathf.Pow(2, pos - 7));
                }
                if (rank != 7)
                {
                    att += (ulong)(Mathf.Pow(2, pos + 9));
                }
            }
            if (rank != 0)
            {
                att += (ulong)(Mathf.Pow(2, pos - 8));
            }
            if (rank != 7)
            {
                att += (ulong)(Mathf.Pow(2, pos + 8));
            }

            kingAttacks[pos] = att;
        }
    }

}
