using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class CopyComponent : EditorWindow
    {
        static Component[] copyComponents;

        [MenuItem("GameObject/Copy Components")]
        static void Copy()
        {
            copyComponents = Selection.activeGameObject.GetComponents<Component>();
        }
        
        [MenuItem("GameObject/Past Components")]
        static void PastComponent()
        {
            foreach (var targetGameObject in Selection.gameObjects)
            {
                if(targetGameObject == null || copyComponents == null) continue;

                foreach (var copyComponent in copyComponents)
                {
                    if (!copyComponent) continue;
                    
                    UnityEditorInternal.ComponentUtility.CopyComponent(copyComponent);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
                }
            }
        }
    }
}