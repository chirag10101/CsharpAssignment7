using System.Text;

namespace CsharpAssignment7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? fname="";
            string root = @"C:\demo";
            string subroot1 = @"C:\demo\demo1";
            string subroot2 = @"C:\demo\demo2";
            string filePath1 = @"C:\demo\demo1\MyTest1.txt";
            string filePath2 = @"C:\demo\demo2\MyTest2.txt";

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            else
                Console.WriteLine(root + " Already Exists !");

            if (!Directory.Exists(subroot1))
                Directory.CreateDirectory(subroot1);
            else
                Console.WriteLine(subroot1 + " Already Exists !");

            if (!Directory.Exists(subroot2))
                Directory.CreateDirectory(subroot2);
            else
                Console.WriteLine(subroot2 + " Already Exists !");

            if (!File.Exists(filePath1))
                File.Create(filePath1);
            else
                Console.WriteLine(filePath1 + " File already exists !");
            
            FileInfo fi = new FileInfo(filePath2);
            if (fi.Exists)
                Console.WriteLine(filePath2 +" File already Exists!");
            else
                fi.Create();

            File.WriteAllText(filePath1, "Hello world");

            try
            {
                File.Copy(filePath1, subroot2 + @"\CopiedMyTest1.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Displaying all directories and files in demo folder");

            string[] directories = Directory.GetDirectories(root, "*" , SearchOption.AllDirectories);
            Console.WriteLine("Directories:");
            foreach (string directory in directories)
            {
                Console.WriteLine(directory);
            }

            // Get all files
            string[] files = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            Console.WriteLine("\nFiles:");
            foreach (string file in files)
            {
                Console.WriteLine(file);
                Console.WriteLine(File.GetCreationTime(file).ToString());
            }

            //Deleting files
            //foreach (string file in files)
            //{
            //    try
            //    {
            //        File.Delete(file);
            //    }catch(Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}

            Console.WriteLine("Do you want to delete all files ? 1.Yes 2. No");
            byte c = byte.Parse(Console.ReadLine());
            //Deleting all files and directories
            if(c == 1)
            {
                foreach (string directory in directories)
                {
                    try
                    {
                        Directory.Delete(directory, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            bool flag=true;
            while(flag==true)
            {
                Console.WriteLine("Enter choice :");
                Console.WriteLine("1. Create a new file\r\n2. Add contents to the file\r\n3. Append contents to the file\r\n4. Display contents one by opne\r\n5. Display all contents together \r\n6. Exit");
                try
                {
                    byte choice = Byte.Parse(Console.ReadLine());
                    switch(choice)
                    {
                        case 1:
                            Console.WriteLine("Enter File Name: ");
                             fname= Console.ReadLine();

                            try
                            {
                                FileStream fs = File.Create(root + @"/" + fname);
                                fs.Close();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            break;
                        case 2:
                            try
                            {
                                FileStream f = new FileStream(root + @"\" + fname, FileMode.OpenOrCreate);
                                StreamWriter s = new StreamWriter(f);
                                string str = Console.ReadLine();
                                s.WriteLine(str);
                                s.Close();
                                f.Close();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                using (StreamWriter sw = File.AppendText(root + @"\" + fname))
                                {
                                    sw.Write("Hello this is appended text");
                                    sw.Close();
                                }
                               
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 4:
                            if (File.Exists(root + @"\" + fname))
                            {
                                // Reads file line by line 
                                StreamReader Textfile = new StreamReader(root + @"\" + fname);
                                string line;

                                while ((line = Textfile.ReadLine()) != null)
                                {
                                    Console.WriteLine(line);
                                }

                                Textfile.Close();
                            }
                                break;
                        case 5:
                            if (File.Exists(root + @"\" + fname))
                            {
                                // Read all the content in one string 
                                // and display the string 
                                string str = File.ReadAllText(root + @"\" + fname);
                                Console.WriteLine(str);
                            }
                            else
                            {
                                Console.WriteLine("File do not Exists");
                            }
                            break;
                        case 6:
                            flag = false;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Enter valid Choice");
                }
            }
        }
    }
}
