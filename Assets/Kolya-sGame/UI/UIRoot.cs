using Kolya_sGame.SceneObjectStorage;
using UnityEngine;
using VGUIService;

namespace Kolya_sGame.UI
{
    public class UIRoot : SceneObject, IUIRoot
    {
        public UnityEngine.Camera Camera { get; set; }
        
        public LayerContainer Container => container;

        public Transform PoolContainer => poolContainer;
        
        public Canvas Canvas
        {
            get => canvas;
            set => canvas = value;
        }
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private LayerContainer container;
        [SerializeField] private Transform poolContainer;
    }
}