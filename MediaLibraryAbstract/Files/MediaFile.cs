using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaLibraryAbstract
{
    public class MediaFile
    {
        private string filePath { get; set; }
        public List<Media> media { get; set; }

        private Movie movie;
        private Show show;
        private Video video;

        private static IServiceCollection serviceCollection = new ServiceCollection();

        private static ServiceProvider serviceProvider =
            serviceCollection.AddLogging(x => x.AddConsole()).BuildServiceProvider();

        private static ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

        public MediaFile(string path)
        {
            filePath = path;
            media = new List<Media>();
            ////////////////////////MOVIES//////////////////////////
            if (path == @"MediaRepository\movies.csv")
            {
                try
                {
                    StreamReader streamReader = new(filePath);
                    streamReader.ReadLine();
                    while (!streamReader.EndOfStream)
                    {
                        Media movie = new Movie();
                        string entry = streamReader.ReadLine();

                        int quote = entry.IndexOf('"') - 1;
                        if (quote == 1)
                        {
                            entry = entry.Replace('"', ' ');
                        }

                        if (entry != "")
                        {
                            string[] movieDetails = entry.Split(',');
                            movie.mediaID = int.Parse(movieDetails[0]);
                            movie.title = movieDetails[1].Trim();
                            movie.genre = movieDetails[2].Split('|').ToList();
                            media.Add(movie);
                        }
                    }

                    streamReader.Close();
                    logger.LogInformation("Movies in file {movieCount}", media.Count);
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    Console.WriteLine("File not found.");
                }
            }

            //////////////////////////////SHOWS//////////////////////////////
            if (path == @"MediaRepository\shows.csv")
            {
                try
                {
                    StreamReader streamReader = new(filePath);
                    streamReader.ReadLine();
                    while (!streamReader.EndOfStream)
                    {
                        Show show = new();
                        string entry = streamReader.ReadLine();

                        int quote = entry.IndexOf('"') - 1;
                        if (quote == 1)
                        {
                            entry = entry.Replace('"', ' ');
                        }

                        if (entry != "")
                        {
                            string[] showDetails = entry.Split(',');
                            show.mediaID = int.Parse(showDetails[0]);
                            show.title = showDetails[1].Trim();
                            show.season = Convert.ToInt32(showDetails[2]);
                            show.season = Convert.ToInt32(showDetails[3]);
                            show.writers = showDetails[4].Split('|').ToList();
                            media.Add(show);
                        }
                    }

                    streamReader.Close();
                    logger.LogInformation("Shows in file {showCount}", media.Count);
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    Console.WriteLine("File not found.");
                }
            }
            
            /////////////////////////////VIDEOS///////////////////////////
            if (path == @"MediaRepository\videos.csv")
            {
                try
                {
                    StreamReader streamReader = new(filePath);
                    streamReader.ReadLine();
                    while (!streamReader.EndOfStream)
                    {
                        Video video = new();
                        string entry = streamReader.ReadLine();

                        int quote = entry.IndexOf('"') - 1;
                        if (quote == 1)
                        {
                            entry = entry.Replace('"', ' ');
                        }

                        if (entry != "")
                        {
                            string[] videoDetails = entry.Split(',');
                            video.mediaID = int.Parse(videoDetails[0]);
                            video.title = videoDetails[1].Trim();
                            video.format = videoDetails[2];
                            video.length = Convert.ToInt32(videoDetails[3]);

                            int commaCount = 0;
                            int region;
                            var regionString = videoDetails[4];
                            for (int i = 0; i < videoDetails[4].Length; i++)
                            {
                                if (videoDetails[4].Contains(','))
                                {
                                    int index = regionString.IndexOf("|");
                                    region = Convert.ToInt32(regionString.Remove(index, regionString.Length - index));
                                    video.regions.Add(region);
                                    regionString = videoDetails[4].Remove(0, region + 1);
                                }
                            }

                            media.Add(video);
                        }
                    }

                    streamReader.Close();
                    logger.LogInformation("Videos in file {videoCount}", media.Count);
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    Console.WriteLine("File not found.");
                }
            }
        }

        public void AddMovie(Media movie)
        {
            try
            {
                movie.mediaID = media.Max(mov => mov.mediaID) + 1;

                string title;
                if (movie.title.IndexOf(',') != -1)
                {
                    title = $"\"{movie.title}\"";
                }
                else
                {
                    title = movie.title;
                }

                StreamWriter streamWriter = new(filePath, true);
                streamWriter.Flush();
                streamWriter.WriteLine($"{movie.mediaID},{title},{string.Join("|", movie.genre)}");
                streamWriter.Close();
                media.Add(movie);

                logger.LogInformation("Movie id {movieID} added", movie.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.StackTrace);
                Console.WriteLine("Error adding movie");
            }
        }

        public void AddShow(Media show)
        {
            if (media.Count == 0)
            {
                show.mediaID = 1;
            }
            else
            {
                show.mediaID = media.Max(media => media.mediaID) + 1;
            }

            try
            {
                string title;
                if (show.title.IndexOf(',') != -1)
                {
                    title = $"\"{show.title}\"";
                }
                else
                {
                    title = show.title;
                }

                StreamWriter streamWriter = new(filePath, true);
                streamWriter.WriteLine(
                    $"{show.mediaID},{title},{show.season},{show.episode},{string.Join("|", show.writers)}");
                streamWriter.Close();
                media.Add(show);

                logger.LogInformation("Show ID {showID} added", show.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("Error adding show");
            }
        }


        public void AddVideo(Video video)
        {
            if (media.Count == 0)
            {
                video.mediaID = 1;
            }
            else
            {
                video.mediaID = media.Max(media => media.mediaID) + 1;
            }

            try
            {
                string title;
                if (video.title.IndexOf(',') != -1)
                {
                    title = $"\"{video.title}\"";
                }
                else
                {
                    title = video.title;
                }

                StreamWriter streamWriter = new(filePath, true);
                streamWriter.WriteLine(
                    $"{video.mediaID},{title},{video.format},{video.length},{string.Join("|", video.regions)}");
                streamWriter.Close();
                media.Add(video);

                logger.LogInformation("Video ID {videoID} added", video.mediaID);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("Error adding video");
            }
        }


        public bool TestTitle(string title)
        {
            var mediaTitle = title.Replace('"', ' ').Trim().ToLower();
            if (media != null)
            {
                if (media.ConvertAll(mov => mov.title.ToLower()).Contains(mediaTitle))
                {
                    logger.LogInformation("Duplicate media title {title}", title);
                    return false;
                }
            }

            return true;
        }
    }
}