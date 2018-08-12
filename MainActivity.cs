using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;

namespace Calculator
{
    [Activity(Label = "Calculator", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView calculatorText;

        private string[] numbers = new string[2];
        private string @operator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            calculatorText = FindViewById<TextView>(Resource.Id.calculator_text_view);
        }

        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View v) {
            Button button = (Button)v;
            if ("0123456789.".Contains(button.Text))
                addNumber(button.Text);

            else if ("-+÷x".Contains(button.Text))
                addOperator(button.Text);

            else if ("=".Contains(button.Text))
                calculate();

            else erase();
        }


        private void erase()
        {
            numbers[0] = null;
            numbers[1] = null;
            @operator = null;
            updateCalculatorText();
        }

        private void calculate(string newOperator = null)
        {
            double? result = null;
            double? first = numbers[0] == null ? null : (double?)double.Parse(numbers[0]);
            double? second = numbers[1] == null ? null : (double?)double.Parse(numbers[1]);

            switch(@operator)
            {
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
                case "x":
                    result = first * second;
                    break;
                case "÷":
                    result = first / second;
                    break;
            }

            if(result != null)
            {
                numbers[0] = result.ToString();
                @operator = newOperator;
                numbers[1] = null;
                updateCalculatorText();
            }

        }

        private void addOperator(string value)
        {
            if (numbers[1] != null)
            {
                calculate(value);
                return;
            }
               
            @operator = value;
            updateCalculatorText();
        }

        private void addNumber(string value)
        {
            int index = @operator == null ? 0 : 1;
            if (value == "." && numbers[index].Contains("."))
                return;

            numbers[index] += value;

            updateCalculatorText();
            
        }

        private void updateCalculatorText() => calculatorText.Text = $"{numbers[0]} {@operator} {numbers[1]}";

    }
}

