using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Core
{
    public class ModuleLoader : MonoBehaviour, IInitializable
    {
        public event Action OnLoadComplete;
        [field:SerializeField] public LoaderType loaderType { get; private set; }

        [SerializeField] private List<string> installerPaths = new();
        private static readonly HashSet<Type> LoadedInstallers = new();
        private DiContainer _container;
        
        public enum LoaderType
        {
            Bootstrap = 0,
            MainMenu = 1,
            Gameplay = 2
        }
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
            LoadAllModules();
        }
        public void Initialize()
        {
            OnLoadComplete?.Invoke();
        }
        private void LoadAllModules()
        {
            foreach (var path in installerPaths)
            {
                if (string.IsNullOrEmpty(path)) continue;

                var installer = Resources.Load<ScriptableObjectInstaller>(path);
                if (installer == null) continue;

                Type type = installer.GetType();
                if (LoadedInstallers.Contains(type)) continue;

                try
                {
                    _container.BindInterfacesAndSelfTo(type).FromInstance(installer).AsSingle();
                    
                    if (installer is IInstaller installerInterface)
                    {
                        _container.Inject(installer); 
                        installerInterface.InstallBindings();
                    }

                    LoadedInstallers.Add(type);
                    Debug.Log($"[ModuleLoader] {type.Name} loaded!");
                    
                }
                catch (Exception e)
                {
                    Debug.LogError($"[ModuleLoader] Error: {e.Message}");
                }
            }
        }

       
    }
}