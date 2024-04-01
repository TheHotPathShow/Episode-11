using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class SyncWithPlayerOrbitCamera : MonoBehaviour
{
    public static Camera[] Instance = new Camera[4];
    [Range(0, 3)]
    [SerializeField] int BelongsToPlayer = 0;
    void Awake()
    {
        Instance[BelongsToPlayer] = GetComponent<Camera>();
        gameObject.SetActive(false);
    }
}

[UpdateInGroup(typeof(PresentationSystemGroup))]
public partial class MainCameraSystem : SystemBase
{
    protected override void OnUpdate()
    {
        if (SyncWithPlayerOrbitCamera.Instance == null && SyncWithPlayerOrbitCamera.Instance?.Length != 4)
            return;
        
        foreach (var (orbitCamLtw, orbitCamPlayerID) in SystemAPI.Query<LocalToWorld, PlayerID>().WithAll<OrbitCamera>())
        {
            SyncWithPlayerOrbitCamera.Instance[orbitCamPlayerID.Value]?.transform.SetPositionAndRotation(orbitCamLtw.Position, orbitCamLtw.Rotation);
        }
    }
}