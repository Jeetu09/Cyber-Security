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
    public Image ScreenUI;

    public ObjDetect objDetect;

    [SerializeField]
    private SceneMan sceneMan;
    [SerializeField]
    private ObjDetect objdetect;

    [SerializeField]
    private HealthSystem healthsystem;

    public GameObject SkinWin;
    public Button myButton;
    public GameObject DummyBtn;
    public GameObject DummyOrgBtn;
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
                healthsystem.DecreaseHealth(20);
                Debug.Log("OTP Matched Successfully!");
                CoorectUI.SetActive(true);
                otpInput.text = "";
                GenerateOTP(); // Make new OTP
                //Invoke(nameof(WarningAnimationClass), 4f);
                ScreenUI.gameObject.SetActive(false);
                objDetect.DisablePlayer();
                objDetect.mobileLocked = true;
                //objDetect.CloseAllUI();
                sceneMan.SceneCounterIncrease();
                SkinWin.SetActive(true);
                DummyBtn.SetActive(true);
                DummyOrgBtn.SetActive(false);
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
    }

    IEnumerator EnableDisable()
    {
        yield return new WaitForSeconds(2f);
    }

    void Start()
    {
        GenerateOTP();
        SkinWin.SetActive(false);
        DummyBtn.SetActive(false);
    }
}
