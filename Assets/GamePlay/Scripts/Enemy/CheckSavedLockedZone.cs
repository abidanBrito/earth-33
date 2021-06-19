using UnityEngine;

public class CheckSavedLockedZone : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            if (GameManager.Instance.savedLocked == false)
            {
                // Debug.Log("Player se mantiene en SavedLockedZone");
                GameManager.Instance.savedLocked = true;
                // Debug.Log("savedLocked value: " + GameManager.Instance.savedLocked);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            // Debug.Log("Player sale en SavedLockedZone");
            GameManager.Instance.savedLocked = false;
            // Debug.Log("savedLocked value: " + GameManager.Instance.savedLocked);
        }
    }

}
