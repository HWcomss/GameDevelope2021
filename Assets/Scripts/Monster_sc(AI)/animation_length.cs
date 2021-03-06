using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_length : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public float ShortAttack;
    public float LongAttack;
    public float Block;
    public float Run;
    public float BackStep;
    public float JumpAttack;
    public float MidMagic1;



    private Animator anim;
    private AnimationClip clip;

    public AnimationClip[] clips;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Error: Did not find anim!");
        }
        else
        {
            //Debug.Log("Got anim");
        }

        UpdateAnimClipTimes();
    }
    public void UpdateAnimClipTimes()
    {
        clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "ShortAttack":
                    ShortAttack = clip.length;
                    break;
                case "LongAttack":
                    LongAttack = clip.length;
                    break;
                case "sRange_BackStep":
                    BackStep = clip.length;
                    break;
                case "MidJumpAttack":
                    JumpAttack = clip.length;
                    break;
                case "MidMagic1":
                    MidMagic1 = clip.length;
                    break;
                case "Block":
                    Block = clip.length;
                    break;
                case "Run_inPlace":
                    Run = clip.length;
                    break;

            }
        }
    }
}
