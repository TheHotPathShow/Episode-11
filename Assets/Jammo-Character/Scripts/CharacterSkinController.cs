using UnityEngine;

public class CharacterSkinController : MonoBehaviour, IAddMonoBehaviourToEntityOnAnimatorInstantiation
{
    Animator animator;
    Renderer[] characterMaterials;

    // Maps the index to the corresponding material and eye color
    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    
    // Enum to control the eye offset
    public enum EyePosition { normal, happy, angry, dead}
    
    static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void ChangeMaterialSettings(int index)
    {
        if (characterMaterials == null)
            Start();
        
        foreach (var r in characterMaterials)
        {
            if (r.transform.CompareTag("PlayerEyes"))
                r.material.SetColor(EmissionColor, eyeColors[index]);
            else
                r.material.SetTexture(BaseMap, albedoList[index]);
        }
    }

    public void ChangeEyeOffset(EyePosition pos)
    {
        var offset = pos switch
        {
            EyePosition.normal => new Vector2(0, 0),
            EyePosition.happy => new Vector2(.33f, 0),
            EyePosition.angry => new Vector2(.66f, 0),
            EyePosition.dead => new Vector2(.33f, .66f),
            _ => Vector2.zero
        };

        foreach (var r in characterMaterials)
        {
            if (r.transform.CompareTag("PlayerEyes"))
                r.material.SetTextureOffset(BaseMap, offset);
        }
    }
}
