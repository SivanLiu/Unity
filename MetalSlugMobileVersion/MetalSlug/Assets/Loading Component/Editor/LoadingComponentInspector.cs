using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof (LoadingComponent))]
public class LoadingComponentInspector : Editor
{
    private string dispName;
    private new string name;
    private SerializedProperty[] properties;
    private SerializedObject so;
    private Rect tooltipRect;
    private List<InspectorPlusVar> vars;

    private void RefreshVars()
    {
        for (int i = 0; i < vars.Count; i += 1) properties[i] = so.FindProperty(vars[i].name);
    }

    private void OnEnable()
    {
        vars = new List<InspectorPlusVar>();
        so = serializedObject;
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Texture2D",
                                      "backgroundImage", "Background Image", InspectorPlusVar.VectorDrawType.None, false,
                                      false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, true, 50,
                                      "", false));
       
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "String", "levelName",
                                      "Level Name", InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                      new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "AudioClip",
                                      "loadingAudio", "Loading Audio", InspectorPlusVar.VectorDrawType.None, false,
                                      false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));

        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Texture2D",
                                      "loadingBarImage", "Loading Bar Image", InspectorPlusVar.VectorDrawType.None,
                                      false, false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, true, 50,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Texture2D",
                                    "emptyBarImage", "Empty Bar Image", InspectorPlusVar.VectorDrawType.None, false,
                                    false, 0, new[] { false, false, false, false }, new[] { "", "", "", "" },
                                    new[] { false, false, false, false }, new[] { false, false, false, false },
                                    new[] { 0, 0, 0, 0 },
                                    new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                    new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                                    new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                                    new[] { false, false, false, false }, 0, "LoadingComponent",
                                    new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, true, 50,
                                    "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Boolean", "normalized",
                                    "Normalized", InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                    new[] { false, false, false, false }, new[] { "", "", "", "" },
                                    new[] { false, false, false, false }, new[] { false, false, false, false },
                                    new[] { 0, 0, 0, 0 },
                                    new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                    new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                                    new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                                    new[] { false, false, false, false }, 0, "LoadingComponent",
                                    new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 1, 0, false, 70,
                                    "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Vector2",
                                      "loadingBarPosition", "Loading Bar Position", InspectorPlusVar.VectorDrawType.None,
                                      false, false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Single",
                              "loadingBarHeight", "Loading Bar Height", InspectorPlusVar.VectorDrawType.None,
                              false, false, 0, new[] { false, false, false, false }, new[] { "", "", "", "" },
                              new[] { false, false, false, false }, new[] { false, false, false, false },
                              new[] { 0, 0, 0, 0 },
                              new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                              new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                              new[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                              new[] { false, false, false, false }, 0, "LoadingComponent",
                              new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                              "", false));
      
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "String", "loadingText",
                                      "Loading Text", InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                      new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "Loading Now...", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Font", "fontType",
                                      "Text Font Type", InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                      new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Int32",
                                      "loadingTextSize", "Text Size", InspectorPlusVar.VectorDrawType.None, false, false,
                                      0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Color",
                                      "loadingTextColor", "Text Color", InspectorPlusVar.VectorDrawType.None, false,
                                      false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "FontStyle",
                                      "loadingTextFont", "Text Font Style", InspectorPlusVar.VectorDrawType.None, false,
                                      false, 0, new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Boolean",
                                      "showProgressPercentage", "Show Progress Percentage",
                                      InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                      new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 1, 0, false, 70,
                                      "", false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None, 0, 0, false, 0, 0, true, "Single", "startDelay",
                                      "Start Delay", InspectorPlusVar.VectorDrawType.None, false, false, 0,
                                      new[] {false, false, false, false}, new[] {"", "", "", ""},
                                      new[] {false, false, false, false}, new[] {false, false, false, false},
                                      new[] {0, 0, 0, 0},
                                      new[]
                                          {
                                              false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false
                                          },
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
                                      new[] {false, false, false, false}, 0, "LoadingComponent",
                                      new Vector3(0.5f, 0.5f, 0f), false, true, "Tooltip", false, false, 0, 0, false, 70,
                                      "", false));
        int count = vars.Count;
        properties = new SerializedProperty[count];
    }

    private void ProgressBar(float value, string label)
    {
        GUILayout.Space(3.0f);
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        GUILayout.Space(3.0f);
    }

    private void Vector2Field(SerializedProperty sp)
    {
        EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);
        EditorGUI.BeginChangeCheck();
        Vector2 newValue = EditorGUILayout.Vector2Field(dispName, sp.vector2Value);

        if (EditorGUI.EndChangeCheck())
            sp.vector2Value = newValue;

        EditorGUI.EndProperty();
    }

    private void FloatField(SerializedProperty sp, InspectorPlusVar v)
    {
        if (v.limitType == InspectorPlusVar.LimitType.Min && !sp.hasMultipleDifferentValues)
            sp.floatValue = Mathf.Max(v.min, sp.floatValue);
        else if (v.limitType == InspectorPlusVar.LimitType.Max && !sp.hasMultipleDifferentValues)
            sp.floatValue = Mathf.Min(v.max, sp.floatValue);

        if (v.limitType == InspectorPlusVar.LimitType.Range)
        {
            if (!v.progressBar)
                EditorGUILayout.Slider(sp, v.min, v.max);
            else
            {
                if (!sp.hasMultipleDifferentValues)
                {
                    sp.floatValue = Mathf.Clamp(sp.floatValue, v.min, v.max);
                    ProgressBar((sp.floatValue - v.min)/v.max, dispName);
                }
                else
                    ProgressBar((sp.floatValue - v.min)/v.max, dispName);
            }
        }
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
    }

    private void IntField(SerializedProperty sp, InspectorPlusVar v)
    {
        if (v.limitType == InspectorPlusVar.LimitType.Min && !sp.hasMultipleDifferentValues)
            sp.intValue = Mathf.Max(v.iMin, sp.intValue);
        else if (v.limitType == InspectorPlusVar.LimitType.Max && !sp.hasMultipleDifferentValues)
            sp.intValue = Mathf.Min(v.iMax, sp.intValue);

        if (v.limitType == InspectorPlusVar.LimitType.Range)
        {
            if (!v.progressBar)
            {
                EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);
                EditorGUI.BeginChangeCheck();

                int newValue = EditorGUI.IntSlider(GUILayoutUtility.GetRect(18.0f, 18.0f), new GUIContent(dispName),
                                                   sp.intValue, v.iMin, v.iMax);

                if (EditorGUI.EndChangeCheck())
                    sp.intValue = newValue;
                EditorGUI.EndProperty();
            }
            else
            {
                if (!sp.hasMultipleDifferentValues)
                {
                    sp.intValue = Mathf.Clamp(sp.intValue, v.iMin, v.iMax);
                    ProgressBar((float) (sp.intValue - v.iMin)/v.iMax, dispName);
                }
                else
                    ProgressBar((float) (sp.intValue - v.iMin)/v.iMax, dispName);
            }
        }
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
    }

    private int BoolField(SerializedProperty sp, InspectorPlusVar v)
    {
        if (v.toggleStart)
        {
            EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);

            EditorGUI.BeginChangeCheck();
            bool newValue = EditorGUILayout.Toggle(dispName, sp.boolValue);

            if (EditorGUI.EndChangeCheck())
                sp.boolValue = newValue;

            EditorGUI.EndProperty();

            if (!sp.boolValue)
                return v.toggleSize;
        }
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));

        return 0;
    }

    private void TextureGUI(SerializedProperty sp, InspectorPlusVar v)
    {
        if (!v.largeTexture)
            PropertyField(sp, name);
        else
        {
            GUILayout.Label(dispName, GUILayout.Width(145.0f));

            EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);

            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            Object newValue = EditorGUILayout.ObjectField(sp.objectReferenceValue, typeof (Texture2D), false,
                                                          GUILayout.Width(v.textureSize),
                                                          GUILayout.Height(v.textureSize));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
                sp.objectReferenceValue = newValue;

            EditorGUI.EndProperty();
        }
    }

    private void TextGUI(SerializedProperty sp, InspectorPlusVar v)
    {
        if (v.textFieldDefault == "")
        {
            PropertyField(sp, name);
            return;
        }

        string focusName = "_focusTextField" + v.name;

        GUI.SetNextControlName(focusName);

        EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);

        EditorGUI.BeginChangeCheck();

        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(dispName);

        string newValue = "";

        if (!v.textArea)
            newValue = EditorGUILayout.TextField("", sp.stringValue, GUILayout.Width(Screen.width));
        else
            newValue = EditorGUILayout.TextArea(sp.stringValue, GUILayout.Width(Screen.width));

        if (GUI.GetNameOfFocusedControl() != focusName && !sp.hasMultipleDifferentValues && sp.stringValue == "")
        {
            GUI.color = new Color(0.7f, 0.7f, 0.7f);
            GUI.Label(GUILayoutUtility.GetLastRect(), v.textFieldDefault);
            GUI.color = Color.white;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
            sp.stringValue = newValue;

        EditorGUI.EndProperty();
    }

    private void PropertyField(SerializedProperty sp, string name)
    {
        if (sp.hasChildren)
        {
            GUILayout.BeginVertical();
            while (true)
            {
                if (sp.propertyPath != name && !sp.propertyPath.StartsWith(name + "."))
                    break;

                EditorGUI.indentLevel = sp.depth;
                bool child = false;

                if (sp.depth == 0)
                    child = EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
                else
                    child = EditorGUILayout.PropertyField(sp);

                if (!sp.NextVisible(child))
                    break;
            }
            EditorGUI.indentLevel = 0;
            GUILayout.EndVertical();
        }
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
    }

    public override void OnInspectorGUI()
    {
        so.Update();
        RefreshVars();

        EditorGUIUtility.LookLikeControls(135.0f, 50.0f);

        for (int i = 0; i < properties.Length; i += 1)
        {
            InspectorPlusVar v = vars[i];

            if (v.active && properties[i] != null)
            {
                SerializedProperty sp = properties[i];
                string s = v.type;
                bool skip = false;
                name = v.name;
                dispName = v.dispName;

                GUI.enabled = v.canWrite;

                GUILayout.BeginHorizontal();

                if (v.toggleLevel != 0)
                    GUILayout.Space(v.toggleLevel*10.0f);

                if (s == typeof (Vector2).Name)
                {
                    Vector2Field(sp);
                    skip = true;
                }
                if (s == typeof (float).Name)
                {
                    FloatField(sp, v);
                    skip = true;
                }
                if (s == typeof (int).Name)
                {
                    IntField(sp, v);
                    skip = true;
                }
                if (s == typeof (bool).Name)
                {
                    i += BoolField(sp, v);
                    skip = true;
                }
                if (s == typeof (Texture2D).Name || s == typeof (Texture).Name)
                {
                    TextureGUI(sp, v);
                    skip = true;
                }
                if (s == typeof (string).Name)
                {
                    TextGUI(sp, v);
                    skip = true;
                }
                if (!skip)
                    PropertyField(sp, name);
                GUILayout.EndHorizontal();
                GUI.enabled = true;
            }
        }
        so.ApplyModifiedProperties();
    }

    public class InspectorPlusVar
    {
        public enum LimitType
        {
            None,
            Max,
            Min,
            Range
        };

        public enum VectorDrawType
        {
            None,
            Direction,
            Point,
            PositionHandle,
            Scale,
            Rotation
        };

        public bool QuaternionHandle;

        public bool active = true;
        public string[] buttonCallback = new string[16];
        public bool[] buttonCondense = new bool[4];
        public bool[] buttonEnabled = new bool[16];
        public string[] buttonText = new string[16];
        public bool canWrite = true;
        public string classType;
        public string dispName;
        public bool hasTooltip = false;
        public int iMax = -0;
        public int iMin = -0;
        public string[] label = new string[4];
        public int[] labelAlign = new int[4];
        public bool[] labelBold = new bool[4];
        public bool[] labelEnabled = new bool[4];
        public bool[] labelItalic = new bool[4];
        public bool largeTexture;
        public LimitType limitType = LimitType.None;
        public float max = -0.0f;
        public float min = -0.0f;
        public string name;

        public int numSpace = 0;
        public Vector3 offset = new Vector3(0.5f, 0.5f);
        public bool progressBar;
        public bool relative = false;
        public bool scale = false;
        public float space = 0.0f;
        public bool textArea;
        public string textFieldDefault;
        public float textureSize;
        public int toggleLevel = 0;
        public int toggleSize = 0;
        public bool toggleStart = false;
        public string tooltip;
        public string type;
        public VectorDrawType vectorDrawType;

        public InspectorPlusVar(LimitType _limitType, float _min, float _max, bool _progressBar, int _iMin, int _iMax,
                                bool _active, string _type, string _name, string _dispName,
                                VectorDrawType _vectorDrawType, bool _relative, bool _scale, float _space,
                                bool[] _labelEnabled, string[] _label, bool[] _labelBold, bool[] _labelItalic,
                                int[] _labelAlign, bool[] _buttonEnabled, string[] _buttonText,
                                string[] _buttonCallback, bool[] buttonCondense, int _numSpace, string _classType,
                                Vector3 _offset, bool _QuaternionHandle, bool _canWrite, string _tooltip,
                                bool _hasTooltip,
                                bool _toggleStart, int _toggleSize, int _toggleLevel, bool _largeTexture,
                                float _textureSize, string _textFieldDefault, bool _textArea)
        {
            limitType = _limitType;
            min = _min;
            max = _max;
            progressBar = _progressBar;
            iMax = _iMax;
            iMin = _iMin;
            active = _active;
            type = _type;
            name = _name;
            dispName = _dispName;
            vectorDrawType = _vectorDrawType;
            relative = _relative;
            scale = _scale;
            space = _space;
            labelEnabled = _labelEnabled;
            label = _label;
            labelBold = _labelBold;
            labelItalic = _labelItalic;
            labelAlign = _labelAlign;
            buttonEnabled = _buttonEnabled;
            buttonText = _buttonText;
            buttonCallback = _buttonCallback;
            numSpace = _numSpace;
            classType = _classType;
            offset = _offset;
            QuaternionHandle = _QuaternionHandle;
            canWrite = _canWrite;
            tooltip = _tooltip;
            hasTooltip = _hasTooltip;
            toggleStart = _toggleStart;
            toggleSize = _toggleSize;
            toggleLevel = _toggleLevel;
            largeTexture = _largeTexture;
            textureSize = _textureSize;
            textFieldDefault = _textFieldDefault;
            textArea = _textArea;
        }
    }
}