using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    interface GameState
    {
        void ChangePlayerSettings(PlayerController playerController);
        void ShowUI(UIManager uiManager);
        void ChangeScene(SceneController sceneController);
        string GetState();
    }

    class GamePlayState : GameState
    {
        public void ChangePlayerSettings(PlayerController playerController)
        {
            playerController.enabled = true;
            Time.timeScale = 1.0f;
        }
        
        public void ShowUI(UIManager uiManager)
        {
            uiManager.ShowGamePlayUI();
        }

        public void ChangeScene(SceneController sceneController)
        {
            // Nothing to do
        }

        public string GetState()
        {
            return "GamePlay";
        }
    }

    class GamePauseState : GameState
    {
        public void ChangePlayerSettings(PlayerController playerController)
        {
            Time.timeScale = 0.0f;  // Time paused
        }
        
        public void ShowUI(UIManager uiManager)
        {
            uiManager.ShowGamePauseUI();
        }

        public void ChangeScene(SceneController sceneController)
        {
            // Nothing to do
        }

        public string GetState()
        {
            return "GamePause";
        }
    }

    class GameOverState : GameState
    {
        public void ChangePlayerSettings(PlayerController playerController)
        {
            playerController.enabled = false;  // No movement allowed
            playerController.RemoveEffects();
        }
        
        public void ShowUI(UIManager uiManager)
        {
            uiManager.ShowGameOverUI();
        }

        public void ChangeScene(SceneController sceneController)
        {
            // Nothing to do
        }

        public string GetState()
        {
            return "GameOver";
        }
    }

    class StageClearState : GameState
    {
        public void ChangePlayerSettings(PlayerController playerController)
        {
            playerController.enabled = false;  // No movement allowed
        }
        
        public void ShowUI(UIManager uiManager)
        {
            uiManager.ShowStageClearUI();
        }

        public void ChangeScene(SceneController sceneController)
        {
            sceneController.ChangeScene();
        }

        public string GetState()
        {
            return "StageClear";
        }
    }

    // Context 
    class GameStateStrategy
    {
        private GameState gameState = new GamePlayState();

        public void SetState(GameState newGameState)
        {
            gameState = newGameState;
        }

        // Player Controller
        public void ChangePlayerSettings(PlayerController playerController)
        {
            gameState.ChangePlayerSettings(playerController);
        }
        
        // UI Manager
        public void ShowUI(UIManager uiManager)
        {
            gameState.ShowUI(uiManager);
        }

        // Scene Controller
        public void ChangeScene(SceneController sceneController)
        {
            gameState.ChangeScene(sceneController);
        }

        public string GetState()
        {
            return gameState.GetState();
        }
    }
}