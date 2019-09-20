using BlazorMVVM.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.ViewModels
{
    public interface ICvdRisk_ViewModel
    {
        int Progress { get; }
        int Step { get; }
        bool PreviousButtonDisabled { get; }
        bool NextButtonDisabled { get; }
        int Age { get; set; }
        int TotalCholesterol { get; set; }
        List<string> GenderChoices { get; }
        string Gender { get; }
        string IsSmoker { get; }
        List<string> YesNoChoices { get; }
        int HdlCholesterol { get; set; }
        int SystolicPressure { get; set; }
        string BpIsTreated { get; }
        string RiskPercentage { get; }

        void GoToNextStep();
        void GoToPreviousStep();
        void SetAge();
        void SetBloodPressure();
        void SetBpIsTreated(string treated);
        void SetGender(string gender);
        void SetHdlCholesterol();
        void SetNavButtons();
        void SetSmoker(string smoker);
        void SetTotalCholesterol();
    }

    public class CvdRisk_ViewModel : ICvdRisk_ViewModel
    {
        private ICvdRisk_Model _cvdRisk_Model;
        private string _gender = "Male";
        private int _age;
        private int _totalCholesterol;
        private string _isSmoker;
        private int _hdlCholesterol = 100;
        private int _systolicPressure;
        private string _bpIsTreated;
        private string _riskPercentage;

        public CvdRisk_ViewModel(ICvdRisk_Model riskModel)
        {
            _cvdRisk_Model = riskModel;
            Progress = 0;
            Step = 1;
            PreviousButtonDisabled = true;
            NextButtonDisabled = true;
            GenderChoices = new List<string>();
            GenderChoices.Add("Male");
            GenderChoices.Add("Female");
            YesNoChoices = new List<string>();
            YesNoChoices.Add("Yes");
            YesNoChoices.Add("No");
        }

        public int Progress { get; private set; }

        public int Step { get; private set; }

        public bool PreviousButtonDisabled { get; private set; }
        public bool NextButtonDisabled { get; private set; }

        public List<string> GenderChoices { get; private set; }
        public int Age { get => _age; set => _age = value; }
        public int TotalCholesterol { get => _totalCholesterol; set => _totalCholesterol = value; }
        public string Gender { get => _gender; private set => _gender = value; }
        public List<string> YesNoChoices { get; private set; }
        public string IsSmoker { get => _isSmoker; private set => _isSmoker = value; }
        public int HdlCholesterol { get => _hdlCholesterol; set => _hdlCholesterol = value; }
        public int SystolicPressure { get => _systolicPressure; set => _systolicPressure = value; }
        public string BpIsTreated { get => _bpIsTreated; private set => _bpIsTreated = value; }
        public string RiskPercentage { get => _riskPercentage; private set => _riskPercentage = value; }

        public void GoToNextStep()
        {
            Step += 1;            
            Progress += 15;
            if (Step == 8)
            {
                Progress = 100;
            }
            SetNavButtons();
        }

        public void GoToPreviousStep()
        {
            if (Step > 1)
            {
                Step -= 1;
                Progress -= 15;
            }
            if (Step == 1)
            {
                Progress = 0;
            }
            SetNavButtons();
        }

        public void SetNavButtons()
        {
            Console.WriteLine($"Set Nav Buttons: {Step}");
            NextButtonDisabled = false;
            switch (Step)
            {
                case 1:
                    PreviousButtonDisabled = true;
                    if (string.IsNullOrEmpty(_gender))
                    {
                        NextButtonDisabled = true;
                    }                    
                    break;
                case 2:
                    PreviousButtonDisabled = false;
                    if (_age == 0)
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 3:
                    PreviousButtonDisabled = false;
                    if (_totalCholesterol == 0)
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 4:
                    PreviousButtonDisabled = false;
                    if (string.IsNullOrEmpty(_isSmoker))
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 5:
                    PreviousButtonDisabled = false;
                    Console.WriteLine($"_hdlCholesterol == {_hdlCholesterol}");
                    if (_hdlCholesterol == 0)
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 6:
                    PreviousButtonDisabled = false;
                    if (_systolicPressure == 0)
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 7:
                    PreviousButtonDisabled = false;
                    if (string.IsNullOrEmpty(_bpIsTreated))
                    {
                        NextButtonDisabled = true;
                    }
                    break;
                case 8:
                    NextButtonDisabled = true;
                    _cvdRisk_Model.CalculateRiskScore();
                    _riskPercentage = _cvdRisk_Model.EvaluateRiskScore();
                    break;
                default:
                    break;
            }
        }

        public void SetGender(string gender)
        {
            Console.WriteLine(gender);            
            _gender = gender;
            _cvdRisk_Model.SetGender(gender);
            SetNavButtons();        
        }

        public void SetAge()
        {
            if (_age < 20)
            {
                _age = 20;
            }
            else if (_age > 79)
            {
                _age = 79;
            }
            _cvdRisk_Model.SetAge(_age);
            SetNavButtons();
        }

        public void SetTotalCholesterol()
        {
            _cvdRisk_Model.SetTotalCholesterol(TotalCholesterol);
            SetNavButtons();
        }

        public void SetSmoker(string smoker)
        {
            IsSmoker = smoker;
            if (smoker == "Yes")
            {
                _cvdRisk_Model.SetSmoker(true);
            }
            else
            {
                _cvdRisk_Model.SetSmoker(false);
            }        
            SetNavButtons();
        }

        public void SetHdlCholesterol()
        {
            _cvdRisk_Model.SetHdlCholesterol(HdlCholesterol);
            SetNavButtons();
        }
        
        public void SetBloodPressure()
        {
            _cvdRisk_Model.SetSystolicPressure(SystolicPressure);
            SetNavButtons();
        }

        public void SetBpIsTreated(string treated)
        {
            BpIsTreated = treated;
            if (treated == "Yes")
            {
                _cvdRisk_Model.SetBloodPressureTreated(true);
            }
            else
            {
                _cvdRisk_Model.SetBloodPressureTreated(false);
            }
            SetNavButtons();            
        }

    }
}
