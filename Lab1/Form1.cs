using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Lab1
{
    public partial class Form1 : Form
    {
        private string CurentFilePass = "";
        private string filePath = "";
        DialogResult result;

      

        public Form1()
        {

            InitializeComponent();
            Results.ReadOnly = true;
            
        }

        /// <summary>
        /// The history of the contents of the TextBox.
        /// </summary>
        private Stack<string> _editingHistory = new Stack<string>();

        /// <summary>
        /// The history of TextBox contents that have been undone and can be redone.
        /// </summary>
        private Stack<string> _undoHistory = new Stack<string>();


        private void RecordEdit()
        {
            _editingHistory.Push(Code.Text);
            Return.Enabled = true;
            отменитьToolStripMenuItem.Enabled = true;
            _undoHistory.Clear();
            Repeat.Enabled = false;
            повторитьToolStripMenuItem.Enabled = true;
        }

        private void Results_TextChanged(object sender, EventArgs e)
        {

        }

        private void Code_TextChanged(object sender, EventArgs e)
        {
            if (Code.Modified)
            {
                RecordEdit();
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Return_Click(object sender, EventArgs e)
        {
            if (_editingHistory.Count == 0)
            {
                MessageBox.Show("Заполните поля для работы данной функции");
            }

            else
            {
                _undoHistory.Push(_editingHistory.Pop());
                Repeat.Enabled = true;
                повторитьToolStripMenuItem.Enabled = true;
                Code.Text = _editingHistory.Peek();
                Return.Enabled = _editingHistory.Count > 1;
                отменитьToolStripMenuItem.Enabled = _editingHistory.Count > 1;
            }




           
        } 

        

        private void Repeat_Click(object sender, EventArgs e)
        {

            if (_editingHistory.Count == 0)
            {
                MessageBox.Show("Заполните поля для работы данной функции");
            }
            else
            {
                _editingHistory.Push(_undoHistory.Pop());
                Repeat.Enabled = _undoHistory.Count > 0;
                повторитьToolStripMenuItem.Enabled = _editingHistory.Count > 0;
                Code.Text = _editingHistory.Peek();
                Return.Enabled = true;
                отменитьToolStripMenuItem.Enabled = true;
            }
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Code.Text != "")

            {
                result = MessageBox.Show("Сохранить текущий файл?", "Сообщение", MessageBoxButtons.YesNo );
                 if (result == DialogResult.Yes)
                 {

                   
                    if (File.Exists(CurentFilePass))
                    {
                        string content = Code.Text;
                        File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
                    }

                    else
                    {
                        выходToolStripMenuItem_Click(sender, e);
                    }
                }
                
               

            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|cpp files (*.cpp)|*.cpp|h files (*.h)| *.h|cs files (*.sc)|*.cs|py files (*.py)|*.py";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                Code.Text = File.ReadAllText(openFile.FileName);
                CurentFilePass = filePath;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (Code.Text != "")

                {
                    result = MessageBox.Show("Сохранить текущий файл?", "Сообщение", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        
                        if (File.Exists(CurentFilePass))
                        {
                            string content = Code.Text;
                            File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
                        }

                        else
                        {
                            выходToolStripMenuItem_Click(sender, e);
                        }
                    }


                }
                Code.Clear();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (File.Exists(CurentFilePass))
            {
                string content = Code.Text;
                File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
            }

            else
            {
                выходToolStripMenuItem_Click(sender, e);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = ".txt";
            savefile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|cpp files (*.cpp)|*.cpp|h files (*.h)| *.h|cs files (*.sc)|*.cs|py files (*.py)|*.py";
           

            if (savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK && savefile.FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName, true))
                {
                    sw.WriteLine(Code.Text);
                    filePath = savefile.FileName;
                    
                    CurentFilePass = filePath;
                    sw.Close();
                }
            }
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_editingHistory.Count == 0)
            {
                MessageBox.Show("Заполните поля для работы данной функции");
            }

            else
            {

                _editingHistory.Push(_undoHistory.Pop());
                Repeat.Enabled = _undoHistory.Count > 0;
                повторитьToolStripMenuItem.Enabled = _editingHistory.Count > 0;
                Code.Text = _editingHistory.Peek();
                Return.Enabled = true;
                отменитьToolStripMenuItem.Enabled = true;
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.Paste();

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Code.Text != "")

            {
                result = MessageBox.Show("Сохранить текущий файл?", "Сообщение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    
                    if (File.Exists(CurentFilePass))
                    {
                        string content = Code.Text;
                        File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
                    }

                    else
                    {
                        выходToolStripMenuItem_Click(sender, e);
                    }
                }


            }

            Close();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.SelectedText = "";

        }

    private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.SelectAll();

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start_Click(sender, e);
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (Code.Text != "")

            {
                result = MessageBox.Show("Сохранить текущий файл?", "Сообщение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                   
                    if (File.Exists(CurentFilePass))
                    {
                        string content = Code.Text;
                        File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
                    }

                    else
                    {
                        выходToolStripMenuItem_Click(sender, e);
                    }
                }


            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|cpp files (*.cpp)|*.cpp|h files (*.h)| *.h|cs files (*.sc)|*.cs|py files (*.py)|*.py";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                Code.Text = File.ReadAllText(openFile.FileName);
                CurentFilePass = filePath;
            }
        }

        private void Save_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(CurentFilePass))
            {
                string content = Code.Text;
                File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
            }

            else
            {
                выходToolStripMenuItem_Click(sender, e);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            {
                if (Code.Text != "")

                {
                    result = MessageBox.Show("Сохранить текущий файл?", "Сообщение", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                       
                        if (File.Exists(CurentFilePass))
                        {
                            string content = Code.Text;
                            File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
                        }

                        else
                        {
                            выходToolStripMenuItem_Click(sender, e);
                        }
                    }


                }

                Code.Clear();
            }
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                Process.Start(@"Reference2\Reference.html");
                
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About the program");
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (_editingHistory.Count == 0)
                {
                    MessageBox.Show("Заполните поля для работы данной функции");
                }

                else
                {
                    _undoHistory.Push(_editingHistory.Pop());
                    Repeat.Enabled = true;
                    повторитьToolStripMenuItem.Enabled = true;
                    Code.Text = _editingHistory.Peek();
                    Return.Enabled = _editingHistory.Count > 1;
                    отменитьToolStripMenuItem.Enabled = _editingHistory.Count > 1;

                }
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            Code.Copy();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Code.Cut();
        }

        private void Reference_Click(object sender, EventArgs e)
        {
            Process.Start(@"Reference2\Reference.html");
        }
        private void Compiler_Load(object sender, EventArgs e)
        {

        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            Code.Font = new Font(Code.Font.FontFamily, int.Parse(FontBox.SelectedItem.ToString()));
            Results.Font = new Font(Results.Font.FontFamily, int.Parse(FontBox.SelectedItem.ToString()));

        } 

        private void Start_Click(object sender, EventArgs e)
        {
            Results.Text = "";

            string[] str = Code.Text.Split(new char[] { '\n' });

            string[] MathSym = { "/", "(", ")", "-", "+", "*", "="};

            string[] NoName = { ":", "?", ".", ",", "{", "}", "[", "]", ";"};

            string[] TypeData = { "int", "boolean", "char", "String", "true", "false", "double" };

            string[] Digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            bool Error;

            string sub;

            string repl;

            int way, space_way, step;

            for (int i = 0; i < str.Length; i++)

            {
                way = 0;
                space_way = 0;
                step = 0;
                sub = str[i];
                str[i] = str[i].Replace(";\r", ";");
                if (str[i].Contains(";") && str[i].Contains(" ;") == false)
                str[i] = str[i].Replace(";", " ;");


               for (int m = 0; m < MathSym.Length; m++)

                {

                    repl = " " + MathSym[m] + " ";

                    str[i] = str[i].Replace(MathSym[m], repl);

                } 

                Results.Text += " Строка " + i + "\r\n";

                string[] words = str[i].Split(new char[] { ' ' });

               

                for (int j = 0; j < words.Length; j++)

                {

                    if (TypeData.Contains(words[j]))

                    {
                        
                      
                        Results.Text += " " + (Array.IndexOf(TypeData, words[j]) + 1) + " - ключевое слово - " + words[j] +  " - с " + str[i].IndexOf((words[j]), way) + " по " + (str[i].IndexOf(words[j], way) + (words[j].Length - 1)) + " символ " + "\r\n";
                        
                    }

                  

                    else if( words[j].Contains('=') == true )
                    {
                        Results.Text += " 10 - оператор присваивания - " + words[j] + " - с " + (str[i].IndexOf((words[j]), way) - 1) + " по " + (str[i].IndexOf(words[j], way) - 1) + " символ " + "\r\n";

                    }

                 
                 
                    else if (words[j][0] == '"')

                    {
                        if (words[j][words[j].Length - 1] == '"')

                        Results.Text += " 11 - строка - " + words[j] + " - с " + ((str[i].IndexOf(words[j], way)) -2) + " по " + ((str[i].IndexOf(words[j], way) + (words[j].Length - 1))-2) + " символ " + "\r\n";

                    }

                    else if (words[j][0] == '\'' && words[j][words[j].Length - 1] == '\'')

                    {

                        Results.Text += " 12 - символ - " + words[j] + " - с " + ((str[i].IndexOf(words[j], way)) -2) + " по " + ((str[i].IndexOf(words[j], way) + (words[j].Length - 1)) -2) + " символ " + "\r\n";

                    }

                    else if (words[j].All(char.IsDigit))

                    {

                        Results.Text +=  " 13 - целое число - " + words[j] + " - с " + ((str[i].IndexOf((words[j]), way))-2) + " по " + ((str[i].IndexOf(words[j], way) + (words[j].Length-1))-2 ) + " символ " + "\r\n";

                    }

                     else if (words[j].Contains('.') && Char.IsDigit(words[j][0]) == true)

                    {

                        string test = words[j].Replace(".", "");

                        if (test.All(char.IsDigit))

                        {

                            Results.Text += " 14 - вещественное число - " + words[j] + " - с " + (str[i].IndexOf((words[j]), way) - 2) + " по " + ((str[i].IndexOf(words[j], way) + (words[j].Length - 1)) - 2) + " символ " + "\r\n";
                        }

                    } 

                    else if (words[j].Contains(';') == true && words[j].Length == 1)

                    {

                        Results.Text += " 16 - конец оператора - ; " + " - с " + ((str[i].IndexOf((words[j]), way) - 1) - 2) + " по " + ((str[i].IndexOf(words[j], way) - 1) -2) + " символ " + "\r\n";

                    }

                    else if (words[j].Contains("\\n") == true)
                    {
                        Results.Text += " 15 - конец строки - \\n " + " - с " + ((str[i].IndexOf((words[j]), way) - 1) - 2) + " по " + ((str[i].IndexOf(words[j], way) - 1) - 2) + " символ " + "\r\n";

                    }

                   else
                    {

                       

                        Error = false;
                        for (int c = 0; c < NoName.Length; c++)

                        {

                            if (words[j].Contains(NoName[c]))

                            {

                                Error = true;

                            }
                        }

                        if (Char.IsDigit(words[j][0]))
                        {
                            Error = true;
                        }

                        if (Error == false)
                        {

                            Results.Text += " 5 - идентификатор - " + words[j] + " - с " + (str[i].IndexOf((words[j]), way) ) + " по " + ((str[i].IndexOf(words[j], way) + (words[j].Length - 1)) ) + " символ " + "\r\n";
                        }

                        else {
                            Results.Text += " Error недопустимый символ в названии переменной " + "\r\n";
                        }

                        if (sub.Contains(" "))

                        {

                            Results.Text += " 9 - разделитель - c " + ((sub.IndexOf(" ", 0)) + step) + " по " + ((sub.IndexOf(" ", 0)) + step) + " символ " + "\r\n";

                            step = step + (sub.IndexOf(" ", 0) + 1);
                            sub = sub.Substring(sub.IndexOf(" ", space_way) + 1);


                        } 

                    }
                   
                    way = way + System.Convert.ToInt32(words[j].Length);
                   







                }

                    }

            }

        }

    }

                            

                        



                   
