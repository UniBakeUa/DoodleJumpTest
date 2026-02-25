using System.Collections.Generic;
using UnityEngine;

namespace _Game.Core
{
    [CreateAssetMenu(menuName = "Game/LoaderSettingsSettings", fileName = "LoaderSettingsSettings")]
    public class LoaderSettings : ScriptableObject
    {
        public List<string> InstallerPaths = new();
    }
}