using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool Parantheses(string expression)
        {
            StringBuilder par = new StringBuilder();
            foreach (char character in expression)
                if (character == '(' || character == ')')
                    par.Append(character);
            Stack<char> verify = new Stack<char>();
            foreach (char paranthese in par.ToString())
                if (paranthese == '(')
                {
                    verify.Push(paranthese);
                }
                else
                {
                    if (verify.Count() != 0 && verify.Peek() == '(')
                        verify.Pop();
                    else
                        return false;
                }
            if (verify.Count() != 0)
                return false;
            return true;
        }
        public int priority(char c)
        {
            switch (c)
            {
                case '*':
                    return 1;
                case '/':
                    return 1;
                case '+':
                    return 0;
                case '-':return 0;
                default:
                    return -1;
            }
        }
        public string CalculateInfixExpression(string expression)
        {
            Stack<string> st = new Stack<string>();
            Queue<string> q = new Queue<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    st.Push(expression[i].ToString());
                else
                    if (expression[i] >= '0' && expression[i] <= '9')
                {
                    StringBuilder number = new StringBuilder();
                    //number.Append(expression[i]);
                    while (expression[i] >= '0' && expression[i] <= '9')
                    {
                        number.Append(expression[i]);
                        i++;
                        if (i == expression.Length)
                            break;
                    }
                    //if (number.Length > 1)
                        i--;
                    q.Enqueue(number.ToString());

                }
                else
                    if (expression[i] == ')')
                {
                    while (st.Peek()[0] != '(')
                        q.Enqueue(st.Pop());
                    st.Pop();
                }
                else
                {
                    if (st.Count==0)
                        st.Push(expression[i].ToString());
                    else
                    {
                        if (priority(expression[i]) <= priority(st.Peek()[0]))
                        {
                            q.Enqueue(st.Pop());
                            st.Push(expression[i].ToString());
                        }
                        else
                            st.Push(expression[i].ToString());
                    }
                    }



                
        }
            while (st.Count != 0)
                q.Enqueue(st.Pop());
            foreach (string op in q)
                if (priority(op[0]) >= 0)
                {
                    int op1, op2;
                    int.TryParse(st.Pop().ToString(), out op1);
                    int.TryParse(st.Pop().ToString(), out op2);
                    int result=0;
                    if (op[0] == '+')
                        result=op2 + op1;
                    if (op[0] == '-')
                        result=op2 - op1;
                    if (op[0] == '*')
                        result=op2*op1;
                    if (op[0] == '/')
                        result=op2 / op1;
                    st.Push(result.ToString());
                }
                else
                    st.Push(op.ToString());
            if (st.Count == 1)
                return st.Pop();
            else
                return "Wrong expression or algorithm";

        }
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            text.AppendText(b.Content.ToString());
        }
        private void ClearText(object sender,RoutedEventArgs e)
        {
            text.Clear();
        } 
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (!Parantheses(text.Text.ToString()))
                text.AppendText("- incoreect due to parantheses");
            else
            {
                string expr = text.Text;
                text.Clear();
                text.AppendText(CalculateInfixExpression(expr));
            }
                
        }

        private void text_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
