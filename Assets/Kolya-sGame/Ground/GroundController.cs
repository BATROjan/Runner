using Kolya_sGame.World;

namespace Kolya_sGame.Ground
{
    public class GroundController
    {
        private readonly GroundConfig _groundConfig;
        private readonly GroundView.Pool _groundPool;
        
        private GroundView _groundView;
        
        public GroundController(
            GroundConfig groundConfig,
            GroundView.Pool groundPool)
        {
            _groundConfig = groundConfig;
            _groundPool = groundPool;
        }

        public void Spawn(WorldName name)
        {
          var view =  _groundPool.Spawn();
          view.transform.position = _groundConfig.GetPositionByName(name);
          _groundView = view;
        }

        public void Despawn()
        {
            _groundPool.Despawn(_groundView);
            _groundView = null;
        }
    }
}