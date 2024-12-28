using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class CopyComponent : EditorWindow
    {
        private static Component[] _copyComponents;

        [MenuItem("GameObject/Copy Components")]
        static void Copy()
        {
            _copyComponents = Selection.activeGameObject.GetComponents<Component>();
        }
        
        [MenuItem("GameObject/Past Components")]
        static void PastComponent()
        {
            foreach (var targetGameObject in Selection.gameObjects)
            {
                if(targetGameObject == null || _copyComponents == null) continue;

                foreach (var copyComponent in _copyComponents)
                {
                    if (!copyComponent) continue;
                    
                    UnityEditorInternal.ComponentUtility.CopyComponent(copyComponent);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
                }
            }
        }
    }
}