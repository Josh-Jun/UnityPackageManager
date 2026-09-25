using System.Linq;
using UnityEngine;
using UnityEditor;
using XLocalization.Components;


namespace XLocalization.Editor
{
    [CustomEditor(typeof(LocalizeTextureEvent))]
    public class LocalizeTextureEventEditor : UnityEditor.Editor
    {
        private LocalizeTextureEvent _event;
        private SerializedObject _target;
        
        private SerializedProperty localizedReference;
        private SerializedProperty updateAsset;
        
        private void OnEnable()
        {
            _event = (LocalizeTextureEvent)target;
            _target = new SerializedObject(target);
            
            localizedReference = _target.FindProperty("LocalizedReference");
            updateAsset = _target.FindProperty("UpdateAsset");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Undo.RecordObject(_event, "object change");
            
            EditorGUILayout.PropertyField(localizedReference);
            if (_event.LocalizedReference != null)
            {
                var array = _event.LocalizedReference.TableDatas.Select(data => data.Key).ToArray();
                var index = Mathf.Max(0, System.Array.IndexOf(array, _event.Key));
                var new_index = EditorGUILayout.Popup(" ", index, array);
                if (new_index != index)
                {
                    _event.SetKey(array[new_index]);
                }
            }
            EditorGUILayout.PropertyField(updateAsset);
            
            _target.ApplyModifiedProperties();
        }
    }
}
