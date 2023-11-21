using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Unimorph.Injection
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class EditorContext
    {
        /// <summary>
        /// Determine whether to use the existing ProjectContext container if the game is running
        /// or a custom instance if in edit mode.
        /// </summary>
        public static DiContainer Container
            => ProjectContext.HasInstance ? ProjectContext.Instance.Container : TemporaryContainer;

        /// <summary>
        /// A temporary container used in edit time.
        /// Separation of the container ensures it doesn't affect the standard flow. 
        /// </summary>
        private static DiContainer TemporaryContainer
            => _temporaryContainer ??= new DiContainer(StaticContext.Container);

        private static DiContainer _temporaryContainer;
        
        static EditorContext()
        {
            Install();
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/Unimorph/Reinstall Editor Context")]
#endif
        private static void Reinstall()
        {
            _temporaryContainer = null;

            if (TryGetInstallers(out var installers))
            {
                Install(installers);

                Debug.Log("Editor context reinstalled.");
            }
            else
            {
                Debug.LogError("ProjectContext not found, cannot reinstall editor context.");   
            }
        }

        private static void Install()
        {
            TryGetInstallers(out var installers);

            Install(installers);
        }

        /// <summary>
        /// Taking registered installers from ProjectContext,
        /// since Zenject already has a mechanism in place for loading it.
        /// </summary>
        private static bool TryGetInstallers(out IEnumerable<IInstaller> installers)
        {
            installers = Enumerable.Empty<IInstaller>();
            
            var projectContextPrefab = ProjectContext.TryGetPrefab();

            if (!projectContextPrefab)
            {
                return false;
            }

            var projectContext = projectContextPrefab.GetComponent<ProjectContext>();

            if (!projectContext)
            {
                return false;
            }
            
            installers = Enumerable.Empty<IInstaller>()
                .Union(projectContext.ScriptableObjectInstallers)
                .Union(projectContext.Installers)
                .Union(projectContext.NormalInstallers);

            return true;
        }
        
        private static void Install(IEnumerable<IInstaller> installers)
        {
            foreach (var installer in installers)
            {
                TemporaryContainer.Inject(installer);

                installer.InstallBindings();
            }
        }
    }
}