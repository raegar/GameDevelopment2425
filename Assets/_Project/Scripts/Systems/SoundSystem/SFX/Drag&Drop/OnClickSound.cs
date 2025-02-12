using UnityEngine;

public class OnClickSound : DragDropSound
{
    // Used for sounds that need to play when the attached object is clicked
    // Example: viking grunt

    [SerializeField] private float cooldownTime = 0.1f; // Adjust as needed
    [SerializeField] private float cooldownPassed = 0;
    private bool onCooldown = false;

    private void OnMouseDown()
    {
        if (!onCooldown)
        {
            soundReceiver.SoundBehaviour(clipIndexToPlay, false);
            onCooldown = true;
        }
    }

    private void FixedUpdate()
    {
        if (onCooldown)
        {
            cooldownPassed += Time.deltaTime;
            if (cooldownPassed >= cooldownTime)
            {
                cooldownPassed = 0;
                onCooldown = false;
            }
        }
    }
}
