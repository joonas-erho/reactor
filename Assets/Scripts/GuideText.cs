/// <summary>
/// Guide Text
/// Joonas Erho, 12.6.2022
/// 
/// Includes fade-away animation control for the guide text visible on the
/// gameplay screen.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoBehaviour
{
    public Animator animator;

    private readonly float waitTime = 5.0f;

    // We start this coroutine, and wait for a while before launching the
    // fadeaway animation.
    private void Start() {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation() {
        yield return new WaitForSeconds(waitTime);
        animator.Play("GuideFade");
    }
}
