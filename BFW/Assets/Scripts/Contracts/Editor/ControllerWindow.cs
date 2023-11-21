using Contracts.Controllers;
using Sirenix.OdinInspector.Editor;
using Unimorph.Injection;
using UnityEditor;
using UnityEngine;

namespace Contracts.Editor
{
    public class ControllerWindow : OdinEditorWindow
    {
        [SerializeField] private TileController tile = new();
        [SerializeField] private TerrainController terrain = new();
        [SerializeField] private UnitController unit = new();
        [SerializeField] private MovementController movement = new();
        [SerializeField] private ArchetypeController archetype = new();
        [SerializeField] private WorldController world = new();

        [MenuItem("BFW/Controller Window")]
        private static void Open()
        {
            var window = GetWindow(typeof(ControllerWindow));

            window.name = "Controller";
        }

        private void OnFocus()
        {
            ManualInject.Into(this);
        }
    }
}