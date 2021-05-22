using System;
using System.Collections.Generic;
using System.IO;
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

namespace ini_Profiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string that holds the filepath for the profile.ini
        string filepathString;

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(@"filepaths.txt"))
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(@"filepaths.txt").ToList();
                
                if (lines.Count > 0)
                {
                   filepathTextBox.Text = lines[0];
                }
                
            }
            


            filepathString = filepathTextBox.Text.ToString();  //get the path
        }


        /// <summary>
        /// method that saves the FOV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fovButton_Click(object sender, RoutedEventArgs e)
        {

            bool isValid = int.TryParse(fovTextBox.Text.ToString(), out _);

            if (isValid && fovTextBox.Text.ToString() != "" && filepathTextBox.Text.ToString() != "")  //if a valid input is given in the text box
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepathString).ToList();

                int i = 0;

                foreach (string line in lines)   //itterate through each line to find the desired setting
                {
                    if (line.Contains("fov="))
                    {
                        lines.Remove(line);  //remove the old line
                        lines.Insert(i, "fov=" + fovTextBox.Text.ToString());   //insert the new modified line
                        break;
                    }
                    else 
                    {
                        i++;  //itterate so we can insert at the proper place
                    }
                }

                File.WriteAllText(filepathString, String.Empty);  //easier to erase all text and replace with the profile from the list instead of changing an individual line

                File.WriteAllLines(filepathString, lines); //restore the profile with the new text line


            }

            if (!isValid)
            {
                MessageBox.Show("Invalid Input. Please Enter A Proper Value.");
            }

            if (fovTextBox.Text.ToString() == "")
            {
                MessageBox.Show("Invalid Input. Please Enter A Proper Value.");
            }

            if (filepathTextBox.Text.ToString() == "")
            {
                MessageBox.Show("Please Enter the Filepath for your profile.ini.");
            }




        }

        



        /// <summary>
        /// get the filepath and store it in the text file for future use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filepathButton_Click(object sender, RoutedEventArgs e)
        {
            filepathString = filepathTextBox.Text.ToString();  //get the filepath from the textbox
            File.WriteAllText(@"filepaths.txt", String.Empty);          //delete old filepath
            
            List<string> lines = new List<string>();
            lines.Add(filepathString);

            File.WriteAllLines(@"filepaths.txt", lines);



        }

        /// <summary>
        /// same process as FOV, just modifying the erode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void erodeButton_Click(object sender, RoutedEventArgs e)
        {

            bool isValid = float.TryParse(erodeTextBox.Text.ToString(), out _);

            if (isValid && erodeTextBox.Text.ToString() != "" && filepathTextBox.Text.ToString() != "")  //if a valid input is given in the text box
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepathString).ToList();

                int i = 0;
               

                foreach (string line in lines)   //itterate through each line to find the desired setting
                {
                    if (line.Contains("[testing]"))
                    {
                        //lines.Remove(line);  //remove the old line
                        lines.Insert(i + 1, "deformation_multiplier=" + erodeTextBox.Text.ToString());   //insert the new modified line
                       
                        break;
                    }
                    else
                    {
                        i++;  //itterate so we can insert at the proper place
                    }
                }

                int count = 0;
                foreach(string line in lines)
                {
                    if (line.Contains("deformation_multiplier"))
                    {
                        if (count > 0)
                        {
                            lines.Remove(line);  //remove the old line
                            break;
                        }
                        else 
                        { 
                            count++; 
                        }
                    }
                    
                }

                File.WriteAllText(filepathString, String.Empty);  //easier to erase all text and replace with the profile from the list instead of changing an individual line

                File.WriteAllLines(filepathString, lines); //restore the profile with the new text line


            }

            if (!isValid)
            {
                MessageBox.Show("Invalid Input. Please Enter A Proper Value.");
            }

            if (erodeTextBox.Text.ToString() == "")
            {
                MessageBox.Show("Invalid Input. Please Enter A Proper Value.");
            }

            if (erodeTextBox.Text.ToString() == "")
            {
                MessageBox.Show("Please Enter the Filepath for your profile.ini.");
            }



        }
    }
}
