using Kolya_sGame.Obstacle;
using UnityEngine;

namespace Kolya_sGame.Buff
{
    public class BuffController
    {
        private readonly ObstacleAnimationController obstacleAnimationController;
        private readonly BuffView.Pool _buffPool;
        private readonly BuffConfig _buffConfig;

        public BuffController(
            ObstacleAnimationController obstacleAnimationController,
            BuffView.Pool buffPool,
            BuffConfig buffConfig)
        {
            this.obstacleAnimationController = obstacleAnimationController;
            _buffPool = buffPool;
            _buffConfig = buffConfig;
        }

        public void DespawnBuff(BuffView buff)
        {
            _buffPool.Despawn(buff);
            obstacleAnimationController.DeleteAnimation(buff.GetInstanceID());
        }

        public BuffView SpawnBuff(
            BuffName buffName,
            Transform transform
            )
        {
            var buff = _buffPool.Spawn(_buffConfig.GetBuffModelByName(buffName));
            obstacleAnimationController.AddStarAnimation(buff);
            buff.gameObject.transform.SetParent(transform, false);
            return buff;
        }
        
    }
}