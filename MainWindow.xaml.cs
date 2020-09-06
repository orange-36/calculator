using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace claculate
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();



        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            page2 w = new page2();//視窗名稱 變數名稱 = new 視窗名稱();
            w.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "7";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            input_text.Text += "+";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string input = input_text.Text;
            input_text.Text = "";
        }

        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            char[] infixa = input_text.Text.ToArray();
            ///////////////////////////////

            string s = new string(infixa);
            double answer = Calculate(s);



            //////////////////////////////
            string[] stra = new string[100];
            string[] strb = new string[100];
            void tostringnum(char[] arr, string[] strr)
            {
                int j = -1;
                Boolean nowisnum = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    switch (arr[i])
                    {
                        case '+':
                            j++;
                            strr[j] = "+";
                            nowisnum = false;
                            break;
                        case '-':
                            j++;
                            strr[j] = "-";
                            nowisnum = false;
                            break;
                        case '*':
                            j++;
                            strr[j] = "*";
                            nowisnum = false;
                            break;
                        case '/':
                            j++;
                            strr[j] = "/";
                            nowisnum = false;
                            break;
                        default:
                            if (nowisnum == true)
                            {
                                int a = 10 * (int.Parse(strr[j])) + int.Parse(arr[i].ToString());
                                strr[j] = a.ToString();
                            }
                            else
                            {
                                j++;
                                int a = int.Parse(arr[i].ToString());
                                strr[j] = a.ToString();
                                nowisnum = true;
                            }
                            break;
                    }

                }

            }
            tostringnum(infixa, stra);
            Array.Reverse(infixa);
            tostringnum(infixa, strb);
            for (int i = 0; i < infixa.Length; i++)
            {
                Console.WriteLine(stra[i] + " ");
            }

            int priority(string op)
            {
                switch (op)
                {
                    case "+": case "-": return 1;
                    case "*": case "/": return 2;
                    default: return 0;
                }
            }

            double cal(string op, double p1, double p2)
            {
                switch (op)
                {
                    case "+": return p1 + p2;
                    case "-": return p1 - p2;
                    case "*": return p1 * p2;
                    case "/": return p1 / p2;
                    default: return 0;
                }
            }

            void inToPostfix(string[] infix, string[] postfix)
            {
                string[] stack = new string[100];
                int i, j, top, long1;
                /*infix[i] != '\0'*/
                long1 = infix.Length;
                for (i = 0, j = 0, top = 0; i < long1; i++)
                {
                    switch (infix[i])
                    {
                        case "(":              // 運算子堆疊 
                            stack[++top] = infix[i];
                            break;
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                            while (priority(stack[top]) >= priority(infix[i]))
                            {
                                postfix[j++] = stack[top--];
                            }
                            stack[++top] = infix[i]; // 存入堆疊 
                            break;
                        case ")":
                            while (stack[top] != "(")
                            { // 遇 ) 輸出至 ( 
                                postfix[j++] = stack[top--];
                            }
                            top--;  // 不輸出 ( 
                            break;
                        default:  // 運算元直接輸出 
                            postfix[j++] = infix[i];
                            break;
                    }
                }
                while (top > 0)
                {
                    postfix[j++] = stack[top--];
                }
            }

            void inToPrefix(string[] infix, string[] postfix)
            {
                string[] stack = new string[100];
                int i, j, top, long1;
                /*infix[i] != '\0'*/
                long1 = infix.Length;
                for (i = 0, j = 0, top = 0; i < long1; i++)
                {
                    switch (infix[i])
                    {
                        case "(":              // 運算子堆疊 
                            stack[++top] = infix[i];
                            break;
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                            while (priority(stack[top]) > priority(infix[i]))
                            {
                                postfix[j++] = stack[top--];
                            }
                            stack[++top] = infix[i]; // 存入堆疊 
                            break;
                        case ")":
                            while (stack[top] != "(")
                            { // 遇 ) 輸出至 ( 
                                postfix[j++] = stack[top--];
                            }
                            top--;  // 不輸出 ( 
                            break;
                        default:  // 運算元直接輸出 
                            postfix[j++] = infix[i];
                            break;
                    }
                }
                while (top > 0)
                {
                    postfix[j++] = stack[top--];
                }
            }
            string test(string[] Stringarray)
            {
                string result = string.Join("", Stringarray);
                return result;
            }

            string[] postfix2 = new string[100];
            string[] postfix3 = new string[100];
            inToPostfix(stra, postfix2);
            inToPrefix(strb, postfix3);
            Array.Reverse(postfix3);
            string str2 = test(postfix2);
            string str3 = test(postfix3);

            Console.WriteLine(str2);
            //Console.WriteLine(eval(stra));
            //Console.WriteLine(Convert.ToString((int)eval(stra), 2)); 
            preorder_text.Text = str3;
            postorder_text.Text = str2;
            dec_text.Text = string.Format("{0:N3}", answer);
            bin_text.Text = Convert.ToString((int)answer, 2);
        }

        double Calculate(String s)
        {
            Parser p = new Parser();
            List<Element> e = p.Parse(s);
            InfixToPostfix i = new InfixToPostfix();
            e = i.ConvertFromInfixToPostFix(e);

            PostFixEvaluator pfe = new PostFixEvaluator();
            return pfe.Evaluate(e);
        }
        private double atof(char[] opnd)
        {
            throw new NotImplementedException();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            string input = input_text.Text;
            input_text.Text += "0";
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "1";
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "2";
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "3";
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "4";
        }


        private void _5_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "5";
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "6";
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "8";
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "9";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            input_text.Text += "-";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            input_text.Text += "*";
        }

        private void ____Click(object sender, RoutedEventArgs e)
        {
            input_text.Text += "/";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string input = input_text.Text;
            if (input.Length > 0)
                input_text.Text = input.Remove(input.Length - 1);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//偵測視窗有沒有被關掉
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Boolean already = false;
            string sqlStr = "insert into calculator (Expression,Preorder,Postorder,Dicimal,bbinary)values( @str1 ,@str2, @str3,@str4, @str5)";
            String cmdText = "select * from calculator";


            string MysqlCon = "server=localhost;User Id=root;password=aa910622;Database=hci";

            MySqlConnection conn = new MySqlConnection(MysqlCon);
            MySqlCommand command = conn.CreateCommand();

            conn.Open();

            //////////
            MySqlCommand cmd1 = new MySqlCommand(cmdText, conn);

            MySqlDataReader reader1 = cmd1.ExecuteReader(); //execure the reader
            int ii = 0;
            while (reader1.Read())
            {

                Console.WriteLine(reader1.GetString(0));
                if (input_text.Text.Equals(reader1.GetString(0)))
                {
                    already = true;
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

            }


            conn.Close();

            //////////

            if (already == true)
            {
                MessageBox.Show(input_text.Text + " :這個算式已經存在了");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);

                cmd.Parameters.AddWithValue("@str1", input_text.Text);
                cmd.Parameters.AddWithValue("@str2", preorder_text.Text);
                cmd.Parameters.AddWithValue("@str3", postorder_text.Text);
                cmd.Parameters.AddWithValue("@str4", dec_text.Text);
                cmd.Parameters.AddWithValue("@str5", bin_text.Text);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
                conn.Close();
            }





            
        }
    }

    public enum OperatorType { MULTIPLY, DIVIDE, ADD, SUBTRACT, EXPONENTIAL, OPAREN, CPAREN };
    public interface Element
    {
    }

    public class NumberElement : Element
    {
        double number;
        public Double getNumber()
        {
            return number;
        }

        public NumberElement(String number)
        {
            this.number = Double.Parse(number);
        }

        public override String ToString()
        {
            return ((int)number).ToString();
        }
    }

    public class OperatorElement : Element
    {
        public OperatorType type;
        char c;
        public OperatorElement(char op)
        {
            c = op;
            if (op == '+')
                type = OperatorType.ADD;
            else if (op == '-')
                type = OperatorType.SUBTRACT;
            else if (op == '*')
                type = OperatorType.MULTIPLY;
            else if (op == '/')
                type = OperatorType.DIVIDE;
            else if (op == '^')
                type = OperatorType.EXPONENTIAL;
            else if (op == '(')
                type = OperatorType.OPAREN;
            else if (op == ')')
                type = OperatorType.CPAREN;
        }

        public override String ToString()
        {
            return c.ToString();
        }
    }

    public class Parser
    {
        List<Element> e = new List<Element>();
        public List<Element> Parse(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (Char.IsDigit(c))
                    sb.Append(c);
                if (i + 1 < s.Length)
                {
                    char d = s[i + 1];
                    if (Char.IsDigit(d) == false && sb.Length > 0)
                    {
                        e.Add(new NumberElement(sb.ToString()));
                        //clears stringbuilder
                        sb.Remove(0, sb.Length);
                    }
                }

                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^'
                        || c == '(' || c == ')')
                    e.Add(new OperatorElement(c));
            }
            if (sb.Length > 0)
                e.Add(new NumberElement(sb.ToString()));

            return e;
        }
    }


    public class InfixToPostfix
    {
        List<Element> converted = new List<Element>();
        int Precedence(OperatorElement c)
        {
            if (c.type == OperatorType.EXPONENTIAL)
                return 2;
            else if (c.type == OperatorType.MULTIPLY || c.type == OperatorType.DIVIDE)
                return 3;
            else if (c.type == OperatorType.ADD || c.type == OperatorType.SUBTRACT)
                return 4;
            else
                return Int32.MaxValue;
        }

        public void ProcessOperators(Stack<Element> st, Element element, Element top)
        {
            while (st.Count > 0 && Precedence((OperatorElement)element) >= Precedence((OperatorElement)top))
            {
                Element p = st.Pop();
                if (((OperatorElement)p).type == OperatorType.OPAREN)
                    break;
                converted.Add(p);
                if (st.Count > 0)
                    top = st.First();
            }
        }
        public List<Element> ConvertFromInfixToPostFix(List<Element> e)
        {
            List<Element> stack1 = new List<Element>(e);
            Stack<Element> st = new Stack<Element>();
            for (int i = 0; i < stack1.Count; i++)
            {
                Element element = stack1[i];
                if (element.GetType().Equals(typeof(OperatorElement)))
                {
                    if (st.Count == 0 ||
                            ((OperatorElement)element).type == OperatorType.OPAREN)
                        st.Push(element);
                    else
                    {
                        Element top = st.First();
                        if (((OperatorElement)element).type == OperatorType.CPAREN)
                            ProcessOperators(st, element, top);
                        else if (Precedence((OperatorElement)element) < Precedence((OperatorElement)top))
                            st.Push(element);
                        else
                        {
                            ProcessOperators(st, element, top);
                            st.Push(element);
                        }
                    }
                }
                else
                    converted.Add(element);
            }

            //pop all operators in stack
            while (st.Count > 0)
            {
                Element b1 = st.Pop();
                converted.Add(b1);
            }

            return converted;
        }

        public override String ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int j = 0; j < converted.Count; j++)
                s.Append(converted[j].ToString() + " ");
            return s.ToString();
        }
    }

    public class PostFixEvaluator
    {
        Stack<Element> stack = new Stack<Element>();

        NumberElement calculate(NumberElement left, NumberElement right, OperatorElement op)
        {
            Double temp = Double.MaxValue;
            if (op.type == OperatorType.ADD)
                temp = left.getNumber() + right.getNumber();
            else if (op.type == OperatorType.SUBTRACT)
                temp = left.getNumber() - right.getNumber();
            else if (op.type == OperatorType.MULTIPLY)
                temp = left.getNumber() * right.getNumber();
            else if (op.type == OperatorType.DIVIDE)
                temp = left.getNumber() / right.getNumber();
            else if (op.type == OperatorType.EXPONENTIAL)
                temp = Math.Pow(left.getNumber(), right.getNumber());

            return new NumberElement(temp.ToString());
        }
        public Double Evaluate(List<Element> e)
        {
            List<Element> v = new List<Element>(e);
            for (int i = 0; i < v.Count; i++)
            {
                Element element = v[i];
                if (element.GetType().Equals(typeof(NumberElement)))
                    stack.Push(element);
                if (element.GetType().Equals(typeof(OperatorElement)))
                {
                    NumberElement right = (NumberElement)stack.Pop();
                    NumberElement left = (NumberElement)stack.Pop();
                    NumberElement result = calculate(left, right, (OperatorElement)element);
                    stack.Push(result);
                }
            }
            return ((NumberElement)stack.Pop()).getNumber();
        }
    }
}
