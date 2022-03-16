using System;
using System.Collections.Generic;
using System.IO;

namespace MediaLibraryAbstract
{
    public class Show : Media
    {
        private static string showFilePath = @"MediaRepository\shows.csv";
        private static MediaFile showFile = new(showFilePath);

        public Show()
        {
            writers = new List<string>();
        }

        public override string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Season: {season}\n" +
                   $"Episode: {episode}\n" +
                   $"Writers: {string.Join(" | ", writers)}\n";
        }


        public override void addMedia()
        {
            InitializeList();
            Show show = new Show();
            Console.WriteLine("Enter show title");
            show.title = Console.ReadLine();

            if (showFile.TestTitle(show.title))
            {
                do
                {
                    Console.WriteLine("Enter the show's season");
                    try
                    {
                        show.season = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid season number.");
                        show.season = null;
                    }
                } while (show.season == null);

                do
                {
                    Console.WriteLine("Enter the show's episode");
                    try
                    {
                        show.episode = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid season number.");
                        show.episode = null;
                    }
                } while (show.episode == null);

                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter writer\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter writer: ");
                            string writerInput = Console.ReadLine();

                            if (writerInput != "")
                            {
                                show.writers.Add(writerInput);
                            }
                            else
                            {
                                Console.Write("Please enter a writer.");
                            }

                            break;
                        case "2":
                            if (show.writers.Count == 0)
                            {
                                show.writers.Add("N/A");
                            }

                            Console.WriteLine("Exit");
                            b = false;
                            break;
                    }
                }

                showFile.AddShow(show);
            }
        }

        public override void listMedia()
        {
            InitializeList();
            List<String> list = new();
            foreach (Show m in showFile.media)
            {
                list.Add(m.Display());
            }

            int index = 0;
            int anotherTen = 10;

            if (list.Count == 0)
            {
                Console.WriteLine("There are no shows.");
            }
            else
            {
                while (list.Count != index)
                {
                    if (index < (list.Count - 10))
                    {
                        for (int i = index; i < (anotherTen); i++)
                        {
                            Console.WriteLine(list[i]);
                            index += 1;
                        }

                        anotherTen = index + 10;
                        Console.WriteLine("Enter 1 to exit. Enter anything else to continue.");
                        var lineRead = Console.ReadLine();

                        if (lineRead.Equals("1"))
                        {
                            index = list.Count;
                            Console.WriteLine("Exit.");
                        }
                        else
                        {
                            Console.WriteLine("Continue.");
                        }
                    }
                    else
                    {
                        anotherTen = (list.Count - index);
                        for (int i = 0; i < anotherTen; i++)
                        {
                            Console.WriteLine(list[i + index]);
                        }

                        index = list.Count;
                    }
                }
            }
        }

        public override void InitializeList()
        {
            if (!File.Exists(showFilePath))
            {
                Console.WriteLine("Creating new file.\nPress anything to continue.");
                StreamWriter streamWriter = new(showFilePath, true);
                streamWriter.WriteLine("showID,title,season,episode,writers");
                streamWriter.Close();
                Console.ReadLine();
            }
        }
    }
}