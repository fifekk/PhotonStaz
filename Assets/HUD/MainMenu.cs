﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Photon.PunBehaviour {
        static MainMenu instance;
		GameObject ui;
		Button joinGameButton;
 
        void Awake () {
            if(instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
			ui = transform.FindAnyChild<Transform>("UI").gameObject;
			joinGameButton = transform.FindAnyChild<Button>("JoinGameButton");

			ui.SetActive(true);
			joinGameButton.interactable = false;
        }
		public override void OnConnectedToMaster()
		{
			joinGameButton.interactable = true;
		}
		void OnLevelWasLoaded()
		{
			ui.SetActive(!PhotonNetwork.inRoom);
		}
    }
