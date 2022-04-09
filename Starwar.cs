using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Linq;


namespace Swapi
{

    public class Starwar
    {

        public void CharactersMovies(string input)
        {

            using (var client = new HttpClient())
            {

                try
                {
                    Character deptObj = JsonConvert.DeserializeObject<Character>(input);
                    // Console.WriteLine(deptObj);
                    string character = deptObj.character;

                    var endPoint = new Uri($"https://swapi.dev/api/people?search={character}");
                    var result = client.GetAsync(endPoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;
                    // Console.WriteLine(json);




                    Root myItems = JsonConvert.DeserializeObject<Root>(json);
                    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
                    // Console.WriteLine($"testing: {myItems}");

                    foreach (var item in myItems.Results)
                    {

                        //Console.WriteLine($"{character}");
                        if (item.Name.Equals(character))
                        {
                            // Console.WriteLine($"{item.Name}");
                            List<MovieData> movieList = new List<MovieData>();
                            foreach (var movie in item.Films)
                            {
                                var endPointMovie = new Uri(movie);
                                var resultMovie = client.GetAsync(endPointMovie).Result;
                                var jsonMovie = resultMovie.Content.ReadAsStringAsync().Result;
                                // Console.WriteLine(jsonMovie);
                                Details detailsInfo = JsonConvert.DeserializeObject<Details>(jsonMovie);
                                // Console.WriteLine(detailsInfo.Title);
                                // Console.WriteLine(detailsInfo.EpisodeId);
                                // Console.WriteLine(detailsInfo.ReleaseDate);
                                movieList.Add(new MovieData { movie = detailsInfo.Title, episode = detailsInfo.EpisodeId, released = detailsInfo.ReleaseDate });
                            }
                            // Console.WriteLine(movieList);
                            movieList = movieList.OrderBy(x => x.released).ToList();
                            string movieResult = JsonConvert.SerializeObject(movieList);
                            File.WriteAllText(@"CharactersMovies.json", movieResult);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }

        }

        public class MovieData
        {
            public string movie { get; set; }
            public int episode { get; set; }
            public string released { get; set; }
        }

        public class Character
        {
            public string character { get; set; }

        }

        public void StarshipsInfo(string input)
        {
            using (var client = new HttpClient())


                try
                {

                    Starships shipsObj = JsonConvert.DeserializeObject<Starships>(input);
                    // Console.WriteLine(shipsObj);
                    String StarshipName = shipsObj.starship;
                    // Console.WriteLine(StarshipName);

                    var endPointShips = new Uri($"https://swapi.dev/api/starships?search={StarshipName}");
                    var resultShips = client.GetAsync(endPointShips).Result;
                    var jsonShips = resultShips.Content.ReadAsStringAsync().Result;
                    // Console.WriteLine($"jsonShips: {jsonShips}");


                    RootStarship myShips = JsonConvert.DeserializeObject<RootStarship>(jsonShips);
                    // Console.WriteLine($"myShips: {myShips}");

                    foreach (var myShip in myShips.ResultsStarship)
                    {
                        // Console.WriteLine($"{myShip.Name}");
                        // Console.WriteLine(name);
                        if (myShip.Name == StarshipName)
                        {

                            List<String> pilotList = new List<String>();
                            foreach (var pilot in myShip.Pilots)
                            {
                                var endPointPilot = new Uri(pilot);
                                var resultPilot = client.GetAsync(endPointPilot).Result;
                                var jsonPilot = resultPilot.Content.ReadAsStringAsync().Result;
                                RootPilot pilotObj = JsonConvert.DeserializeObject<RootPilot>(jsonPilot);
                                pilotList.Add(pilotObj.Name);
                            }
                            StarshipResult starshipResult = new StarshipResult { passengers = Int32.Parse(myShip.Passengers), pilots = pilotList };
                            string result = JsonConvert.SerializeObject(starshipResult);
                            File.WriteAllText(@"StarshipsInfo.json", result);
                        }
                    }




                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            // Console.WriteLine(input);


        }

        public class StarshipResult
        {
            public int passengers { get; set; }
            public List<String> pilots { get; set; }
        }

        public class Starships
        {
            public string starship { get; set; }

        }


    }






}







