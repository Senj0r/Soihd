using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace soihd
{
    public partial class Form1 : Form
    {
        MySqlConnection sqlCon;

        public Form1()
        {
            InitializeComponent();
            string connectionString = "Database=soihd;Data Source=localhost;User Id=root;Password=1111";
            sqlCon = new MySqlConnection(connectionString);
            sqlCon.Open();
            

        }

        private string select_id_brand(string name_brand)
        {
            MySqlDataReader sqlReader = null;
            string sql = "SELECT ID_brand_phone FROM phone_brands WHERE phone_brands.name_brand='" + name_brand + "';";
            MySqlCommand command = new MySqlCommand(sql, sqlCon);
            sqlReader = (MySqlDataReader)command.ExecuteReader();
            string brand = "";
            sqlReader.Read();
            brand = Convert.ToString(sqlReader[0]);
            if (sqlReader != null)
                sqlReader.Close();
            return brand;
        }
        private string select_id_employee(string name_employee, string surname_employee)
        {
            MySqlDataReader sqlReader = null;
            string sql = "SELECT number_employee from employees where name_employee='" + name_employee +
                "' AND surname_employee='" + surname_employee + "';";
            MySqlCommand command = new MySqlCommand(sql, sqlCon);
            sqlReader = (MySqlDataReader)command.ExecuteReader();
            string name = "";
            sqlReader.Read();
            name = Convert.ToString(sqlReader[0]);
            if (sqlReader != null)
                sqlReader.Close();
            return name;
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            { 
                string model = maskedTextBox1.Text;
                if (!string.IsNullOrEmpty(model) && !string.IsNullOrWhiteSpace(model))
                {
                    string sql = "SELECT phone_brands.name_brand 'Марка',phone_warehouse.name_model 'Модель'," +
                    "phone_warehouse.price_phone 'Цена'" +
                    ",phone_warehouse.qty_phone 'Количество' " +
                    " FROM phone_brands INNER JOIN phone_warehouse ON " +
                    "phone_brands.ID_brand_phone = phone_warehouse.ID_brand_phone " +
                    "WHERE phone_brands.name_brand='" + model + "' ORDER BY phone_warehouse.Serial_number_phone;";

                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();


                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);

                    }
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Введите название"), Convert.ToString("FindError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }



        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                string sql = "SELECT phone_brands.name_brand 'Марка',phone_warehouse.name_model 'Модель'," +
                        "phone_warehouse.price_phone 'Цена'" +
                        ",phone_warehouse.qty_phone 'Количество' " +
                        " FROM phone_brands INNER JOIN phone_warehouse ON " +
                        "phone_brands.ID_brand_phone = phone_warehouse.ID_brand_phone ";

                if (checkBox1.Checked)
                {
                    MySqlDataReader sqlReader = null;

                    string tmp = sql + "WHERE phone_warehouse.price_phone>0 AND phone_warehouse.price_phone<10000 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox2.Checked)
                {
                    MySqlDataReader sqlReader = null;
                    string tmp = sql + "WHERE phone_warehouse.price_phone>=10000 AND phone_warehouse.price_phone<40000 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox3.Checked)
                {
                    MySqlDataReader sqlReader = null;
                    String tmp = sql + "WHERE phone_warehouse.price_phone>=40000 AND phone_warehouse.price_phone<65000 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox4.Checked)
                {
                    MySqlDataReader sqlReader = null;
                    string tmp = sql + " WHERE phone_warehouse.price_phone>=65000  ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                string sql = "SELECT phone_brands.name_brand 'Марка',phone_warehouse.name_model 'Модель'," +
                        "phone_warehouse.price_phone 'Цена'" +
                        ",phone_warehouse.qty_phone 'Количество' " +
                        " FROM phone_brands INNER JOIN phone_warehouse ON " +
                        "phone_brands.ID_brand_phone = phone_warehouse.ID_brand_phone ";
                if (radioButton1.Checked)
                {
                    MySqlDataReader sqlReader = null;
                    string tmp=sql+" WHERE phone_warehouse.ID_brand_phone=2 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);
                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                else if (radioButton2.Checked)
                {
                    MySqlDataReader sqlReader = null;
                    string tmp=sql+"WHERE phone_warehouse.ID_brand_phone=3 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);
                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                else if (radioButton3.Checked)
                {

                    MySqlDataReader sqlReader = null;

                    string tmp = sql +"WHERE phone_warehouse.ID_brand_phone=4 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);
                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                else if (radioButton4.Checked)
                {
                    MySqlDataReader sqlReader = null;

                    string tmp = sql +"WHERE phone_warehouse.ID_brand_phone=1 ORDER BY phone_warehouse.price_phone;";
                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3]) };
                        dataGridView1.Rows.Add(rows);
                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     
        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox2.Text) && !string.IsNullOrEmpty(maskedTextBox3.Text) && !string.IsNullOrEmpty(maskedTextBox4.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox5.Text) && !string.IsNullOrEmpty(maskedTextBox6.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox7.Text) && !string.IsNullOrEmpty(maskedTextBox8.Text) && !string.IsNullOrEmpty(maskedTextBox34.Text))
                {

                    string brand = "",
                        employee = "";
                    brand = Convert.ToString(select_id_brand(Convert.ToString(maskedTextBox4.Text)));
                    employee = Convert.ToString(select_id_employee(Convert.ToString(maskedTextBox5.Text), Convert.ToString(maskedTextBox34.Text)));
                    if (brand == "")
                    {
                        MessageBox.Show(Convert.ToString("Такого бренда нет в базе"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (employee == "")
                    {
                        MessageBox.Show(Convert.ToString("Такого сотрудника нет в базе"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string sql = "INSERT phone_warehouse(Serial_number_phone, Serial_number, number_employee," +
                        " ID_brand_phone, name_model, qty_phone, price_phone)" +
                        "VALUES(" + maskedTextBox2.Text + "," + maskedTextBox3.Text + "," + employee + "," + brand + ", '" + maskedTextBox6.Text +
                        "'," + maskedTextBox8.Text + "," + maskedTextBox7.Text + ");";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно добавлена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox10.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox11.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox13.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox15.Text))
                {
                    string sql = "INSERT employees (number_employee,name_employee,surname_employee,age)" +
                        "VALUES (" + Convert.ToString(maskedTextBox10.Text) + ",'" +
                        Convert.ToString(maskedTextBox11.Text) + "','" +
                        Convert.ToString(maskedTextBox15.Text) + "'," +
                        Convert.ToString(maskedTextBox13.Text) + ");";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно добавлена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox12.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox14.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox16.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox17.Text))
                {
                    string sql = "UPDATE employees SET name_employee='" + Convert.ToString(maskedTextBox12.Text) +
                        "', surname_employee='" + Convert.ToString(maskedTextBox16.Text) +
                        "',age=" + Convert.ToString(maskedTextBox17.Text) + " WHERE number_employee=" + Convert.ToString(maskedTextBox14.Text) + ";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно изменена"), Convert.ToString("UpdateSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("UpdateError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox18.Text) && !string.IsNullOrEmpty(maskedTextBox19.Text))
                {

                    string sql = "DELETE FROM employees WHERE name_employee='" + Convert.ToString(maskedTextBox18.Text) + "' AND surname_employee='" + Convert.ToString(maskedTextBox19.Text) + "';";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись удалена"), Convert.ToString("Delete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("DeleteError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            MySqlDataReader sqlRead = null;
            dataGridView4.Rows.Clear();
            try
            {

                if (!string.IsNullOrEmpty(maskedTextBox20.Text) && !string.IsNullOrWhiteSpace(maskedTextBox20.Text))
                {
                    checkBox9.Checked = false;
                    string sql = "Select ID_brand_phone,name_brand FROM phone_brands WHERE name_brand='" + Convert.ToString(maskedTextBox20.Text) + "' ORDER BY name_brand;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]) };
                        dataGridView4.Rows.Add(rows);

                    }

                } else if (checkBox9.Checked)
                {
                    string sql = "Select ID_brand_phone,name_brand FROM phone_brands ORDER BY name_brand;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]) };
                        dataGridView4.Rows.Add(rows);

                    }
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlRead != null)
                    sqlRead.Close();
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            MySqlDataReader sqlRead = null;
            dataGridView3.Rows.Clear();
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox9.Text) && !string.IsNullOrWhiteSpace(maskedTextBox9.Text))
                {

                    string sql = "Select name_employee,surname_employee,age FROM employees WHERE surname_employee='" + Convert.ToString(maskedTextBox9.Text) + "' ORDER BY age;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]), Convert.ToString(sqlRead[2]) };
                        dataGridView3.Rows.Add(rows);

                    }

                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlRead != null)
                    sqlRead.Close();
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            MySqlDataReader sqlRead = null;
            dataGridView3.Rows.Clear();
            try
            {

                if (checkBox5.Checked)
                {
                    string sql = "Select name_employee,surname_employee,age FROM employees WHERE age>=18 AND age<25 ORDER BY age;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]), Convert.ToString(sqlRead[2]) };
                        dataGridView3.Rows.Add(rows);

                    }
                    if (sqlRead != null)
                        sqlRead.Close();

                }
                if (checkBox6.Checked)
                {
                    string sql = "Select name_employee,surname_employee,age FROM employees WHERE age>=25 AND age<30 ORDER BY age;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]), Convert.ToString(sqlRead[2]) };
                        dataGridView3.Rows.Add(rows);

                    }
                    if (sqlRead != null)
                        sqlRead.Close();
                }
                if (checkBox7.Checked)
                {
                    string sql = "Select name_employee,surname_employee,age FROM employees WHERE age>=30 AND age<45 ORDER BY age;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]), Convert.ToString(sqlRead[2]) };
                        dataGridView3.Rows.Add(rows);

                    }
                    if (sqlRead != null)
                        sqlRead.Close();
                }
                if (checkBox8.Checked)
                {
                    string sql = "Select name_employee,surname_employee,age FROM employees WHERE age>=45 AND age<=60 ORDER BY age;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlRead = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlRead.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlRead[0]), Convert.ToString(sqlRead[1]), Convert.ToString(sqlRead[2]) };
                        dataGridView3.Rows.Add(rows);

                    }
                    if (sqlRead != null)
                        sqlRead.Close();
                }
                if (!checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked && !checkBox8.Checked)
                {
                    MessageBox.Show(Convert.ToString("Выберете параметр фильтрации"), Convert.ToString("Внимание"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlRead != null)
                    sqlRead.Close();
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(maskedTextBox21.Text) && !string.IsNullOrEmpty(maskedTextBox22.Text))
                {
                    string sql = "INSERT phone_brands (ID_brand_phone,name_brand) " +
                        "VALUES (" + Convert.ToString(maskedTextBox21.Text) + ",'" + Convert.ToString(maskedTextBox22.Text) + "');";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно добавлена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox23.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox24.Text))
                {

                    string sql = "UPDATE phone_brands SET name_brand='" + Convert.ToString(maskedTextBox24.Text) +
                           "'" + "WHERE ID_brand_phone=" + Convert.ToString(maskedTextBox23.Text) + ";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно изменена"), Convert.ToString("UpdateSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("UpdateError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox25.Text))
                {

                    string sql = "DELETE FROM phone_brands WHERE name_brand='" + Convert.ToString(maskedTextBox25.Text) + "';";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись удалена"), Convert.ToString("Delete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("DeleteError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox26.Text))
                {
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.name_detail='" + Convert.ToString(maskedTextBox26.Text) + "';";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            MySqlDataReader sqlReader = null;


            try
            {
                if (checkBox10.Checked)
                {
                    checkBox13.Checked = false;
                    checkBox14.Checked = false;
                    checkBox15.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.price_detail>=0 AND parts_warehouse.price_detail<2000;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox11.Checked)
                {
                    checkBox13.Checked = false;
                    checkBox14.Checked = false;
                    checkBox15.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.price_detail>=2000 AND parts_warehouse.price_detail<5000;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox12.Checked)
                {
                    checkBox13.Checked = false;
                    checkBox14.Checked = false;
                    checkBox15.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.price_detail>=5000;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            MySqlDataReader sqlReader = null;


            try
            {
                if (checkBox13.Checked)
                {
                    checkBox10.Checked = false;
                    checkBox11.Checked = false;
                    checkBox12.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.qty_detail>=0 AND parts_warehouse.qty_detail<150;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox14.Checked)
                {
                    checkBox10.Checked = false;
                    checkBox11.Checked = false;
                    checkBox12.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.qty_detail>=150 AND parts_warehouse.qty_detail<500;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox15.Checked)
                {
                    checkBox10.Checked = false;
                    checkBox11.Checked = false;
                    checkBox12.Checked = false;
                    string sql = "SELECT parts_warehouse.name_detail,  parts_warehouse.price_detail, parts_warehouse.qty_detail," +
                        " parts_warehouse.date_shipmant,employees.name_employee ,employees.surname_employee " +
                        "FROM parts_warehouse" +
                        " INNER JOIN employees ON " +
                        "parts_warehouse.number_employee=employees.number_employee WHERE " +
                        "parts_warehouse.qty_detail>=500;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]), Convert.ToString(sqlReader[3])
                        ,Convert.ToString(sqlReader[4]),Convert.ToString(sqlReader[5])};
                        dataGridView2.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(maskedTextBox27.Text) && !string.IsNullOrEmpty(maskedTextBox28.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox29.Text) && !string.IsNullOrEmpty(maskedTextBox30.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox31.Text) && !string.IsNullOrEmpty(maskedTextBox32.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox32.Text))
                {
                    string idSotr = select_id_employee(Convert.ToString(maskedTextBox32.Text), Convert.ToString(maskedTextBox33.Text));
                    string sql = "INSERT parts_warehouse (Serial_number,name_detail,price_detail,qty_detail,date_shipmant,number_employee) " +
                        "VALUES (" + Convert.ToString(maskedTextBox27.Text) + ",'" + Convert.ToString(maskedTextBox28.Text) + "'," +
                        Convert.ToString(maskedTextBox29.Text) + "," + Convert.ToString(maskedTextBox30.Text) + ",'" +
                        Convert.ToString(maskedTextBox31.Text) + "'," + idSotr + ");";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно добавлена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox44.Text) && !string.IsNullOrEmpty(maskedTextBox45.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox46.Text) && !string.IsNullOrEmpty(maskedTextBox47.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox48.Text) && !string.IsNullOrEmpty(maskedTextBox48.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox43.Text))
                {
                    string idSotr = select_id_employee(Convert.ToString(maskedTextBox45.Text), Convert.ToString(maskedTextBox44.Text));
                    string sql = "UPDATE parts_warehouse SET name_detail='" + Convert.ToString(maskedTextBox48.Text) +
                            "',price_detail=" + Convert.ToString(maskedTextBox47.Text) +
                            ",qty_detail=" + Convert.ToString(maskedTextBox43.Text) + ",date_shipmant='" + Convert.ToString(maskedTextBox46.Text) +
                            "',number_employee=" + idSotr + " WHERE Serial_number=" +
                        Convert.ToString(maskedTextBox49.Text) + ";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно изменена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("InserError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox50.Text))
                {

                    string sql = "DELETE FROM parts_warehouse WHERE name_detail='" + Convert.ToString(maskedTextBox50.Text) + "';";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись удалена"), Convert.ToString("Delete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("DeleteError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(maskedTextBox35.Text))&& (!string.IsNullOrEmpty(maskedTextBox39.Text)))
                {

                    string sql = "DELETE FROM phone_warehouse WHERE name_model='" + Convert.ToString(maskedTextBox39.Text) + 
                        "' AND ID_brand_phone="+select_id_brand(Convert.ToString(maskedTextBox35.Text))+";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись удалена"), Convert.ToString("Delete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("DeleteError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox36.Text) && !string.IsNullOrEmpty(maskedTextBox37.Text) &&
                   !string.IsNullOrEmpty(maskedTextBox38.Text) && !string.IsNullOrEmpty(maskedTextBox40.Text) &&
                   !string.IsNullOrEmpty(maskedTextBox42.Text))
                {

                    string sql = "UPDATE  phone_warehouse SET name_model='" + Convert.ToString(maskedTextBox38.Text) + "', ID_brand_phone=" +
                        select_id_brand(Convert.ToString(maskedTextBox40.Text)) + ", qty_phone=" + Convert.ToString(maskedTextBox36.Text) +
                        ",price_phone=" + Convert.ToString(maskedTextBox37.Text) + " WHERE Serial_number_phone=" + Convert.ToString(maskedTextBox42.Text) + ";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись Изменена"), Convert.ToString("Update"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            {
                string sql = "SELECT workshop_number,qty_work_shift," +
                         "build_date, Serial_number_phone,Serial_number," +
                        "employees.name_employee,employees.surname_employee " +

                    " FROM workshop INNER JOIN employees ON " +
                    "employees.number_employee=workshop.number_employee ";

                string ceh = maskedTextBox41.Text;
                if (!string.IsNullOrEmpty(ceh) && !string.IsNullOrWhiteSpace(ceh))
                {
                    checkBox16.Checked = false;
                    
                    sql=sql+"WHERE workshop_number=" + ceh + " ORDER BY workshop.Serial_number_phone;";

                }
                else if(checkBox16.Checked)
                {
                    checkBox17.Checked = false;
                    checkBox18.Checked = false;
                    checkBox19.Checked = false;
                    sql = sql + " ORDER BY workshop.Serial_number_phone;";
                  
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Введите название"), Convert.ToString("FindError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MySqlCommand command = new MySqlCommand(sql, sqlCon);
                sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();


                while (await sqlReader.ReadAsync())
                {
                    string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,Convert.ToString(sqlReader[6])  };
                    dataGridView5.Rows.Add(rows);

                }
                if (sqlReader != null)
                    sqlReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            {
                string sql = "SELECT workshop_number,qty_work_shift," +
                         "build_date, Serial_number_phone,Serial_number," +
                        "employees.name_employee,employees.surname_employee " +
                    " FROM workshop INNER JOIN employees ON " +
                    "employees.number_employee=workshop.number_employee ";
                if (checkBox17.Checked)
                {
                    checkBox16.Checked = false;
                    string tmp=sql+"WHERE qty_work_shift>=0  AND  qty_work_shift<20 ORDER BY workshop.qty_work_shift;";

                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();


                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,Convert.ToString(sqlReader[6])  };
                        dataGridView5.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox18.Checked)
                {
                    checkBox16.Checked = false;
                  string tmp =sql+"WHERE qty_work_shift>=20  AND qty_work_shift<100 ORDER BY workshop.qty_work_shift;";

                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();


                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,Convert.ToString(sqlReader[6])  };
                        dataGridView5.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                if (checkBox19.Checked)
                {
                    checkBox16.Checked = false;
                    string tmp = sql+"WHERE qty_work_shift>=100 ORDER BY workshop.qty_work_shift;";

                    MySqlCommand command = new MySqlCommand(tmp, sqlCon);
                    sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();


                    while (await sqlReader.ReadAsync())
                    {
                        string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,Convert.ToString(sqlReader[6])  };
                        dataGridView5.Rows.Add(rows);

                    }
                    if (sqlReader != null)
                        sqlReader.Close();
                }
               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            {
                string sql = "SELECT Order_number,purchaser," +
                         "date_sale, price_order,qty_purchased_product," +
                         "phone_brands.name_brand,phone_warehouse.name_model," +
                        "employees.name_employee,employees.surname_employee " +
                    " FROM sales_departament INNER JOIN employees ON " +
                    "employees.number_employee=sales_departament.number_employee " +
                    "INNER JOIN phone_brands ON phone_brands.ID_brand_phone=sales_departament.ID_brand_phone " +
                    "INNER JOIN phone_warehouse ON phone_warehouse.Serial_number_phone=sales_departament.Serial_number_phone ";
                string comp = maskedTextBox51.Text;
                if (!string.IsNullOrEmpty(comp) && !string.IsNullOrWhiteSpace(comp))
                {
                    checkBox20.Checked = false;    
                    sql =sql+"WHERE sales_departament.purchaser='" + comp + "' ORDER BY sales_departament.purchaser;";
   
                }
                else if (checkBox20.Checked)
                {
                     sql = sql+" ORDER BY sales_departament.purchaser;";
                   
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Введите название"), Convert.ToString("FindError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MySqlCommand command = new MySqlCommand(sql, sqlCon);
                sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,
                            Convert.ToString(sqlReader[6]),Convert.ToString(sqlReader[7]),Convert.ToString(sqlReader[8])  };
                    dataGridView6.Rows.Add(rows);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            MySqlDataReader sqlReader = null;
            try
            {
                checkBox20.Checked = false;
                string sql = "SELECT Order_number,purchaser," +
                     "date_sale, price_order,qty_purchased_product," +
                     "phone_brands.name_brand,phone_warehouse.name_model," +
                    "employees.name_employee,employees.surname_employee " +
                " FROM sales_departament INNER JOIN employees ON " +
                "employees.number_employee=sales_departament.number_employee " +
                "INNER JOIN phone_brands ON phone_brands.ID_brand_phone=sales_departament.ID_brand_phone " +
                "INNER JOIN phone_warehouse ON phone_warehouse.Serial_number_phone=sales_departament.Serial_number_phone WHERE ";
                if (radioButton5.Checked)
                {
                    sql = sql + "price_order>=0 AND price_order<200000 ORDER BY price_order;";
                }
                else if(radioButton6.Checked)
                {
                    sql = sql + "price_order>=200000 AND price_order<500000 ORDER BY price_order;";
                }
                else if(radioButton7.Checked)
                {
                    sql = sql + "price_order>=500000 AND price_order<2000000 ORDER BY price_order;";
                }
                else if (radioButton8.Checked)
                {
                    sql = sql + "price_order>=2000000  ORDER BY price_order;";
                }
                MySqlCommand command = new MySqlCommand(sql, sqlCon);
                sqlReader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    string[] rows = { Convert.ToString(sqlReader[0]), Convert.ToString(sqlReader[1]), Convert.ToString(sqlReader[2]),
                            Convert.ToString(sqlReader[3]),Convert.ToString(sqlReader[4]) ,Convert.ToString(sqlReader[5]) ,
                            Convert.ToString(sqlReader[6]),Convert.ToString(sqlReader[7]),Convert.ToString(sqlReader[8])  };
                    dataGridView6.Rows.Add(rows);

                }
                if (sqlReader != null)
                    sqlReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(maskedTextBox52.Text)&&
                    !string.IsNullOrEmpty(maskedTextBox53.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox54.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox55.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox56.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox57.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox58.Text))
                {
                    string[] id = select_phone(maskedTextBox57.Text,maskedTextBox58.Text);
                  
                    string sql = "INSERT sales_departament (Order_number,purchaser,date_sale,price_order,qty_purchased_product,ID_brand_phone,Serial_number_phone,Serial_number,number_employee) " +
                        "VALUES (" + Convert.ToString(maskedTextBox52.Text) + ",'" + Convert.ToString(maskedTextBox53.Text) + "','" +
                        Convert.ToString(maskedTextBox54.Text) + "'," + Convert.ToString(maskedTextBox55.Text) + "," +
                        Convert.ToString(maskedTextBox56.Text) + "," +id[2] + ","+id[0]+","+id[1]+","+id[3]+");";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно добавлена"), Convert.ToString("InsertSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] select_phone(string brand_phone,string model_phone)
        {
            MySqlDataReader sqlReader = null;
           
                string sql = "SELECT Serial_number_phone,Serial_number,ID_brand_phone,number_employee from phone_warehouse where ID_brand_phone=" +
                    select_id_brand(Convert.ToString(brand_phone)) +
                    " AND name_model='" + model_phone + "';";
                MySqlCommand command = new MySqlCommand(sql, sqlCon);
                sqlReader = (MySqlDataReader)command.ExecuteReader();

                sqlReader.Read();
                
                   string [] name = { Convert.ToString(sqlReader[0]),Convert.ToString(sqlReader[1]),Convert.ToString(sqlReader[2]),Convert.ToString(sqlReader[3])};
          
                if (sqlReader != null)
                    sqlReader.Close();
            

            return name;
        }

        private async void button27_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(maskedTextBox59.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox60.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox61.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox62.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox63.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox64.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox65.Text))
                {
                    string[] id = select_phone(maskedTextBox60.Text, maskedTextBox59.Text);
                    string sql="UPDATE sales_departament SET Serial_number_phone="+id[0]+
                        ",ID_brand_phone="+id[2]+",purchaser='"+maskedTextBox64.Text+
                        "',date_sale='"+maskedTextBox63.Text+"',price_order="+ maskedTextBox62.Text+
                        ",qty_purchased_product="+ maskedTextBox61.Text+ ",Serial_number="+id[1]+
                        ",number_employee="+id[3]+
                        " WHERE Order_number="+ maskedTextBox65.Text+ " ;";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись успешно изменена"), Convert.ToString("UpdateSuccsess"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            try
            {
                if((!string.IsNullOrEmpty(maskedTextBox66.Text)) && (!string.IsNullOrEmpty(maskedTextBox67.Text))&&
                    (!string.IsNullOrEmpty(maskedTextBox68.Text)) && (!string.IsNullOrEmpty(maskedTextBox69.Text)))
                {

                    string sql = "DELETE FROM sales_departament WHERE Order_number=" + Convert.ToString(maskedTextBox66.Text) +
                        " AND purchaser='" + Convert.ToString(maskedTextBox67.Text) + "' AND date_sale='"+
                        Convert.ToString(maskedTextBox68.Text)+"' AND price_order="+Convert.ToString(maskedTextBox69.Text)+";";
                    MySqlCommand command = new MySqlCommand(sql, sqlCon);
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show(Convert.ToString("Запись удалена"), Convert.ToString("Delete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Convert.ToString("Заполните все поля"), Convert.ToString("DeleteError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
