using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour {
        public static GameManager instance;
        public static GameObject localPlayer;
 
        void Awake()
        {
            if(instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
            PhotonNetwork.automaticallySyncScene = true;
        }
		void Start()
        {
            PhotonNetwork.ConnectUsingSettings("PhotonStaz");
        }
		public void JoinGame()
        {
            RoomOptions ro = new RoomOptions();
            ro.maxPlayers = 6;
            PhotonNetwork.JoinOrCreateRoom("Default Room",  ro, null);
        }
        public override void OnJoinedRoom(){
            Debug.Log("Joined room");
             if(PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("Game Scene");
            }
        }
        void OnLevelWasLoaded(int levelNumber)
        {
            if(!PhotonNetwork.inRoom) return;
 
            localPlayer = PhotonNetwork.Instantiate(
            "Player",
            new Vector3(0,0.5f,0),
            Quaternion.identity, 0);
        }
        public static List<GameObject> GetAllObjectsOfTypeInScene<T>()
        {
            List<GameObject> objectsInScene = new List<GameObject>();
 
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject))
                     as GameObject[])
            {
                if (go.hideFlags == HideFlags.NotEditable ||
                    go.hideFlags == HideFlags.HideAndDontSave)
                    continue;
 
                if (go.GetComponent<T>() != null)
                    objectsInScene.Add(go);      
            }
 
            return objectsInScene;
        }
}
	

