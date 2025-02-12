using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

namespace Controllers.Scenes
{
    public class InitSceneController : MonoBehaviour
    {
        private void OnEnable()
        {
            LoadMenuScene(SceneType.Menu);
        }

        private void LoadMenuScene(SceneType scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}