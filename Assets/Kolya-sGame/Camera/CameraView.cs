using Kino;
using Kolya_sGame.SceneObjectStorage;
using UnityEngine;

namespace Kolya_sGame.Camera
{
    public class CameraView : SceneObject
    {
        public UnityEngine.Camera Camera => camera;
        public AnalogGlitch AnalogGlitch => analogGlitch;

        [SerializeField] private UnityEngine.Camera camera;
        [SerializeField] private AnalogGlitch analogGlitch;
    }
}