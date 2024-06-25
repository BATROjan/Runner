using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MaskView :  MonoBehaviour, IPoolable<MaskProtocol, IMemoryPool>
{
    public void OnDespawned()
    {
    }

    public void OnSpawned(MaskProtocol protocol, IMemoryPool p2)
    {
        transform.position = protocol.StartPosition;
        transform.rotation = Quaternion.identity;
    }
    
    private void ReInit(MaskProtocol protocol)
    {
        transform.position = protocol.StartPosition;
        transform.rotation = Quaternion.identity;
    }
    
    public class Pool : MonoMemoryPool<MaskProtocol, MaskView>
    {
        protected override void Reinitialize(MaskProtocol protocol, MaskView item)
        {
            item.ReInit(protocol);
            item.OnSpawned(protocol, this);
        }

        protected override void OnDespawned(MaskView item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}

[Serializable]
public struct MaskProtocol
{
    public Vector3 StartPosition;
    public MaskProtocol(
        Vector3 startPosition)
    {
        StartPosition = startPosition;
    }
}
