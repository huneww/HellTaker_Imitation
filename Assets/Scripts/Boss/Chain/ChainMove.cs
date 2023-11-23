using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMove : MonoBehaviour
{
    public void ChainMoveSound()
    {
        JGBossAudioManager.Instance.BindingChainMove();
    }
}
