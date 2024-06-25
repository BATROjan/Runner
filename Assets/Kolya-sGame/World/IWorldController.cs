using System.Collections;
using System.Collections.Generic;
using Kolya_sGame.Buff;
using Kolya_sGame.World;
using UnityEngine;

public interface IWorldController
{
    void StartSpawn(WorldName name);
    void StartMove();
    void StopMove();
    void ChangeWorld();
    void DespawnAll();
}
