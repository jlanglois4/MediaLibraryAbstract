using System;
using System.Collections.Generic;
using System.IO;

namespace MediaLibraryAbstract
{
    public class Movie : Media
    {
        private static string movieFilePath = @"MediaRepository\movies.csv";
        private static MediaFile movieFile = new(movieFilePath);

        public Movie()
        {
            genre = new List<string>();
        }

        public override string Display()
        {
            return $"ID: {mediaID}\n" +
                   $"Title: {title}\n" +
                   $"Genres: {string.Join(" | ", genre)}\n";
        }

        public override void addMedia()
        {
            Media movie = new Movie();
            Console.WriteLine("Enter movie title");
            movie.title = Console.ReadLine();
            if (movieFile.TestTitle(movie.title))
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter genre\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter genre: ");
                            string genreInput = Console.ReadLine();

                            if (genreInput != "")
                            {
                                movie.genre.Add(genreInput);
                            }
                            else
                            {
                                Console.Write("Please enter a genre.");
                            }

                            break;
                        case "2":
                            Console.WriteLine("Exit");
                            if (movie.genre.Count == 0)
                            {
                                movie.genre.Add("N/A");
                            }

                            b = false;
                            break;
                    }
                }

                movieFile.AddMovie(movie);
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }

        public override void listMedia()
        {
            List<String> list = new();
            foreach (Movie m in movieFile.media)
            {
                list.Add(m.Display());
            }

            int index = 0;
            int anotherTen = 10;
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

        public override void InitializeList()
        {
            if (!File.Exists(movieFilePath))
            {
                StreamWriter streamWriter = new(movieFilePath, true);
                streamWriter.WriteLine("movieID,title,genres");
                streamWriter.Close();
            }
        }
    }
}