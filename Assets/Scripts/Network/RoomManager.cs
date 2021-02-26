using UnityEngine;
using System.IO;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public Transform allPlayersStartHere;
    public GameObject playerController;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), allPlayersStartHere.position, Quaternion.identity);

        GameObject newPlayer = Instantiate(playerController, allPlayersStartHere.position, Quaternion.identity);
        newPlayer.SetActive(true);
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public void OnClick()
    {
        OnEnable();
    }    
}
