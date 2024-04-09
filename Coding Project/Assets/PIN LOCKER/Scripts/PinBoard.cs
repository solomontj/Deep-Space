using UnityEngine;
using System.Collections;
using TMPro;

namespace PinLocker
{
    public class PinBoard : MonoBehaviour
    {
        [SerializeField] private TMP_Text pinText;

        public int PIN = 0000;

        private int i = 0;

        [SerializeField] private AudioSource pinEnterAudio;
        [SerializeField] private AudioSource pinAcceptedAudio;
        [SerializeField] private AudioSource pinRejectedAudio;

        [SerializeField] private GameObject ExampleUnlockText;

        [SerializeField] private DoorBehaviour door;
        [SerializeField] private GameObject pinLockerUI;

        public void PinEnter(string pin) // PIN ENTER SYSTEM
        {
            if (pin == "back")
            {
                if (i > 0)
                {
                    pinEnterAudio.Play();

                    pinText.text = pinText.text.Substring(0, pinText.text.Length - 1);

                    i--;
                }
            }
            else
            {
                if (i < 4)
                {
                    pinEnterAudio.Play();

                    pinText.text += pin;

                    i++;

                    if (i == 4)
                    {
                        CheckPIN();
                    }
                }
            }
        }

        public void CheckPIN()
        {
            if (PIN.ToString() == pinText.text)
            {
                StartCoroutine(PIN_CORRECT());
            }
            else
            {
                StartCoroutine(PIN_INCORRECT());

            }
        }

        IEnumerator PIN_CORRECT() // PIN IS CORRECT
        {
            // PLAY CORRECT PIN SOUND
            pinAcceptedAudio.Play();

            yield return new WaitForSeconds(.2f);
            pinText.enabled = false;
            yield return new WaitForSeconds(.2f);
            pinText.enabled = true;
            yield return new WaitForSeconds(.2f);
            pinText.enabled = false;
            yield return new WaitForSeconds(.2f);
            pinText.enabled = true;
            yield return new WaitForSeconds(.2f);
            pinText.enabled = true;


            // PIN IS CORRECT ---> WHAT DO YOU WANT TO, DOWN BELOW ! [EG : OPENING CASE, OPENING GATE, ETC]

            ExampleUnlockText.SetActive(true); //Remove this line of Code, as this just for Demo

            yield return new WaitForSeconds(1.0f); // Wait a bit before deactivating the PIN locker UI and opening the door

            pinLockerUI.SetActive(false); // Deactivate the PIN locker UI

            door.ToggleDoor(); // Open the door
        }

        IEnumerator PIN_INCORRECT() // PIN IS INCORRECT
        {
            pinRejectedAudio.Play();

            Color normalPinColor = pinText.color;
            pinText.color = Color.red;
            // PLAY INCORRECT PIN SOUND

            yield return new WaitForSeconds(.35f);

            pinText.color = normalPinColor;
            pinText.text = "";
            i = 0;
        }
    }
}