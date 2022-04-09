using System;

namespace Swapi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("test");
            Starwar starwar = new Starwar();
            starwar.CharactersMovies("{\"character\":\"Luke Skywalker\"}");
            starwar.StarshipsInfo("{\"starship\":\"Millennium Falcon\"}");




        }
    }
}
