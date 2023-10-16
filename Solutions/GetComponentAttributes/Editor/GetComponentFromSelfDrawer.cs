using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(GetComponentFromSelfAttribute))]
public class GetComponentFromSelfDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		PropertyDrawerHelper.SetComponentFromSource(property, go => go.GetComponent(fieldInfo.FieldType));
		EditorGUI.PropertyField(position, property, label);
	}
}
