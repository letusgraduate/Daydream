using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCallback : MonoBehaviour
{
    /* ------------- 컴포넌트 변수 ------------- */
    private IAnimCheck animCheck;

    /* -------------- 이벤트 함수 -------------- */
    private void Awake()
    {
        animCheck = GetComponentInParent<IAnimCheck>();
    }

    /* ------------ 애니메이션 호출 ------------ */
    public void AnimEnd()
    {
        animCheck.AnimEnd();
    }

    public void AnimShot()
    {
        animCheck.AnimShot();
    }

    public void AnimSound()
    {
        animCheck.AnimSound();
    }
}
