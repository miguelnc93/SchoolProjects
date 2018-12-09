using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace abd_project
{
    /**
     * En este proyecto se pretende desarrollar un gestor de base de datos que funcione 
     * a través de sentencias en español. Donde al final se pretende sea funcional y englobe 
     * conocimientos adquiridos durante todo el curso y los requerimientos 
     * y estatutos establecidos al inicio del proyecto.
     **/
    public partial class Form1 : Form
    {
        //Variables de conexion para MYSQL
        MySqlConnection con = new MySqlConnection();
        String connectionString;

        public Form1()
        {
            InitializeComponent();
        }

        
        private void esconder()
        {
            txtusuario.Hide();
            txtcontraseña.Hide();
            button1.Hide();
            lblusuario.Hide();
            lblcontraseña.Hide();
            txtservidor.Hide();
            lblservidor.Hide();
            groupBox1.Hide();
            pictureBox1.Hide();
        }

        /**
          * Conectar a mysql y mostrar las bases de datos disponibles en ese servidor.
          * Se pide ingresar tanto el server(Ej: 127.0.0.1 o localhost), user y password correspondiente.
        **/
        private void button1_Click(object sender, EventArgs e)
        {
            //Crear comando.
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "show databases";
            try
            {
                connectionString = "SERVER=" + txtservidor.Text + ";UID=" + txtusuario.Text + ";" + "PASSWORD=" + txtcontraseña.Text + ";";
                con.ConnectionString = connectionString;
                //Abrir conexion con mysql.
                con.Open();

                //Comando a ejecutar.
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row += reader.GetValue(i).ToString();
                        //Se agrega en listbox cada uno de los valores encontrados.
                        listBox1.Items.Add(row);
                    }
                }
                reader.Close();

                //Una vez se valida la conexion con mysql, manda a llamar al metodo esconder()
                //y aparte se muestran ciertos elementos. 
                listBox1.Show();
                txtsentencia.Show();
                lblsentencia.Show();
                txtresultados.Show();
                btexecute.Show();
                btimportar.Show();
                //Llamar a metodo esconder.
                esconder();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number.ToString());
                MessageBox.Show(ex.Message);

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /**
         * Al cargar la forma por primera vez se esconden elementos que solo se utilizan cuando existe 
         * conexion con mysql.
         **/
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Hide();
            txtsentencia.Hide();
            lblsentencia.Hide();
            txtresultados.Hide();
            btexecute.Hide();
            listBox2.Hide();
            dataGridView1.Hide();
            btimportar.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            con.Close();
        }


        /**
         * Este btn de "Ejecutar" contendra todas las condiciones necesarias para poder ejecutar los 
         * comandos de mysql.
         * 
         * En un TextBox se ingresaran los comandos manualmente, los comandos en español que se pueden utilizar
         * son los siguientes:
         *      CREA BD NOMBRE_BD;
         *      USA BD NOMBRE_BD;
         *      BORRA BD NOMBRE_BD;
         *      CREA TABLA NOMBRE_TABLA(NOMBRE_CAMPO TIPO_DE_CAMPO);
         *      BORRA TABLA NOMBRE_TABLA;
         *      MUESTRA TABLAS;
         *      ALTERAR TABLA NOMBRE_TABLA AGREGA NOMBRE_CAMPO TIPO_DE_DATO;
         *      ALTERAR TABLA NOMBRE_TABLA BORRA CAMPO;
         *      ALTERAR TABLA NOMBRE_TABLA MODIFICA NOMBRE_COLUMNA NOMBRE_NUEVA_COLUMNA TIPO_DE_DATO;
         *      SELECCIONAR * DE NOMBRE_TABLA;
         *      SELECCIONAR * DE NOMBRE_TABLA DONDE INGRESAR_CONDICION;
         *      AGREGA REGISTRO NOMBRE_TABLA(NOMBRE_CAMPO(S)) VALUES(DATOS_A_INGRESAR);
         *      BORRA REGISTRO DE NOMBRE_TABLA DONDE INGRESAR_CONDICION;
         *      MODIFICA REGISTRO NOMBRE_TABLA FIJAR CAMPOS_CON_VALOR DONDE INGRESAR_CONDICION;
         *      DESCRIBE NOMBRE_TABLA;
         **/
        private void btexecute_Click(object sender, EventArgs e)
        {
            string resultado = "";

            try
            {
                string cadenaTexto = txtsentencia.Text;

                if (txtsentencia.Text.Contains("USA BD") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("BD") + 2, cadenaTexto.Length - cadenaTexto.IndexOf("BD") - 2);

                    MySqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandText = "use " + resultado.ToLower();
                    try
                    {
                        MySqlDataReader reader3 = cmd3.ExecuteReader();
                        reader3.Read();
                        txtresultados.Text = "BD " + resultado + " en uso.";
                        //Mostrar elementos escondidos
                        listBox1.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //Llamar a metodo esconder
                        esconder();

                        //Cerrar instruccion para estar disponible y usar otra
                        reader3.Close();

                        //Limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                        labelbdenuso.Text = "BD seleccionada:" + resultado;
                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }
                    dataGridView1.Hide();
                }

                else if (txtsentencia.Text.Contains("CREA BD") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("BD") + 2, cadenaTexto.Length - cadenaTexto.IndexOf("BD") - 2);

                    MySqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandText = "create database " + resultado;
                    try
                    {
                        MySqlDataReader reader1 = cmd1.ExecuteReader();
                        reader1.Read();
                        txtresultados.Text = "BD CREADA CON EXITO.";
                        //mostrar elementos escondidos
                        listBox1.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //llamar a metodo esconder
                        esconder();

                        //cerrar instruccion para estar disponible y usar otra
                        reader1.Close();

                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();

                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }

                    dataGridView1.Hide();
                }

                else if (txtsentencia.Text.Contains("BORRA BD") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("BD") + 2, cadenaTexto.Length - cadenaTexto.IndexOf("BD") - 2);

                    MySqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandText = "drop database " + resultado;
                    try
                    {
                        MySqlDataReader reader4 = cmd4.ExecuteReader();
                        reader4.Read();
                        txtresultados.Text = "BD " + resultado + " eliminada.";
                        //mostrar elementos escondidos
                        listBox1.Show();
                        //comboBox1.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //llamar a metodo esconder
                        esconder();

                        //cerrar instruccion para estar disponible y usar otra
                        reader4.Close();

                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        listBox2.Items.Clear();
                        listBox2.Hide();

                    }

                    dataGridView1.Hide();
                }

                else if (txtsentencia.Text.Contains("CREA TABLA") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("TABLA") + 6, cadenaTexto.Length - cadenaTexto.IndexOf("TABLA") - 6);

                    MySqlCommand cmd5 = con.CreateCommand();

                    int countSpaces = resultado.Count(Char.IsWhiteSpace);

                    if (countSpaces > 100)
                    {
                        listBox2.Hide();
                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                    }

                    else
                    {
                        try
                        {
                            cmd5.CommandText = "create table " + resultado + ";";
                            MySqlDataReader reader5 = cmd5.ExecuteReader();
                            reader5.Read();
                            txtresultados.Text = "Tabla " + resultado + " creada exitosamente.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader5.Close();
                            muestrabases();
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();

                        }
                        catch (MySqlException ex)
                        {
                            txtresultados.Text = ex.Message;
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }
                        dataGridView1.Hide();
                    }

                }

                else if (txtsentencia.Text.Contains("BORRA TABLA") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("TABLA") + 5, cadenaTexto.Length - cadenaTexto.IndexOf("TABLA") - 5);

                    MySqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandText = "drop table " + resultado;
                    try
                    {
                        MySqlDataReader reader6 = cmd6.ExecuteReader();
                        reader6.Read();
                        txtresultados.Text = "Tabla " + resultado + " eliminada.";
                        //mostrar elementos escondidos
                        listBox1.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //llamar a metodo esconder
                        esconder();

                        //cerrar instruccion para estar disponible y usar otra
                        reader6.Close();
                        muestrabases();
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();

                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }

                    dataGridView1.Hide();
                }


                else if (txtsentencia.Text.Contains("MUESTRA TABLAS") == true)
                {
                    MySqlCommand cmd7 = con.CreateCommand();
                    cmd7.CommandText = "show tables;";
                    try
                    {
                        MySqlDataReader reader7 = cmd7.ExecuteReader();

                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();

                        if (reader7.Read() == true)
                        {
                            do
                            {
                                string row = "";
                                for (int i = 0; i < reader7.FieldCount; i++)
                                {
                                    row += reader7.GetValue(i).ToString() + ", ";
                                    listBox2.Items.Add(row);

                                }

                            }
                            while (reader7.Read() == true);
                        }


                        else /*if (reader7.FieldCount == -1)*/
                        {
                            listBox2.Items.Clear();
                            listBox2.Hide();
                            MessageBox.Show("La Base de Datos no contiene ninguna Tabla.", "Revisar!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //mostrar elementos escondidos
                        listBox1.Show();
                        listBox2.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //llamar a metodo esconder
                        esconder();

                        //cerrar instruccion para estar disponible y usar otra
                        reader7.Close();

                    }
                    catch (MySqlException ex)
                    {
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                        txtresultados.Text = ex.Message;
                    }
                    dataGridView1.Hide();
                }


                else if (txtsentencia.Text.Contains("AGREGA REGISTRO") == true)
                {
                    resultado = cadenaTexto.Substring(cadenaTexto.IndexOf("REGISTRO") + 8, cadenaTexto.Length - cadenaTexto.IndexOf("REGISTRO") - 8);

                    MySqlCommand cmd8 = con.CreateCommand();
                    cmd8.CommandText = "insert into " + resultado + ";";

                    int countSpaces = resultado.Count(Char.IsWhiteSpace);
                    if (countSpaces > 2)
                    {
                        listBox2.Hide();
                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                        dataGridView1.Hide();
                    }

                    else
                    {/*try catch*/

                        try
                        {
                            MySqlDataReader reader8 = cmd8.ExecuteReader();
                            reader8.Read();
                            txtresultados.Text = "REGISTRO INGRESADO CON EXITO.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader8.Close();

                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();

                        }
                        catch (MySqlException ex)
                        {
                            txtresultados.Text = ex.Message;
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }
                        dataGridView1.Hide();
                    }

                }



                else if (txtsentencia.Text.Contains("BORRA REGISTRO DE") == true)
                {
                    string sentence = txtsentencia.Text;
                    string pattern = @"(?<before>\w+) DE (?<after>\w+)";
                    Match matches = Regex.Match(sentence, pattern);
                    String resul = matches.Groups[2].Value;

                    string pattern2 = @"DONDE (?<after>[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                    Match matches2 = Regex.Match(sentence, pattern2);
                    String resul2 = matches2.Groups[1].Value;

                    MySqlCommand cmd9 = con.CreateCommand();
                    cmd9.CommandText = "delete from " + resul + " where " + resul2 + ";";



                    int countSpaces = resul2.Count(Char.IsWhiteSpace);
                    if (countSpaces > 0)
                    {
                        listBox2.Hide();
                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                    }

                    else
                    {/*try catch*/

                        try
                        {
                            MySqlDataReader reader9 = cmd9.ExecuteReader();
                            reader9.Read();
                            txtresultados.Text = "Registro de la Tabla " + resul + " eliminado correctamente.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader9.Close();
                            muestrabases();
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();

                        }
                        catch (MySqlException ex)
                        {
                            txtresultados.Text = ex.Message;
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }

                        dataGridView1.Hide();
                    }
                }


                else if (txtsentencia.Text.Contains("MODIFICA REGISTRO") == true)
                {
                    string sentence = txtsentencia.Text;
                    string pattern = @"(?<before>\w+) REGISTRO (?<after>\w+)";
                    Match matches = Regex.Match(sentence, pattern);
                    String resul = matches.Groups[2].Value;

                    string pattern2 = @"(?<before>\w+) FIJAR (?<after>[a-zA-Z0-9=,'-]+)";
                    Match matches2 = Regex.Match(sentence, pattern2);
                    String resul2 = matches2.Groups[2].Value;

                    string pattern3 = @"DONDE (?<after>[a-zA-Z0-9='-\\s]+)";
                    Match matches3 = Regex.Match(sentence, pattern3);
                    String resul3 = matches3.Groups[1].Value;

                    MySqlCommand cmd10 = con.CreateCommand();
                    cmd10.CommandText = "update " + resul + " set " + resul2 + " where " + resul3 + ";";


                    try
                    {
                        MySqlDataReader reader10 = cmd10.ExecuteReader();
                        reader10.Read();
                        txtresultados.Text = "Modificación de la Tabla " + resul + " realizada correctamente.";
                        //mostrar elementos escondidos
                        listBox1.Show();
                        txtsentencia.Show();
                        lblsentencia.Show();
                        btexecute.Show();
                        //llamar a metodo esconder
                        esconder();

                        //cerrar instruccion para estar disponible y usar otra
                        reader10.Close();
                        muestrabases();
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();

                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }
                    dataGridView1.Hide();
                }


                else if (txtsentencia.Text.Contains("SELECCIONAR * DE") == true)
                {
                    dataGridView1.Show();
                    try
                    {
                        if (txtsentencia.Text.Contains("DONDE") == true)
                        {
                            string sentence = txtsentencia.Text;
                            string pattern = @"DE (?<after>\s*\w+)";
                            Match matches = Regex.Match(sentence, pattern);
                            String resul = matches.Groups[1].Value;

                            string pattern3 = @"DONDE (?<after>\s*[a-zA-Z0-9='-\\s_]+)";
                            Match matches3 = Regex.Match(sentence, pattern3);
                            String resul3 = matches3.Groups[1].Value;

                            string Query = "select * from " + resul + " where " + resul3 + ";";
                            MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                            MyAdapter.SelectCommand = MyCommand2;
                            DataTable dTable = new DataTable();
                            MyAdapter.Fill(dTable);
                            dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                        }
                        else
                        {
                            string sentence = txtsentencia.Text;
                            string pattern = @"DE (?<after>\s*\w+)";

                            Match matches = Regex.Match(sentence, pattern);
                            String resul = matches.Groups[1].Value;

                            string Query = "select * from " + resul + ";";
                            MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                            //For offline connection we weill use  MySqlDataAdapter class.
                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                            MyAdapter.SelectCommand = MyCommand2;
                            DataTable dTable = new DataTable();
                            MyAdapter.Fill(dTable);
                            dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        dataGridView1.Hide();
                        txtresultados.Clear();
                    }
                }



                else if (txtsentencia.Text.Contains("DESCRIBE") == true)
                {
                    dataGridView1.Show();
                    try
                    {
                        string sentence = txtsentencia.Text;
                        string pattern = @"DESCRIBE (?<after>\s*\w+)";
                        Match matches = Regex.Match(sentence, pattern);
                        String resul = matches.Groups[1].Value;

                        string Query = "describe " + resul + ";";
                        MySqlCommand MyCommand12 = new MySqlCommand(Query, con);

                        //For offline connection we weill use  MySqlDataAdapter class.
                        MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                        MyAdapter.SelectCommand = MyCommand12;
                        DataTable dTable = new DataTable();
                        MyAdapter.Fill(dTable);
                        dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        dataGridView1.Hide();
                        txtresultados.Clear();
                    }
                }





                else if (txtsentencia.Text.Contains("ALTERAR TABLA") == true)
                {
                    try
                    {
                        if (txtsentencia.Text.Contains("AGREGA") == true)
                        {
                            string sentence = txtsentencia.Text;
                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                            Match matches = Regex.Match(sentence, pattern);
                            String resul = matches.Groups[2].Value;

                            string pattern2 = @"AGREGA (?<after>\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                            Match matches2 = Regex.Match(sentence, pattern2);
                            String resul2 = matches2.Groups[1].Value;

                            MySqlCommand cmd13 = con.CreateCommand();
                            cmd13.CommandText = "alter table " + resul + " add " + resul2 + ";";

                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                            reader13.Read();
                            txtresultados.Text = "La columna " + resul + " se agrego correctamente.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader13.Close();
                            muestrabases();
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }

                        if (txtsentencia.Text.Contains("BORRA") == true)
                        {
                            string sentence = txtsentencia.Text;
                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                            Match matches = Regex.Match(sentence, pattern);
                            String resul = matches.Groups[2].Value;

                            string pattern2 = @"BORRA (?<after>\s*\w+)";
                            Match matches2 = Regex.Match(sentence, pattern2);
                            String resul2 = matches2.Groups[1].Value;

                            MySqlCommand cmd13 = con.CreateCommand();
                            cmd13.CommandText = "alter table " + resul + " drop " + resul2 + ";";


                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                            reader13.Read();
                            txtresultados.Text = "La columna " + resul + " se elimino correctamente.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader13.Close();
                            muestrabases();
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }


                        if (txtsentencia.Text.Contains("MODIFICA") == true)
                        {
                            string sentence = txtsentencia.Text;
                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                            Match matches = Regex.Match(sentence, pattern);
                            String resul = matches.Groups[2].Value;

                            string pattern2 = @"MODIFICA (?<after>\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                            Match matches2 = Regex.Match(sentence, pattern2);
                            String resul2 = matches2.Groups[1].Value;

                            MySqlCommand cmd13 = con.CreateCommand();
                            cmd13.CommandText = "alter table " + resul + " change " + resul2 + ";";


                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                            reader13.Read();
                            txtresultados.Text = "La columna " + resul + " se modifico correctamente.";
                            //mostrar elementos escondidos
                            listBox1.Show();
                            txtsentencia.Show();
                            lblsentencia.Show();
                            btexecute.Show();
                            //llamar a metodo esconder
                            esconder();

                            //cerrar instruccion para estar disponible y usar otra
                            reader13.Close();
                            muestrabases();
                            //limpiar listbox con las tablas...esconder
                            listBox2.Items.Clear();
                            listBox2.Hide();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        txtresultados.Text = ex.Message;
                        //limpiar listbox con las tablas...esconder
                        listBox2.Items.Clear();
                        listBox2.Hide();
                    }
                    dataGridView1.Hide();
                }

                else
                {
                    MessageBox.Show("Error de sintaxis.(Respetar espacios, solo se puede dejar un espacio entre palabras)", "UPS!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtresultados.Clear();
                    //limpiar listbox con las tablas...esconder
                    listBox2.Items.Clear();
                    listBox2.Hide();
                    dataGridView1.Hide();
                }
            }
            catch
            { }
            muestrabases();


        }

        /**
         * Metodo para mostrar base de datos actualizadas cuando se mande a llamar
         **/
        public void muestrabases()
        {
            try
            {
                //COMANDO PARA MOSTRAR BASES DE DATOS ACTUALIZADAS
                MySqlCommand cmd2 = con.CreateCommand();
                //actualizacion de la lista de base de datos
                cmd2.CommandText = "show databases";
                MySqlDataReader reader2 = cmd2.ExecuteReader();

                listBox1.Items.Clear();
                while (reader2.Read())
                {

                    string row = "";
                    for (int i = 0; i < reader2.FieldCount; i++)
                    {
                        row += reader2.GetValue(i).ToString();
                        listBox1.Items.Add(row);
                    }
                }
                reader2.Close();
            }
            catch (MySqlException ex)
            {
                txtresultados.Text = ex.Message;
            }

            //limpiar txtsentencia 
            txtsentencia.Clear();
        }


        /**
         * Este btn de "Importar archivo" contendra todas las condiciones necesarias para poder ejecutar los 
         * comandos de mysql.
         * 
         * Se importara un archivo txt donde este debe seguir cierto estandar para poder ejecutar los comandos mysql.
         **/
        private void btimportar_Click(object sender, EventArgs e)
        {
            Stream mystream;

            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Text|*.txt";

            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((mystream = openfile.OpenFile()) != null)
                {
                    string filename = openfile.FileName;


                    string[] filetext = File.ReadAllLines(filename);

                    for (var i = 0; i < filetext.Length; i++)
                    {
                        string MessageBoxTitle = "Desea ejecutar linea?";
                        string MessageBoxContent = "Se ejecutara la siguiente linea:" + filetext[i];
                        DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            txtsentencia.Text += filetext[i].ToUpper();
                            string resultado;

                            try
                            {
                                string cadenaTexto = txtsentencia.Text;

                                if (filetext[i].Contains("USA BD") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("BD") + 2, filetext[i].Length - filetext[i].IndexOf("BD") - 2);

                                    MySqlCommand cmd3 = con.CreateCommand();
                                    cmd3.CommandText = "use " + resultado.ToLower();
                                    try
                                    {
                                        MySqlDataReader reader3 = cmd3.ExecuteReader();
                                        reader3.Read();
                                        txtresultados.Text = "BD " + resultado + " en uso.";
                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader3.Close();

                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                        labelbdenuso.Text = "BD seleccionada:" + resultado;
                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }


                                    dataGridView1.Hide();
                                }

                                else if (filetext[i].Contains("CREA BD") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("BD") + 2, filetext[i].Length - filetext[i].IndexOf("BD") - 2);

                                    MySqlCommand cmd1 = con.CreateCommand();
                                    cmd1.CommandText = "create database " + resultado;
                                    try
                                    {
                                        MySqlDataReader reader1 = cmd1.ExecuteReader();
                                        reader1.Read();
                                        txtresultados.Text = "BD CREADA CON EXITO.";
                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader1.Close();

                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();

                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }

                                    dataGridView1.Hide();
                                }

                                else if (txtsentencia.Text.Contains("BORRA BD") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("BD") + 2, filetext[i].Length - filetext[i].IndexOf("BD") - 2);

                                    MySqlCommand cmd4 = con.CreateCommand();
                                    cmd4.CommandText = "drop database " + resultado;
                                    try
                                    {
                                        MySqlDataReader reader4 = cmd4.ExecuteReader();
                                        reader4.Read();
                                        txtresultados.Text = "BD " + resultado + " eliminada.";
                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader4.Close();

                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();

                                    }

                                    dataGridView1.Hide();
                                }

                                else if (filetext[i].Contains("CREA TABLA") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("TABLA") + 6, filetext[i].Length - filetext[i].IndexOf("TABLA") - 6);

                                    MySqlCommand cmd5 = con.CreateCommand();

                                    int countSpaces = resultado.Count(Char.IsWhiteSpace); //son 2

                                    if (countSpaces > 100)
                                    {
                                        listBox2.Hide();
                                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                                    }

                                    else
                                    {
                                        try
                                        {
                                            cmd5.CommandText = "create table " + resultado + ";";
                                            MySqlDataReader reader5 = cmd5.ExecuteReader();
                                            reader5.Read();
                                            txtresultados.Text = "Tabla " + resultado + " creada exitosamente.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader5.Close();
                                            muestrabases();
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();

                                        }
                                        catch (MySqlException ex)
                                        {
                                            txtresultados.Text = ex.Message;
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }

                                        dataGridView1.Hide();
                                    }
                                }

                                else if (filetext[i].Contains("BORRA TABLA") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("TABLA") + 5, filetext[i].Length - filetext[i].IndexOf("TABLA") - 5);

                                    MySqlCommand cmd6 = con.CreateCommand();
                                    cmd6.CommandText = "drop table " + resultado;
                                    try
                                    {
                                        MySqlDataReader reader6 = cmd6.ExecuteReader();
                                        reader6.Read();
                                        txtresultados.Text = "Tabla " + resultado + " eliminada.";
                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader6.Close();
                                        muestrabases();
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();

                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }

                                    dataGridView1.Hide();
                                }


                                else if (filetext[i].Contains("MUESTRA TABLAS") == true)
                                {
                                    MySqlCommand cmd7 = con.CreateCommand();
                                    cmd7.CommandText = "show tables;";
                                    try
                                    {
                                        MySqlDataReader reader7 = cmd7.ExecuteReader();

                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();

                                        //si no funciona solo quitar if y else
                                        if (reader7.Read() == true)
                                        {
                                            do
                                            {
                                                string row = "";
                                                for (int j = 0; j < reader7.FieldCount; j++)
                                                {
                                                    row += reader7.GetValue(j).ToString() + ", ";
                                                    listBox2.Items.Add(row);

                                                }

                                            }
                                            while (reader7.Read() == true);
                                        }


                                        else /*if (reader7.FieldCount == -1)*/
                                        {
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                            MessageBox.Show("La Base de Datos no contiene ninguna Tabla.", "Revisar!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        listBox2.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader7.Close();

                                    }
                                    catch (MySqlException ex)
                                    {
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                        txtresultados.Text = ex.Message;
                                    }

                                    dataGridView1.Hide();
                                }




                                else if (filetext[i].Contains("AGREGA REGISTRO") == true)
                                {
                                    resultado = filetext[i].Substring(filetext[i].IndexOf("REGISTRO") + 8, filetext[i].Length - filetext[i].IndexOf("REGISTRO") - 8);

                                    MySqlCommand cmd8 = con.CreateCommand();
                                    cmd8.CommandText = "insert into " + resultado + ";";

                                    int countSpaces = resultado.Count(Char.IsWhiteSpace); 
                                    if (countSpaces > 2)
                                    {
                                        listBox2.Hide();
                                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                                        dataGridView1.Hide();
                                    }

                                    else
                                    {/*try catch*/

                                        try
                                        {
                                            MySqlDataReader reader8 = cmd8.ExecuteReader();
                                            reader8.Read();
                                            txtresultados.Text = "REGISTRO INGRESADO CON EXITO.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            //comboBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader8.Close();

                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();

                                        }
                                        catch (MySqlException ex)
                                        {
                                            txtresultados.Text = ex.Message;
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }

                                        dataGridView1.Hide();
                                    }

                                }




                                else if (filetext[i].Contains("BORRA REGISTRO DE") == true)
                                {
                                    string sentence = filetext[i];
                                    string pattern = @"(?<before>\w+) DE (?<after>\w+)";
                                    Match matches = Regex.Match(sentence, pattern);
                                    String resul = matches.Groups[2].Value;

                                    string pattern2 = @"DONDE (?<after>[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                                    Match matches2 = Regex.Match(sentence, pattern2);
                                    String resul2 = matches2.Groups[1].Value;

                                    MySqlCommand cmd9 = con.CreateCommand();
                                    cmd9.CommandText = "delete from " + resul + " where " + resul2 + ";";



                                    int countSpaces = resul2.Count(Char.IsWhiteSpace);
                                    if (countSpaces > 0)
                                    {
                                        listBox2.Hide();
                                        txtresultados.Text = "ERROR DE SINTAXIS, REVISAR ESPACIOS.";
                                    }

                                    else
                                    {/*try catch*/

                                        try
                                        {
                                            MySqlDataReader reader9 = cmd9.ExecuteReader();
                                            reader9.Read();
                                            txtresultados.Text = "Registro de la Tabla " + resul + " eliminado correctamente.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader9.Close();
                                            muestrabases();
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();

                                        }
                                        catch (MySqlException ex)
                                        {
                                            txtresultados.Text = ex.Message;
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }

                                        dataGridView1.Hide();
                                    }
                                }







                                else if (filetext[i].Contains("MODIFICA REGISTRO") == true)
                                {
                                    string sentence = filetext[i];
                                    string pattern = @"(?<before>\w+) REGISTRO (?<after>\w+)";
                                    Match matches = Regex.Match(sentence, pattern);
                                    String resul = matches.Groups[2].Value;

                                    string pattern2 = @"(?<before>\w+) FIJAR (?<after>[a-zA-Z0-9=,'-]+)";
                                    Match matches2 = Regex.Match(sentence, pattern2);
                                    String resul2 = matches2.Groups[2].Value;

                                    string pattern3 = @"DONDE (?<after>[a-zA-Z0-9='-\\s]+)";
                                    Match matches3 = Regex.Match(sentence, pattern3);
                                    String resul3 = matches3.Groups[1].Value;

                                    MySqlCommand cmd10 = con.CreateCommand();
                                    cmd10.CommandText = "update " + resul + " set " + resul2 + " where " + resul3 + ";";


                                    try
                                    {
                                        MySqlDataReader reader10 = cmd10.ExecuteReader();
                                        reader10.Read();
                                        txtresultados.Text = "Modificación de la Tabla " + resul + " realizada correctamente.";
                                        //mostrar elementos escondidos
                                        listBox1.Show();
                                        txtsentencia.Show();
                                        lblsentencia.Show();
                                        btexecute.Show();
                                        //llamar a metodo esconder
                                        esconder();

                                        //cerrar instruccion para estar disponible y usar otra
                                        reader10.Close();
                                        muestrabases();
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();

                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }
                                    dataGridView1.Hide();
                                }


                                else if (filetext[i].Contains("SELECCIONAR * DE") == true)
                                {
                                    dataGridView1.Show();
                                    try
                                    {
                                        if (filetext[i].Contains("DONDE") == true)
                                        {
                                            string sentence = filetext[i];
                                            string pattern = @"DE (?<after>\s*\w+)";
                                            Match matches = Regex.Match(sentence, pattern);
                                            String resul = matches.Groups[1].Value;

                                            string pattern3 = @"DONDE (?<after>\s*[a-zA-Z0-9='-\\s_]+)";
                                            Match matches3 = Regex.Match(sentence, pattern3);
                                            String resul3 = matches3.Groups[1].Value;

                                            string Query = "select * from " + resul + " where " + resul3 + ";";
                                            MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                                            //For offline connection we weill use  MySqlDataAdapter class.
                                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                                            MyAdapter.SelectCommand = MyCommand2;
                                            DataTable dTable = new DataTable();
                                            MyAdapter.Fill(dTable);
                                            dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                                            // MyConn2.Close();
                                        }
                                        else
                                        {
                                            string sentence = filetext[i];
                                            string pattern = @"DE (?<after>\s*\w+)";

                                            Match matches = Regex.Match(sentence, pattern);
                                            String resul = matches.Groups[1].Value;

                                            string Query = "select * from " + resul + ";";
                                            MySqlCommand MyCommand2 = new MySqlCommand(Query, con);
                                            //  MyConn2.Open();
                                            //For offline connection we weill use  MySqlDataAdapter class.
                                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                                            MyAdapter.SelectCommand = MyCommand2;
                                            DataTable dTable = new DataTable();
                                            MyAdapter.Fill(dTable);
                                            dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                                            // MyConn2.Close();
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        dataGridView1.Hide();
                                        txtresultados.Clear();
                                    }
                                }



                                else if (filetext[i].Contains("DESCRIBE") == true)
                                {
                                    dataGridView1.Show();
                                    try
                                    {
                                        string sentence = filetext[i];
                                        string pattern = @"DESCRIBE (?<after>\s*\w+)";
                                        Match matches = Regex.Match(sentence, pattern);
                                        String resul = matches.Groups[1].Value;

                                        string Query = "describe " + resul + ";";
                                        MySqlCommand MyCommand12 = new MySqlCommand(Query, con);
                                        //  MyConn2.Open();
                                        //For offline connection we weill use  MySqlDataAdapter class.
                                        MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                                        MyAdapter.SelectCommand = MyCommand12;
                                        DataTable dTable = new DataTable();
                                        MyAdapter.Fill(dTable);
                                        dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.             
                                        // MyConn2.Close();
                                    }
                                    catch (MySqlException ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        dataGridView1.Hide();
                                    }
                                }
                          

                                else if (filetext[i].Contains("ALTERAR TABLA") == true)
                                {
                                    try
                                    {
                                        if (filetext[i].Contains("AGREGA") == true)
                                        {
                                            string sentence = filetext[i];
                                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                                            Match matches = Regex.Match(sentence, pattern);
                                            String resul = matches.Groups[2].Value;

                                            string pattern2 = @"AGREGA (?<after>\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                                            Match matches2 = Regex.Match(sentence, pattern2);
                                            String resul2 = matches2.Groups[1].Value;

                                            MySqlCommand cmd13 = con.CreateCommand();
                                            cmd13.CommandText = "alter table " + resul + " add " + resul2 + ";";


                                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                                            reader13.Read();
                                            txtresultados.Text = "La columna " + resul + " se agrego correctamente.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader13.Close();
                                            muestrabases();
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }

                                        if (filetext[i].Contains("BORRA") == true)
                                        {
                                            string sentence = filetext[i];
                                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                                            Match matches = Regex.Match(sentence, pattern);
                                            String resul = matches.Groups[2].Value;

                                            string pattern2 = @"BORRA (?<after>\s*\w+)";
                                            Match matches2 = Regex.Match(sentence, pattern2);
                                            String resul2 = matches2.Groups[1].Value;

                                            MySqlCommand cmd13 = con.CreateCommand();
                                            cmd13.CommandText = "alter table " + resul + " drop " + resul2 + ";";


                                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                                            reader13.Read();
                                            txtresultados.Text = "La columna " + resul + " se elimino correctamente.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            //comboBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader13.Close();
                                            muestrabases();
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }


                                        if (filetext[i].Contains("MODIFICA") == true)
                                        {
                                            string sentence = filetext[i];
                                            string pattern = @"(?<before>\w+) TABLA (?<after>\s*\w+)";
                                            Match matches = Regex.Match(sentence, pattern);
                                            String resul = matches.Groups[2].Value;

                                            string pattern2 = @"MODIFICA (?<after>\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+\s*[a-zA-Z0-9=,'-\\s]+)";
                                            Match matches2 = Regex.Match(sentence, pattern2);
                                            String resul2 = matches2.Groups[1].Value;

                                            MySqlCommand cmd13 = con.CreateCommand();
                                            cmd13.CommandText = "alter table " + resul + " change " + resul2 + ";";


                                            MySqlDataReader reader13 = cmd13.ExecuteReader();
                                            reader13.Read();
                                            txtresultados.Text = "La columna " + resul + " se modifico correctamente.";
                                            //mostrar elementos escondidos
                                            listBox1.Show();
                                            //comboBox1.Show();
                                            txtsentencia.Show();
                                            lblsentencia.Show();
                                            btexecute.Show();
                                            //llamar a metodo esconder
                                            esconder();

                                            //cerrar instruccion para estar disponible y usar otra
                                            reader13.Close();
                                            muestrabases();
                                            //limpiar listbox con las tablas...esconder
                                            listBox2.Items.Clear();
                                            listBox2.Hide();
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        txtresultados.Text = ex.Message;
                                        //limpiar listbox con las tablas...esconder
                                        listBox2.Items.Clear();
                                        listBox2.Hide();
                                    }
                                    dataGridView1.Hide();
                                }


                                else
                                {
                                    MessageBox.Show("Error de sintaxis.(Respetar espacios, solo se puede dejar un espacio entre palabras)", "UPS!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtresultados.Clear();
                                    //limpiar listbox con las tablas...esconder
                                    listBox2.Items.Clear();
                                    listBox2.Hide();
                                    dataGridView1.Hide();
                                }
                            }
                            catch
                            {
                            }
                            muestrabases();
                        }
                    }

                }
            }
        }
    }
}
