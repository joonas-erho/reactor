using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoBehaviour
{
    public Animator animator;

    private void Start() {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation() {
        yield return new WaitForSeconds(5.0f);
        animator.Play("GuideFade");
    }
}
