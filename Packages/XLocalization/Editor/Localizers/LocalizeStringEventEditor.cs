using System.Linq;
using UnityEngine;
using UnityEditor;
using XLocalization.Components;


namespace XLocalization.Editor
{
    [CustomEditor(typeof(LocalizeStringEvent))]
    public class LocalizeStringEventEditor : UnityEditor.Editor
    {
        private LocalizeStringEvent _event;
        private SerializedObject _target;
        
        private SerializedProperty localizedReference;
        private SerializedProperty updateString;
        
        private void OnEnable()
        {
            _event = (LocalizeStringEvent)target;
            _target = new SerializedObject(target);
            
            localizedReference = _target.FindProperty("LocalizedReference");
            updateString = _target.FindProperty("UpdateString");
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
                EditorGUILayout.Space();
                _event.UseArguments = EditorGUILayout.Toggle("UseArguments", _event.UseArguments);
                if (_event.UseArguments)
                {
                    for (var i = 0; i < _event.Arguments.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        _event.Arguments[i] = EditorGUILayout.TextField(_event.Arguments[i]);
                        if (GUILayout.Button("-", GUILayout.Width(20)))
                        {
                            _event.Arguments.RemoveAt(i);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    if (GUILayout.Button("+"))
                    {
                        _event.Arguments.Add("");
                    }
                }
                EditorGUILayout.Space();
            }
            EditorGUILayout.PropertyField(updateString);
            
            _target.ApplyModifiedProperties();
        }
    }
}
