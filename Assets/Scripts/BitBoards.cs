using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitBoards
{
    BitBoardHelper bbHelper;
    ulong[] occupied; //0 = white, 1 = black (maybe 2 = all, just do combo tho)
    ulong[] pieces; //index of piecetype

    ulong[] attacks;
    ulong[] attacked;
    ulong[] moves;

    public BitBoards()
    {
        bbHelper = new BitBoardHelper();
        occupied = new ulong[3];
        pieces = new ulong[6];
        attacks = new ulong[64];
        attacked = new ulong[64];
        moves = new ulong[64];
    }

    public void createAttacks(ulong occ)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            ulong pieceboard = pieces[i] & occ;

        }
    }
    private ulong generateAttacks(int pos, int pieceType)
    {
        //010 011 110 111
        //000 001 100 101
        ulong att = 0;
        switch(pieceType & 7)
        {
            //Pawn
            case 0:
                int col = pieceType < 8 ? 0 : 1;
                att = bbHelper.pawnAttacks[pos, col];
                break;
            //Knight
            case 1:
                att = bbHelper.knightAttacks[pos];
                break;
            //Bishop
            case 2:
                att = bbHelper.getBishopAttacks(occupied[2], pos);
                break;
            //Rook
            case 3:
                att = bbHelper.getRookAttacks(occupied[2], pos);
                break;
            //King
            case 4:
                att = bbHelper.kingAttacks[pos];
                break;
            //Queen
            case 5:
                att = bbHelper.getBishopAttacks(occupied[2], pos);
                att |= bbHelper.getRookAttacks(occupied[2], pos);
                break;
            default:
                break;
        }

        return att;
    }

}
