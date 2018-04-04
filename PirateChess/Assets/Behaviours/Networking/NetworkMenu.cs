using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMenu : MonoBehaviour {

    NetworkManager m_networkManager;

    private void Start()
    {
        m_networkManager = GetComponent<NetworkManager>();
    }

    public void enterMatchmaking()
    {
        if(m_networkManager.matchMaker == null)
            m_networkManager.StartMatchMaker();

        if (m_networkManager.matchInfo == null)
        {
            if (m_networkManager.matches == null)
                m_networkManager.matchMaker.CreateMatch(m_networkManager.matchName, m_networkManager.matchSize, true, "", "", "", 0, 0, m_networkManager.OnMatchCreate);
            else
            {
                foreach (var match in m_networkManager.matches)
                {
                    if(match.currentSize != match.maxSize)
                    {
                        m_networkManager.matchName = match.name;
                        m_networkManager.matchSize = (uint)match.currentSize;
                        m_networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, m_networkManager.OnMatchJoined);
                    }
                }
            }
        }
    }
}
