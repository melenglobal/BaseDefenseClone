using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorScripts
{
   [CustomEditor(typeof(GridSystem))]
   public class GridEditor : Editor
   {
      private GridSystem gridSystem;
      

      #region Event Subscribetions
      
      private void OnEnable()
      {
         //gridSystem = (GridSystem)target;
         //SubscribeEvents();
      }

      private void SubscribeEvents()
      {
         //SceneView.onSceneGUIDelegate = OnGridUpdate;
         //SceneView.beforeSceneGui += OnGridUpdate;
         //SceneView.duringSceneGui += OnGridUpdate;
      }

      private void UnsubscribeEvents()
      {  
         //SceneView.onSceneGUIDelegate -= OnGridUpdate;
         //SceneView.beforeSceneGui -= OnGridUpdate;
         //SceneView.duringSceneGui -= OnGridUpdate;
      }
      private void OnDisable()
      {
         //UnsubscribeEvents();
      }
      
      #endregion
  

      void OnGridUpdate(SceneView sceneView)
      {
         Event e = Event.current;
 
         if (e.isKey && e.character == 'c')
         {
            CreateSelectedObjectAndAligned(GetPositionToSpawn(e));
         }
         else if (e.isKey && e.character =='d')
         {
           // DestroyAllSelectedObject();
         }
      }

      private Vector3 GetPositionToSpawn(Event e)
      {
         Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
         
         return  r.origin;
      }
      private void CreateSelectedObjectAndAligned(Vector3 mousePos)
      {
         GameObject obj;
         Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeObject);
         //PrefabUtility.GetCorrespondingObjectFromSource
         
         if (prefab)
         {
            obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x/gridSystem.width)*gridSystem.width + gridSystem.width/2.0f,
               Mathf.Floor(mousePos.y/gridSystem.height)*gridSystem.height + gridSystem.height/2.0f, 0.0f);
            obj.transform.position = aligned;
            gridSystem.PlacablePositions.Add(obj.transform);

         }
      }
      // private void DestroyAllSelectedObject()
      // {
      //    foreach (GameObject gameObject in Selection.gameObjects)
      //    {
      //       DestroyImmediate(gameObject);
      //       
      //    }
      // }
      public override void OnInspectorGUI()
      {
         SetGridWithToTheInspector();
         
         SetGridHeightToTheInspector();
         
         SceneView.RepaintAll();
      }
      
      private void SetGridWithToTheInspector()
      {
         GUILayout.BeginHorizontal();
         GUILayout.Label(" Grid Width ");
         gridSystem.width = EditorGUILayout.FloatField(gridSystem.width, GUILayout.Width(50));
         GUILayout.EndHorizontal();
      }

      private void SetGridHeightToTheInspector()
      {
         GUILayout.BeginHorizontal();
         GUILayout.Label(" Grid Height ");
         gridSystem.height = EditorGUILayout.FloatField(gridSystem.height, GUILayout.Width(50));
         GUILayout.EndHorizontal();
      }

   }
}
