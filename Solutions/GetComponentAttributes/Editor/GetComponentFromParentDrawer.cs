using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(GetComponentFromParentAttribute))]
public class GetComponentFromParentDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		PropertyDrawerHelper.SetComponentFromSource(property, go => go.transform.parent?.GetComponent(fieldInfo.FieldType));
		EditorGUI.PropertyField(position, property, label);
	}
}
