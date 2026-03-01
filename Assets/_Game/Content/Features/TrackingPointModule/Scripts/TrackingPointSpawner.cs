using UnityEngine;
using Zenject;

namespace _Game.Content.Features.JumpLogic.Scripts
{
    public class TrackingPointSpawner
    {
        private readonly IInstantiator _instantiator;
        private TrackingPoint _trackingPointPrefab;
        
        public TrackingPointSpawner(TrackingPoint trackingPointPrefab, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _trackingPointPrefab = trackingPointPrefab;
        }
        public TrackingPoint SpawnTracking()
        {
            var sceneContext = GameObject.FindObjectOfType<SceneContext>();
            Transform parentTransform = sceneContext != null ? sceneContext.transform : null;
            
            var PointInstance = _instantiator.InstantiatePrefabForComponent<TrackingPoint>(
                _trackingPointPrefab, 
                Vector3.zero, 
                Quaternion.identity, 
                parentTransform);
            return PointInstance;
        }
    }
}