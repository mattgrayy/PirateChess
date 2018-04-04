using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientManager : NetworkBehaviour {

    NetworkIdentity m_networkID;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_networkID = GetComponent<NetworkIdentity>();
    }

    void Update ()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
            CmdFlipTile(8, m_networkID);
	}

    [Command]
    void CmdFlipTile(int _tileIndex, NetworkIdentity _identity)
    {
        RpcFlipTile(_tileIndex, _identity);
    }

    [ClientRpc]
    void RpcFlipTile(int _tileIndex, NetworkIdentity _identity)
    {
        if (!_identity.isLocalPlayer)
            TileGrid.Instance.flipTile(_tileIndex, true);
        else
            TileGrid.Instance.flipTile(_tileIndex);
    }
}
