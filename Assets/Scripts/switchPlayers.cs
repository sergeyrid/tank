using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchPlayers : MonoBehaviour
{
    public GameObject player0;
    public GameObject player1;

    public void switcher ()
    {
        if (player0.GetComponent<PVPcontrol>().activePlayer == true)
        {
            player0.GetComponent<PVPcontrol>().activePlayer = false;
            player1.GetComponent<PVPcontrol>().activePlayer = true;
        }
        else
        {
            player0.GetComponent<PVPcontrol>().activePlayer = true;
            player1.GetComponent<PVPcontrol>().activePlayer = false;
        }
        // for (int i = 0; i < 1000000; i++);
    }
}
