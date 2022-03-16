using System;
using System.Collections.Generic;
using System.IO;

namespace MediaLibraryAbstract
{
    public class Video : Media
    {
        private static string videoFilePath = @"MediaRepository\videos.csv";
        private static MediaFile videoFile = new(videoFilePath);

        public Video()
        {
            regions = new List<int>();
        }

        public override string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Format: {format}\n" +
                   $"Length: {length}\n" +
                   $"Regions: {string.Join(" | ", regions)}\n";
        }


        public override void addMedia()
        {
            InitializeList();
            Video video = new();
            Console.WriteLine("Enter video title");
            video.title = Console.ReadLine();
            if (videoFile.TestTitle(video.title))
            {
                do
                {
                    Console.WriteLine("Enter the video's format");
                    try
                    {
                        video.format = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid video format.");
                        video.format = null;
                    }
                } while (video.format == null);

                do
                {
                    Console.WriteLine("Enter the video's length");
                    try
                    {
                        video.length = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid video length.");
                        video.length = null;
                    }
                } while (video.length == null);

                bool b = true;

                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter region\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter region: ");

                            if (!int.TryParse(Console.ReadLine(), out int regionInput))
                            {
                                Console.Write("Please enter a region.");
                            }
                            else
                            {
                                video.regions.Add(regionInput);
                            }

                            break;
                        case "2":
                            Console.WriteLine("Exit");
                            if (video.regions.Count == 0)
                            {
                                video.regions.Add(0);
                            }

                            b = false;
                            break;
                    }
                }

                videoFile.AddVideo(video);
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }

        public override void listMedia()
        {
            InitializeList();
            List<String> list = new();
            foreach (Video m in videoFile.media)
            {
                list.Add(m.Display());
            }

            int index = 0;
            int anotherTen = 10;

            if (list.Count == 0)
            {
                Console.WriteLine("There are no videos.");
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
            if (!File.Exists(videoFilePath))
            {
                Console.WriteLine("Creating new file.\nPress anything to continue.");
                StreamWriter streamWriter = new(videoFilePath, true);
                streamWriter.WriteLine("videoID,title,format,length,regions");
                streamWriter.Close();
                Console.ReadLine();
            }
        }
    }
}