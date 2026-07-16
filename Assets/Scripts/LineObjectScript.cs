using UnityEngine;
using TMPro;

public class LineObjectScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _extendRetractObject;
    [SerializeField] private GameObject _inputObject;
    public TMP_InputField _inputField;
    private ManagerScript manager;
    private BasePageScript pageScript;

    [Header("Texts")]
    public TMP_Text _order_main_text;
    public TMP_Text _modify_bucket_input_text;
    public TMP_Text _pallets_done_text;
    public TMP_Text _pallets_total_text;
    public TMP_Text _buckets_total_text;
    public TMP_Text _buckets_left_text;

    public TMP_Text _orderNumberText;
    public TMP_Text _bucketsText;
    public TMP_Text _capsText;
    public TMP_Text _boxesText;

    //[Header("Data")]

    private int _buckets_input_amount;
    private int _pallets_done_amount;
    private int _pallets_total_amount;
    private int _buckets_total_amount;
    private bool _extendRetractEnabled;
    private bool _orderNumberChecked;
    private string _orderNumberFirstText;

    private enum TextField { None, ModifyBucketInput, PalletsDone, PalletsTotal, BucketsTotal, OrderNumber, Buckets, Caps, Boxes }
    private TextField _textField;

    private void Start()
    {
        StartInitialize();
    }

    private void StartInitialize()
    {
        GameObject MS = GameObject.Find("Manager");
        manager = MS.GetComponent<ManagerScript>();
        pageScript = GetComponentInParent<BasePageScript>();
        _textField = TextField.None;
        _extendRetractEnabled = false;
        _extendRetractObject.SetActive(false);
        _inputObject.SetActive(false);
        _inputField.onSubmit.AddListener(ChangeText);
    }

    public void ChangeText(string text)
    {
        TMP_Text input;
        int amount = int.Parse(text);

        if (_textField == TextField.OrderNumber)
        {
            if (!_orderNumberChecked) OrderNumberChangeTextFirst(text);
            else OrderNumberChangeTextSecond(text);

                return;
        }

        switch (_textField)
        {
            case TextField.None: return;
            case TextField.ModifyBucketInput: input = _modify_bucket_input_text; _buckets_input_amount = amount; break;
            case TextField.PalletsDone: input = _pallets_done_text; _pallets_done_amount = amount; Calculate(); break;
            case TextField.PalletsTotal: input = _pallets_total_text; _pallets_total_amount = amount; Calculate(); break;
            case TextField.BucketsTotal: input = _buckets_total_text; _buckets_total_amount = amount; Calculate(); break;
            case TextField.Buckets: input = _bucketsText; break;
            case TextField.Caps: input = _capsText; break;
            case TextField.Boxes: input = _boxesText; break;
            default: return;
        }

        
        input.text = text;
        _inputField.text = "";

        _inputObject.SetActive(false);
        Calculate();
    }

    private void OrderNumberChangeTextFirst(string text)
    {
        _inputField.text = "";
        _orderNumberFirstText = text;
        _orderNumberChecked = true;
    }

    private void OrderNumberChangeTextSecond(string text)
    {

        _orderNumberText.text = $"{_orderNumberFirstText}.{text}";
        _inputField.text = "";
        _orderNumberChecked = false;
        _inputObject.SetActive(false);
    }

    public void OrderNumberButton()
    {
        _textField = TextField.OrderNumber;
        _inputObject.SetActive(true);
    }

    public void BucketsButton()
    {
        _textField = TextField.Buckets;
        _inputObject.SetActive(true);
    }

    public void CapsButton()
    {
        _textField = TextField.Caps;
        _inputObject.SetActive(true);
    }

    public void BoxesButton()
    {
        _textField = TextField.Boxes;
        _inputObject.SetActive(true);
    }

    public void ModifyBucketInputButton()
    {
        _textField = TextField.ModifyBucketInput;
        _inputObject.SetActive(true);
    }

    public void PalletsDoneButton()
    {
        _textField = TextField.PalletsDone;
        _inputObject.SetActive(true);
    }

    public void PalletsTotalButton()
    {
        _textField = TextField.PalletsTotal;
        _inputObject.SetActive(true);
    }

    public void BucketsTotal()
    {
        _textField = TextField.BucketsTotal;
        _inputObject.SetActive(true);
    }

    public void Calculate()
    {
        float number = _buckets_total_amount * (_pallets_total_amount - _pallets_done_amount) / _pallets_total_amount;
        float toBring = number / _buckets_input_amount;
        _buckets_left_text.text = toBring.ToString("F2");
    }



    public void ExtendRetractButton()
    {
        _extendRetractEnabled = !_extendRetractEnabled;

        if (_extendRetractEnabled ) _extendRetractObject.SetActive(true);
        else _extendRetractObject.SetActive(false);
    }

    public void DeleteOrderButton()
    {
        pageScript.BaseLinesSort();
        Destroy(this.gameObject);
    }

    public void ChangeOrderMainText(int number)
    {
        _order_main_text.text = $"ORDER #{number + 1}";
    }

    public string SendStringToBasePage()
    {
        return _order_main_text.text;
    }

}
