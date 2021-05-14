using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
using UnityEditor;
using UnityEditor.UI;
*/

public class UIText : Text
{
    [SerializeField] private bool m_DisableWordWrap;

    public override string text
    {
        get => base.text;
        set
        {
            if (m_DisableWordWrap)
            {
                string nsbp = value.Replace(' ', '\u00A0');
                base.text = nsbp;
                return;
            }
            base.text = value;
        }
    }
}
/*
[CustomEditor(typeof(UIText))]
public class UITextInspector : UnityEditor.UI.TextEditor
{
    private SerializedProperty m_DisableWordWrap;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_DisableWordWrap = serializedObject.FindProperty("m_DisableWordWrap");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(m_DisableWordWrap);

        serializedObject.ApplyModifiedProperties();
    }

}*/
