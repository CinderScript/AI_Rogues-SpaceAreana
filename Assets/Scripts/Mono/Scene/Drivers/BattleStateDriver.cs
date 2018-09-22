﻿using AIRogue.GameState;

using UnityEngine;

namespace AIRogue.Scene
{

	/// <summary>
	/// Each Driver script is the entry point in the current scene into the Unity framework.  
	/// The driver classes get the current instance of the GameStateManager and run that state 
	/// manager's current state in Unity's Update loop.
	/// 
	/// </summary>
	class BattleStateDriver : MonoBehaviour {

        private GameStateManager game;

        void Awake()
        {
            game = GameStateManager.Instance();
        }

        void Start()
        {
			UnitBank units = GetComponentInChildren<UnitBank>();
			WeaponBank weaponBank = GetComponentInChildren<WeaponBank>();
			LevelProperties levelProperties = GetComponentInChildren<LevelProperties>();

            game.LoadBattleState( units, weaponBank, levelProperties );
        }

        void FixedUpdate()
        {
            game.FixedUpdateGame();
        }
        void Update()
        {
            game.UpdateGame();
        }
    }
}