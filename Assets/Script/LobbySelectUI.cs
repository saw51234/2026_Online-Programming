using UnityEngine;

public class LobbySelectUI : MonoBehaviour
{
    private PlayerLobbyData localLobbyData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (localLobbyData != null) return;

        PlayerLobbyData[] lobbyDatas = FindObjectsByType<PlayerLobbyData>(FindObjectsSortMode.None);

        foreach (var data in lobbyDatas)
        {
            if(data.Object != null && data.Object.HasInputAuthority)
            {
                localLobbyData = data;
                Debug.Log("내 로비 데이터 찾음");
                break;
            }
        }
    }

    public void SelectCharacter0()
    {
        if (localLobbyData == null) return;
        localLobbyData.RPC_SelectCharacter(0);
    }

    public void SelectCharacter1()
    {
        if (localLobbyData == null) return;
        localLobbyData.RPC_SelectCharacter(1);
    }

    public void SelectRedTeam()
    {
        if (localLobbyData == null) return;
        localLobbyData.RPC_SelectTeam(0);
    }

    public void SelectBlueTeam()
    {
        if (localLobbyData == null) return;
        localLobbyData.RPC_SelectTeam(1);
    }

    public void Ready()
    {
        if(localLobbyData == null) return;
        localLobbyData.RPC_SetReady(true);
    }
}
