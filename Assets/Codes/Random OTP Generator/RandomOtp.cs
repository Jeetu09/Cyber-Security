using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject  FatherScolding;

    [SerializeField]
    private SceneMan sceneMan;
    [SerializeField]
    private ObjDetect objdetect;
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
                otpInput.text = "";
                GenerateOTP(); // Make new OTP
                Invoke(nameof(WarningAnimationClass), 4f);
                ScreenUI.gameObject.SetActive(false);
                objDetect.DisablePlayer();
                objDetect.mobileLocked = true;
                objDetect.CloseAllUI();
                FatherScolding.SetActive(true);
                sceneMan.SceneCounterIncrease();
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
        objdetect.CloseAllUI();
        SceneManager.LoadScene("Gaming");
    }

    IEnumerator EnableDisable()
    {
        yield return new WaitForSeconds(2f);
    }

    void Start()
    {
        GenerateOTP();
        warning.gameObject.SetActive(false);
        FatherScolding.SetActive(false);
    }
}
