using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace claculate
{
    /// <summary>
    /// page2.xaml 的互動邏輯
    /// </summary>
    public partial class page2 : Window
    {
        List<UserData> list = new List<UserData>();
        string MysqlCon = "server=localhost;User Id=root;password=aa910622;Database=hci";
        public page2()
        {
            InitializeComponent();
            //List<UserData> list = new List<UserData>();
            //string MysqlCon = "server=localhost;User Id=root;password=aa910622;Database=hci";

            MySqlConnection conn = new MySqlConnection(MysqlCon);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            String cmdText = "select * from calculator";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader
            while (reader.Read())
            {
                list.Add(new UserData(reader.GetString("Expression"),
                    reader.GetString("Preorder"),
                    reader.GetString("Postorder"),
                    reader.GetString("Dicimal"),
                    reader.GetString("bbinary")));
                    
            }
            Console.ReadLine();
            conn.Close();

            //Console.WriteLine(list[0].Expression);
            /*list.Add(new UserData("77", "77", "77", "77", "77"));*/
            listview.ItemsSource = list;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//偵測視窗有沒有被關掉
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow w = new MainWindow();//視窗名稱 變數名稱 = new 視窗名稱();
            w.Show();
        }




        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("11");

        }

        //刪除資料
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //设置girid的选中元素为Button所在行的元素
            //listview.SelectedItem = ((Button)sender).DataContext;
            //在数据集合中删除此元素    
            if (listview.SelectedIndex >-1)
            {
                int a = listview.SelectedIndex;
                Console.WriteLine(a);
                MessageBox.Show("移除"+list[a].Expression);
                //list.RemoveAt(listview.SelectedIndex);
                MySqlConnection conn = new MySqlConnection(MysqlCon);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                String cmdText = "DELETE FROM calculator WHERE Expression = @pname ";
                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                Console.WriteLine(a);
                cmd.Parameters.AddWithValue("@pname", list[a].Expression);
                MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader                
                conn.Close();
                list.RemoveAt(listview.SelectedIndex);

            }
            
            listview.Items.Refresh();//刷新listview
        }
    }
    class UserData
    {
        private string _Expression;
        private string _Preorder;
        private string _Postorder;
        private string _Decimal;
        private string _Binary;
        public string Expression
        {
            get { return _Expression; }
            set
            {
                _Expression = value;
            }
        }
        public string Preorder
        {
            get { return _Preorder; }
            set
            {
                _Preorder = value;
            }
        }
        public string Postorder
        {
            get { return _Postorder; }
            set
            {
                _Postorder = value;
            }
        }
        public string Decimal
        {
            get { return _Decimal; }
            set
            {
                _Decimal = value;
            }
        }
        public string Binary
        {
            get { return _Binary; }
            set
            {
                _Binary = value;
            }
        }
        public UserData(string Expression, string Preorder, string Postorder, string Decimal, string Binary)
        {
            _Expression = Expression;
            _Preorder = Preorder;
            _Postorder = Postorder;
            _Decimal = Decimal;
            _Binary = Binary;

        }

        public UserData()
        {
        }
    }


    //UserData的定義：



}
