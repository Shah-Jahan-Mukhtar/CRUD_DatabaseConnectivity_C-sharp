﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace CRUD_DatabaseConnectivity
{
    public partial class Form1 : Form

    {
        SqlConnection connection;
        SqlCommand command;
        DataTable data = new DataTable();

        string db_connection = @"Data Source=DESKTOP-T2H5N7O\SQLEXPRESS;initial Catalog=students;Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
            this.connection = new SqlConnection(db_connection);
            data.Columns.Add("Name");
            data.Columns.Add("Roll");
            data.Columns.Add("CNIC");

            dataGridView1.DataSource = data;
            read_database();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row1 = data.NewRow();
            row1["Name"] = name.Text;
            row1["Roll"] = roll.Text;
            row1["CNIC"] = cnic.Text;
            data.Rows.Add(row1);
            write_database();
            dataGridView1.Refresh();

            name.Text = "";
            roll.Text = "";
            cnic.Text = "";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void read_database()
        {
            if(!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(roll.Text) && !string.IsNullOrWhiteSpace(cnic.Text))
            {
                try
                {
                    SqlDataReader dataReader;
                    command = new SqlCommand("select * from student_table", this.connection);
                    this.connection.Open();
                    dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        DataRow row = this.data.NewRow();
                        row["Name"] = dataReader.GetValue(0);
                        row["Roll"] = dataReader.GetValue(1);
                        row["CNIC"] = dataReader.GetValue(2);
                        this.data.Rows.Add(row);
                    }
                    dataGridView1.Refresh();
                    this.connection.Close();


                }
                catch(Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
           
        }

        private void write_database()
        {
            if (!string.IsNullOrWhiteSpace(name.Text) && !string.IsNullOrWhiteSpace(roll.Text) && !string.IsNullOrWhiteSpace(cnic.Text))
            {
                try
                {
                    string sql = $"insert into student_table(Name,Roll_No,CNIC)" +
                        $"values('{name.Text}','{roll.Text},{cnic.Text}')";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    MessageBox.Show(adapter.InsertCommand.ExecuteNonQuery().ToString() + "New Record added");

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("Something is missing");
            }

        }
        }
   
}
