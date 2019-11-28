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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ein Double muss mit einer Ziffer beginnen. Gibt es Nachkommastellen,
        /// müssen auch diese mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private double ScanNumber(ref int pos)
        {
            int scanNum1;
            double result = 0;
            bool isPositive = true;

            if(ExpressionText[pos] == '-')
            {
                isPositive = false;
                SkipBlanks(ref pos);
            }

            scanNum1 = ScanInteger(ref pos);
            if(ExpressionText[pos] == ',')
            {
                int scanNum2 = ScanInteger(ref pos);
                int tmp = scanNum2;
                int count = 1;
                while(tmp != 0)
                {
                    tmp = tmp / 10;
                    count *= 10;
                    if(tmp == 0)
                    {
                        count *= 10;
                    }
                }
                scanNum2 = scanNum2 / count;
                result = scanNum1 + scanNum2;
            }
            else
            {
                result = scanNum1;
            }

            if(isPositive && result > 0)
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
        private int ScanInteger(ref int pos)
        {
            int number = 0;

            while(char.IsDigit(ExpressionText[pos]))
            {
                number = number*10 + (ExpressionText[pos]);
                pos++;
            }

            return number;
        }

        /// <summary>
        /// Setzt die Position weiter, wenn Leerzeichen vorhanden sind
        /// </summary>
        /// <param name="pos"></param>
        private void SkipBlanks(ref int pos)
        {
            while(ExpressionText[pos] == ' ' && pos < ExpressionText.Length)
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
