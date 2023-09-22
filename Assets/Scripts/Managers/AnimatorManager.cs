using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetup;
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEATH
    }

    public void PlayAnimation(AnimationType type)
    {
        foreach (var animation in animatorSetup)
        {
            if(animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                break;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayAnimation(AnimationType.IDLE);
        }else if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            PlayAnimation(AnimationType.RUN);

        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            PlayAnimation(AnimationType.DEATH);

        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
}
