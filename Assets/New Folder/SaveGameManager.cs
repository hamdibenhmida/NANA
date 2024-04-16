using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SaveGameManager : Singleton<SaveGameManager>
{
    public enum LoadType
    {
        /// <summary>
        /// Normal scene loading.
        /// </summary>
        Normal,

        /// <summary>
        /// Load saved game from slot.
        /// </summary>
        LoadGameState,

        /// <summary>
        /// Load world state (Previous Scene Persistency).
        /// </summary>
        LoadWorldState,

        /// <summary>
        /// Load player data.
        /// </summary>
        LoadPlayer
    }

    public static LoadType GameLoadType = LoadType.Normal;
    public static string LoadSceneName;
    public static string LoadFolderName;

    public static Dictionary<string, string> LastSceneSaves;

    
    /// <summary>
    /// Set the load type to load the game state.
    /// <br>The game state and player data will be loaded from a saved game.</br>
    /// </summary>
    /// <param name="sceneName">The name of the scene to be loaded.</param>
    /// <param name="folderName">The name of the saved game folder.</param>
    public static void SetLoadGameState(string sceneName, string folderName)
    {
        GameLoadType = LoadType.LoadGameState;
        LoadSceneName = sceneName;
        LoadFolderName = folderName;
    }

}