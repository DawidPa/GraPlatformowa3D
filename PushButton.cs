using UnityEngine;

public class PushButton : MonoBehaviour
{
    public GameObject targetObject; 
    public GameObject buttonModel; 
    public GameObject buttonPressedModel; 

    private bool isPlayerInRange = false; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {

            if (targetObject != null)
            {
   
                targetObject.SetActive(true);
            }
         
            if (buttonModel != null)
            {
                buttonModel.SetActive(false);
                AudioManager.instance.PlaySFX(10);
            }
            if (buttonPressedModel != null)
            {

                buttonPressedModel.SetActive(true);
            }
        }
    }
}
