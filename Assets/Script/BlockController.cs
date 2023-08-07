using UnityEngine;

public class BlockController : MonoBehaviour
{
    public bool isHolded = false;

    public delegate void BlockHeldHandler(Transform blockTransform);
    public static event BlockHeldHandler OnBlockHeld;

    public void HoldBlock()
    {
        isHolded = true;
        // Notify the event subscribers that this block is held
        if (OnBlockHeld != null)
            OnBlockHeld(transform);
    }
}
