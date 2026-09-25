using System.Linq;
using UnityEngine;
using UnityEditor;
using XLocalization.Components;


namespace XLocalization.Editor
{
    [CustomEditor(typeof(LocalizeGameObjectEvent))]
    public class LocalizeGameObjectEventEditor : UnityEditor.Editor
    {
        private LocalizeGameObjectEvent _event;
        private SerializedObject _target;
        
        private SerializedProperty localizedReference;
        
        private void OnEnable()
        {
            _event = (LocalizeGameObjectEvent)target;
            _target = new SerializedObject(target);
            
            localizedReference = _target.FindProperty("LocalizedReference");
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
            
            _target.ApplyModifiedProperties();
        }
    }
}
