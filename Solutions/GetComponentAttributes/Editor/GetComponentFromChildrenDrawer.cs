using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(GetComponentFromChildrenAttribute))]
public class GetComponentFromChildrenDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		PropertyDrawerHelper.SetComponentFromSource(property, go => go.GetComponentInChildren(fieldInfo.FieldType));
		EditorGUI.PropertyField(position, property, label);
	}
}
