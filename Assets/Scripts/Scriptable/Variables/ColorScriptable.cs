using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Variables/Color")]
public class ColorScriptable : ScriptableObject 
{
	[ColorUsageAttribute(true,true)]
	public Color value;
}
