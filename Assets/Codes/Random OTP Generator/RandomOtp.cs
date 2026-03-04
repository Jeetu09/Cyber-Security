using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class RandomOtp : MonoBehaviour
{
    public TextMeshProUGUI otpText, otpText2;
    public TMP_InputField otpInput;
    public GameObject CoorectUI, WrongUI;

    [Header("Animation")]
    public Image warning;
    public Image ScreenUI;

    public ObjDetect objDetect;


    private void GenerateOTP()
    {
        int otp = Random.Range(1000, 9999);
        otpText.text = otp.ToString();
        otpText2.text = otp.ToString();
    }

    public void OtpButton()
    {
        if (otpInput.text.Length == 4)
        {
            if (otpInput.text == otpText.text)
            {
                Debug.Log("OTP Matched Successfully!");
                CoorectUI.SetActive(true);
                WrongUI.SetActive(false);
                otpInput.text = "";

                GenerateOTP(); // Make new OTP

                Invoke(nameof(WarningAnimationClass), 4f);
                ScreenUI.gameObject.SetActive(false);
                objDetect.DisablePlayer();
                objDetect.mobileLocked = true;
                objDetect.CloseAllUI();



            }
            else
            {
                Debug.Log("OTP Did Not Match. Try Again!");
                otpInput.text = "";
                StartCoroutine(EnableDisable());
            }
        }
    }

    public void WarningAnimationClass()
    {
        //warninganim.SetTrigger("Warning");
        warning.gameObject.SetActive(true);

    }

    IEnumerator EnableDisable()
    {
        WrongUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        WrongUI.SetActive(false);
    }

    void Start()
    {
        GenerateOTP();
        CoorectUI.SetActive(false);
        WrongUI.SetActive(false);
        warning.gameObject.SetActive(false);
    }
}
