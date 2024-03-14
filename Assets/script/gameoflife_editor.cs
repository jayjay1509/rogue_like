using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(gameoflife_generetor))]

    public class gameoflife_editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

             gameoflife_generetor gen  = (gameoflife_generetor)target;
             
             if (GUILayout.Button("final"))
             {
                 gen.Clear();
                 gen.Generate_Final();
             }
             if (GUILayout.Button("generate but no merge"))
             {
                 gen.Generate();
             }
             
             if (GUILayout.Button("clear"))
             {
                 gen.Clear();
             }
             if (GUILayout.Button("stone"))
             {
                 gen.generatestone();
             }
             if (GUILayout.Button("mineral"))
             {
                 gen.generateminerale();
             }
             if (GUILayout.Button("granit"))
             {
                 gen.generateGranit();
             }
             if (GUILayout.Button("air"))
             {
                 gen.generateNull();
             }
            
             if (GUILayout.Button("merge"))
             {
                 gen.MergeMap();
             }
             if (GUILayout.Button("active"))
             {
                 gen.active();
             }
             if (GUILayout.Button("deactive"))
             {
                 gen.Deactive();
             }
        }
    }
