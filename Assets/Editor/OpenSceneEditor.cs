using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class OpenSceneEditor : EditorWindow {
   private static string _scenePath = "Assets/MyGame/Scenes/Project/{0}.unity";

   [MenuItem("OpenScene/Loading", false, 1)]
   public static void Menu()
   {
      EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
      EditorSceneManager.OpenScene
         (string.Format(_scenePath, "Loading"), OpenSceneMode.Single);
   }

   [MenuItem("OpenScene/Main", false, 1)]
   public static void Level1()
   {
      EditorSceneManager.SaveScene(SceneManager.GetActiveScene()); 
      EditorSceneManager.OpenScene
         (string.Format(_scenePath, "Main"), OpenSceneMode.Single);
   }
}
