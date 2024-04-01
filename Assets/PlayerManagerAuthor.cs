using Unity.Entities;
using UnityEngine;

public class PlayerManagerAuthor : MonoBehaviour
{
    [SerializeField] GameObject[] SpawnOnPlayerSpawn;

    class PlayerManagerAuthorBaker : Baker<PlayerManagerAuthor>
    {
        public override void Bake(PlayerManagerAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            var buffer = AddBuffer<SpawnOnPlayerSpawn>(entity);
            foreach (var prefab in authoring.SpawnOnPlayerSpawn)
            {
                buffer.Add(new SpawnOnPlayerSpawn
                {
                    Prefab = GetEntity(prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}

public struct SpawnOnPlayerSpawn : IBufferElementData
{
    public Entity Prefab;
}
