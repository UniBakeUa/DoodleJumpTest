using System;
using UnityEngine;
using Zenject;

namespace _Game.Core
{
    public class ModuleLoader : IInitializable
    {
        private readonly DiContainer _container;
        private readonly LoaderSettings _settings;
        
        public event Action OnLoadComplete;

        public ModuleLoader(DiContainer container, LoaderSettings settings)
        {
            _container = container;
            _settings = settings;
        }

        public void Initialize()
        {
            LoadAllModules();
        }

        private void LoadAllModules()
        {
            if (_settings == null || _settings.InstallerPaths == null)
            {
                Debug.LogError("[ModuleLoader] LoaderSettings or InstallerPaths is null!");
                OnLoadComplete?.Invoke();
                return;
            }

            foreach (var path in _settings.InstallerPaths)
            {
                if (string.IsNullOrEmpty(path)) continue;
        
                var installer = Resources.Load<ScriptableObjectInstaller>(path);
        
                if (installer == null)
                {
                    Debug.LogWarning($"[ModuleLoader] Installer not found at: Resources/{path}");
                    continue;
                }

                try
                {
                    // Отримуємо тип самого інсталятора (наприклад, SceneLoaderInstaller)
                    var installerType = installer.GetType();

                    // ПЕРЕВІРКА: чи цей інсталятор вже був застосований?
                    // Zenject зазвичай реєструє самі інсталятори в контейнері після виклику
                    if (_container.HasBinding(installerType))
                    {
                        Debug.LogWarning($"[ModuleLoader] {installerType.Name} вже зареєстрований. Пропускаю, щоб уникнути дублювання.");
                        continue;
                    }

                    _container.Inject(installer);
                    installer.InstallBindings();

                    Debug.Log($"[ModuleLoader] {installerType.Name} installed successfully.");
                }
                catch (Exception e)
                {
                    Debug.LogError($"[ModuleLoader] Error installing {path}: {e.Message}");
                }
            }

            Debug.Log("[ModuleLoader] All modules loaded!");
            OnLoadComplete?.Invoke();
        }
    }
}