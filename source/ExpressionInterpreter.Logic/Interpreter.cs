using System;
using System.Text;

namespace ExpressionInterpreter.Logic
{
    public class Interpreter
    {
        //Hallo Bernhard!
        //asdfasdfasdf

        private double _operandLeft;
        private double _operandRight;
        private char _op;  // Operator                  

        /// <summary>
        /// Eingelesener Text
        /// </summary>
        public string ExpressionText { get; private set; }

        public double OperandLeft
        {
            get { return _operandLeft; }
        }

        public double OperandRight
        {
            get { return _operandRight; }
        }

        public char Op
        {
            get { return _op; }
        }


        public void Parse(string expressionText)
        {
            ExpressionText = expressionText;
            ParseExpressionStringToFields();
        }

        /// <summary>
        /// Wertet den Ausdruck aus und gibt das Ergebnis zurück.
        /// Fehlerhafte Operatoren und Division durch 0 werden über Exceptions zurückgemeldet
        /// </summary>
        public double Calculate()
        {
            double result = 0;

            switch (_op)
            {
                case '+':
                    result = OperandLeft + OperandRight;
                    break;
                case '-':
                    result = OperandLeft - OperandRight;
                    break;
                case '*':
                    result = OperandLeft * OperandRight;
                    break;
                case '/':
                    result = OperandLeft / OperandRight;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Expressionstring in seine Bestandteile zerlegen und in die Felder speichern.
        /// 
        ///     { }[-]{ }D{D}[,D{D}]{ }(+|-|*|/){ }[-]{ }D{D}[,D{D}]{ }
        ///     
        /// Syntax  OP = +-*/
        ///         Vorzeichen -
        ///         Zahlen double/int
        ///         Trennzeichen Leerzeichen zwischen OP, Vorzeichen und Zahlen
        /// </summary>
        public void ParseExpressionStringToFields()
        {
            int pos = 0;
            _operandLeft = GetOperand(ref pos);
            _op = GetOp(ref pos);
            _operandRight = GetOperand(ref pos);
        }

        private double GetOperand(ref int pos)
        {
            double result = 0;

            SkipBlanks(ref pos);
            result = ScanNumber(ref pos);
            SkipBlanks(ref pos);

            return result;
        }
        
        private char GetOp(ref int pos)
        {
            char result = char.MinValue;
            switch(ExpressionText[pos])
            {
                case '+':
                    result = '+';
                    break;
                case '-':
                    result = '-';
                    break;
                case '*':
                    result = '*';
                    break;
                case '/':
                    result = '/';
                    break;
            }
            pos++;

            return result;
        }
        /// <summary>
        /// Ein Double muss mit einer Ziffer beginnen. Gibt es Nachkommastellen,
        /// müssen auch diese mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private double ScanNumber(ref int pos)
        {
            double scanNum1 = 0;
            double result = 0;
            bool isPositive = true;

            if (ExpressionText[pos] == '-')
            {
                isPositive = false;
                pos++;
                SkipBlanks(ref pos);
            }

            int count = 0;
            scanNum1 = ScanInteger(ref pos, ref count);
            if (ExpressionText[pos] == ',')
            {
                pos++;
                double scanNum2 = ScanInteger(ref pos, ref count);
                //double tmp = scanNum2;
                //int count = 1;
                //while (tmp != 0)
                //{
                //    tmp = tmp / 10;
                //    count *= 10;
                //    if (tmp == 0)
                //    {
                //        count *= 10;
                //    }
                //}
                while(scanNum2 > 1)
                {
                    scanNum2 = scanNum2 / 10;
                }
                while(count > 0)
                {
                    scanNum2 = scanNum2 / 10;
                    count--;
                }
                //scanNum2 = scanNum2 / count;
                result = scanNum1 + scanNum2;
            }
            else
            {
                result = scanNum1;
            }

            if (!(isPositive) && result > 0)
            {
                result = result * -1;
            }

            return result;
        }

        /// <summary>
        /// Eine Ganzzahl muss mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int ScanInteger(ref int pos, ref int count)
        {
            int number = 0;
            int numberChanger = 0;
            bool breakIt = false;

            while (char.IsDigit(ExpressionText[pos]) && !breakIt)
            {
                if(ExpressionText[pos] == '0')
                {
                    count++;
                }
                numberChanger = ExpressionText[pos] -48;
                number = number * 10 + (numberChanger);
                pos++;
                if(pos == ExpressionText.Length)
                {
                    pos--;
                    breakIt = true;
                }
            }


            return number;
        }

        /// <summary>
        /// Setzt die Position weiter, wenn Leerzeichen vorhanden sind
        /// </summary>
        /// <param name="pos"></param>
        private void SkipBlanks(ref int pos)
        {
            while (pos < ExpressionText.Length && ExpressionText[pos] == ' ')
            {
                pos++;
            }
        }

        /// <summary>
        /// Exceptionmessage samt Innerexception-Texten ausgeben
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionTextWithInnerExceptions(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
