using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMVVM.Client.Models
{
    public interface ICvdRisk_Model
    {
        Func<string> EvaluateRiskScore { get; set; }

        void CalculateRiskScore();      
        void SetAge(int age);
        void SetBloodPressureTreated(bool isTreated);
        void SetGender(string gender);
        void SetHdlCholesterol(int hdl);
        void SetSmoker(bool isSmoker);
        void SetSystolicPressure(int pressure);
        void SetTotalCholesterol(int tC);
    }

    public class CvdRisk_Model : ICvdRisk_Model
    {
        private string _gender;
        private int _score;
        private int _age;
        private int _totalCholesterol;
        private bool _isSmoker;
        private int _hdlCholesterol;
        private int _bloodPressure;
        private bool _bpTreated;


        public Func<int> EvaluateAge;
        public Func<int> EvaluateTotalCholesterol;
        public Func<int> EvaluateSmoker;
        public Func<int> EvaluateBloodPressure;
        private Func<string> evaluateRiskScore;

        public Func<string> EvaluateRiskScore { get => evaluateRiskScore; set => evaluateRiskScore = value; }

        public void SetGender(string gender)
        {
            _gender = gender;

            switch (_gender)
            {
                case "Female":
                    EvaluateAge = EvaluateAgeFemale;
                    EvaluateTotalCholesterol = EvaluateTotalCholesterolFemale;
                    EvaluateSmoker = EvaluateSmokerFemale;
                    EvaluateBloodPressure = EvaluateBloodPressureFemale;
                    EvaluateRiskScore = EvaluateRiskScoreFemale;
                    break;

                case "Male":
                    EvaluateAge = EvaluateAgeMale;
                    EvaluateTotalCholesterol = EvaluateTotalCholesterolMale;
                    EvaluateSmoker = EvaluateSmokerMale;
                    EvaluateBloodPressure = EvaluateBloodPressureMale;
                    EvaluateRiskScore = EvaluateRiskScoreMale;
                    break;
            }

        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void SetTotalCholesterol(int tC)
        {
            _totalCholesterol = tC;
        }

        public void SetSmoker(bool isSmoker)
        {
            _isSmoker = isSmoker;
        }

        public void SetHdlCholesterol(int hdl)
        {
            _hdlCholesterol = hdl;
        }

        public void SetSystolicPressure(int pressure)
        {
            _bloodPressure = pressure;
        }

        public void SetBloodPressureTreated(bool isTreated)
        {
            _bpTreated = isTreated;
        }

        public void CalculateRiskScore()
        {
            _score = 0;
            _score += EvaluateAge();
            _score += EvaluateTotalCholesterol();
            _score += EvaluateSmoker();
            _score += EvaluateHdlCholesterol();
            _score += EvaluateBloodPressure();
        }

        private int EvaluateAgeFemale()
        {
            int result = 0;

            switch (_age)
            {
                case var n when (n <= 34):
                    result = -7;
                    break;

                case var n when (n >= 35 && n <= 39):
                    result = -3;
                    break;

                case var n when (n >= 40 && n <= 44):
                    result = 0;
                    break;

                case var n when (n >= 45 && n <= 49):
                    result = 3;
                    break;

                case var n when (n >= 50 && n <= 54):
                    result = 6;
                    break;

                case var n when (n >= 55 && n <= 59):
                    result = 8;
                    break;

                case var n when (n >= 60 && n <= 64):
                    result = 10;
                    break;

                case var n when (n >= 65 && n <= 69):
                    result = 12;
                    break;

                case var n when (n >= 70 && n <= 74):
                    result = 14;
                    break;

                case var n when (n >= 75 && n <= 79):
                    result = 16;
                    break;
            }

            return result;
        }

        private int EvaluateAgeMale()
        {
            int result = 0;

            switch (_age)
            {
                case var n when (n <= 34):
                    result = -9;
                    break;

                case var n when (n >= 35 && n <= 39):
                    result = -4;
                    break;

                case var n when (n >= 40 && n <= 44):
                    result = 0;
                    break;

                case var n when (n >= 45 && n <= 49):
                    result = 3;
                    break;

                case var n when (n >= 50 && n <= 54):
                    result = 6;
                    break;

                case var n when (n >= 55 && n <= 59):
                    result = 8;
                    break;

                case var n when (n >= 60 && n <= 64):
                    result = 10;
                    break;

                case var n when (n >= 65 && n <= 69):
                    result = 11;
                    break;

                case var n when (n >= 70 && n <= 74):
                    result = 12;
                    break;

                case var n when (n >= 75 && n <= 79):
                    result = 13;
                    break;
            }

            return result;
        }

        private int EvaluateTotalCholesterolFemale()
        {
            int result = 0;

            switch (_age)
            {
                case var n when n >= 20 && n <= 39:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 4;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 8;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 11;
                            break;

                        case var t when t >= 280:
                            result = 13;
                            break;
                    }
                    break;

                case var n when n >= 40 && n <= 49:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 3;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 6;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 8;
                            break;

                        case var t when t >= 280:
                            result = 10;
                            break;
                    }
                    break;

                case var n when n >= 50 && n <= 59:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 2;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 4;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 5;
                            break;

                        case var t when t >= 280:
                            result = 7;
                            break;
                    }
                    break;

                case var n when n >= 60 && n <= 69:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 1;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 2;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 3;
                            break;

                        case var t when t >= 280:
                            result = 4;
                            break;
                    }
                    break;

                case var n when n >= 70 && n <= 79:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 1;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 1;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 2;
                            break;

                        case var t when t >= 280:
                            result = 2;
                            break;
                    }
                    break;
            }

            return result;
        }

        private int EvaluateTotalCholesterolMale()
        {
            int result = 0;

            switch (_age)
            {
                case var n when n >= 20 && n <= 39:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 4;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 7;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 9;
                            break;

                        case var t when t >= 280:
                            result = 11;
                            break;
                    }
                    break;

                case var n when n >= 40 && n <= 49:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 3;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 5;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 6;
                            break;

                        case var t when t >= 280:
                            result = 8;
                            break;
                    }
                    break;

                case var n when n >= 50 && n <= 59:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 2;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 3;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 4;
                            break;

                        case var t when t >= 280:
                            result = 5;
                            break;
                    }
                    break;

                case var n when n >= 60 && n <= 69:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 1;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 1;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 2;
                            break;

                        case var t when t >= 280:
                            result = 3;
                            break;
                    }
                    break;

                case var n when n >= 70 && n <= 79:
                    switch (_totalCholesterol)
                    {
                        case var t when t < 160:
                            result = 0;
                            break;

                        case var t when t >= 160 && t <= 199:
                            result = 0;
                            break;

                        case var t when t >= 200 && t <= 239:
                            result = 0;
                            break;

                        case var t when t >= 240 && t <= 279:
                            result = 1;
                            break;

                        case var t when t >= 280:
                            result = 1;
                            break;
                    }
                    break;
            }

            return result;
        }

        private int EvaluateSmokerFemale()
        {
            int result = 0;
            if (_isSmoker)
            {
                switch (_age)
                {
                    case var a when a >= 20 && a <= 39:
                        result = 9;
                        break;

                    case var a when a >= 40 && a <= 49:
                        result = 7;
                        break;

                    case var a when a >= 50 && a <= 59:
                        result = 4;
                        break;

                    case var a when a >= 60 && a <= 69:
                        result = 2;
                        break;

                    case var a when a >= 70 && a <= 79:
                        result = 1;
                        break;
                }
            }
            return result;
        }

        private int EvaluateSmokerMale()
        {
            int result = 0;
            if (_isSmoker)
            {
                switch (_age)
                {
                    case var a when a >= 20 && a <= 39:
                        result = 8;
                        break;

                    case var a when a >= 40 && a <= 49:
                        result = 5;
                        break;

                    case var a when a >= 50 && a <= 59:
                        result = 3;
                        break;

                    case var a when a >= 60 && a <= 69:
                        result = 1;
                        break;

                    case var a when a >= 70 && a <= 79:
                        result = 1;
                        break;
                }
            }
            return result;
        }

        public int EvaluateHdlCholesterol()
        {
            int result = 0;

            switch (_hdlCholesterol)
            {
                case var h when h >= 60:
                    result = -1;
                    break;

                case var h when h >= 40 && h <= 49:
                    result = 1;
                    break;

                case var h when h <= 40:
                    result = 2;
                    break;

                default:
                    break;
            }

            return result;
        }

        private int EvaluateBloodPressureFemale()
        {
            int result = 0;

            if (_bpTreated)
            {
                result = 2;
            }

            switch (_bloodPressure)
            {
                case var b when b < 120:
                    result = 0;
                    break;

                case var b when b >= 120 && b <= 129:
                    result += 1;
                    break;

                case var b when b >= 130 && b <= 139:
                    result += 2;
                    break;

                case var b when b >= 140 && b <= 159:
                    result += 3;
                    break;

                case var b when b >= 160:
                    result += 4;
                    break;
            }

            return result;
        }

        private int EvaluateBloodPressureMale()
        {
            int result = 0;

            if (_bpTreated)
            {
                result = 1;
            }

            switch (_bloodPressure)
            {
                case var b when b < 120:
                    result = 0;
                    break;

                case var b when b >= 120 && b <= 129:
                    result += 0;
                    break;

                case var b when b >= 130 && b <= 139:
                    result += 1;
                    break;

                case var b when b >= 140 && b <= 159:
                    result += 1;
                    break;

                case var b when b >= 160:
                    result += 2;
                    break;
            }

            return result;
        }

        private string EvaluateRiskScoreFemale()
        {
            string result;

            switch (_score)
            {
                case var s when s < 9:
                    result = "< 1%";
                    break;

                case var s when s >= 9 && s <= 12:
                    result = "1%";
                    break;

                case var s when s >= 13 && s <= 14:
                    result = "2%";
                    break;

                case 15:
                    result = "3%";
                    break;

                case 16:
                    result = "4%";
                    break;

                case 17:
                    result = "5%";
                    break;

                case 18:
                    result = "6%";
                    break;

                case 19:
                    result = "8%";
                    break;

                case 20:
                    result = "11%";
                    break;

                case 21:
                    result = "14%";
                    break;

                case 22:
                    result = "17%";
                    break;

                case 23:
                    result = "22%";
                    break;

                case 24:
                    result = "27%";
                    break;

                default:
                    result = "> 30%";
                    break;
            }

            return result;
        }

        private string EvaluateRiskScoreMale()
        {
            string result;

            switch (_score)
            {
                case var s when s < 1:
                    result = "< 1%";
                    break;

                case var s when s >= 1 && s <= 4:
                    result = "1%";
                    break;

                case var s when s >= 5 && s <= 6:
                    result = "2%";
                    break;

                case 7:
                    result = "3%";
                    break;

                case 8:
                    result = "4%";
                    break;

                case 9:
                    result = "5%";
                    break;

                case 10:
                    result = "6%";
                    break;

                case 11:
                    result = "8%";
                    break;

                case 12:
                    result = "10%";
                    break;

                case 13:
                    result = "12%";
                    break;

                case 14:
                    result = "16%";
                    break;

                case 15:
                    result = "20%";
                    break;

                case 16:
                    result = "25%";
                    break;

                default:
                    result = "> 30%";
                    break;
            }

            return result;
        }
    }
}
