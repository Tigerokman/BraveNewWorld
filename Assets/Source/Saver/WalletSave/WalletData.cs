using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WalletData
{
    public int Gold { get; private set; }
    public int Crystals { get; private set; }

    public WalletData (PlayerWallet wallet)
    {
        Gold = wallet.Gold;
        Crystals = wallet.Crystals;
    }
}