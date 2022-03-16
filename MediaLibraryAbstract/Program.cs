using System;

namespace MediaLibraryAbstract
{
    internal class Program
    {
        private static string pickedChoice;

        // Allows selection for the user to choose from
        public static void Main(string[] args)
        {
            var choice = true;
            do
            {
                Console.WriteLine(
                    "Welcome to the Media Library. Please pick a form of media.\n1. Movies.\n2. Shows.\n3. Videos.\nEnter anything else to exit the Media Library.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        MovieSelection();
                        break;
                    case "2":
                        ShowSelection();
                        break;
                    case "3":
                        VideoSelection();
                        break;
                    default:
                        Console.WriteLine("Thank you for using the Media Library.");
                        choice = false;
                        break;
                }
            } while (choice);
        }

        // Allows you to enter an option for Main to run
        private static void MovieSelection()
        {
            Media movie = new Movie();
            var choice = true;
            do
            {
                Console.WriteLine("1. List movies.\n2. Add movie.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        movie.listMedia();
                        break;
                    case "2":
                        movie.addMedia();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }

        private static void ShowSelection()
        {
            Media show = new Show();
            var choice = true;
            do
            {
                Console.WriteLine("1. List shows.\n2. Add show.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        show.listMedia();
                        break;
                    case "2":
                        show.addMedia();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }

        private static void VideoSelection()
        {
            Media video = new Video();
            var choice = true;
            do
            {
                Console.WriteLine($"1. List videos.\n2. Add video.\nEnter anything else to exit.");
                pickedChoice = Console.ReadLine();
                switch (pickedChoice)
                {
                    case "1":
                        video.listMedia();
                        break;
                    case "2":
                        video.addMedia();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }
    }
}