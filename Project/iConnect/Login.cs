﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Guna.UI2.WinForms;

namespace iConnect
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "LvKz9QEmzDWQ6ncJrL9woSgNH9IChypOchmOSTOB",
            BasePath = "https://iconnect-nt106-default-rtdb.asia-southeast1.firebasedatabase.app"
        };

        IFirebaseClient client;

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                if (client != null)
                {
                    //MessageBox.Show("Connection Success");
                }
            }
            catch
            {
                MessageBox.Show("Connection Failed");
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Data datalayer = new Data()
            {
                username = usernameTxt.Text,
                password = passwdTxt.Text,
            };

            // Check if username is valid
            if (string.IsNullOrEmpty(usernameTxt.Text))
            {
                MessageBox.Show("Hãy nhập tên tài khoản.");
                return;
            }

            // Check if password is valid
            if (string.IsNullOrEmpty(passwdTxt.Text))
            {
                MessageBox.Show("Hãy nhập mật khẩu.");
                return;
            }

            FirebaseResponse response = client.Get("Information/" + usernameTxt.Text);

            if (response != null && response.ResultAs<Data>() != null)
            {
                Data result = response.ResultAs<Data>();

                if (usernameTxt.Text.Equals(result.username))
                {
                    // Check if password matches the password stored in the database
                    if (passwdTxt.Text.Equals(result.password))
                    {
                        Login.ActiveForm.Hide();
                        Home home = new Home();
                        home.Show();
                        //MessageBox.Show("Login successful!");

                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.");
            }
        }

        class Data
        {
            public string username { get; set; }
            public string password { get; set; }

        }



        private void closeAppBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login.ActiveForm.Hide();
            Signup signup = new Signup();
            signup.Show();
        }
    }
}
