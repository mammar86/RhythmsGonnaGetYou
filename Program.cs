using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
    }

    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }


    }


    class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Band> Bands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Rhythm");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();
            var albums = context.Albums.Include(album => album.Band);
            var bands = context.Bands;

            var quit = false;

            // foreach (var band in bands)
            // {
            //     Console.WriteLine(band.Name);
            // }

            while (quit != true)
            {
                Console.WriteLine("Please choose one of the following");
                Console.WriteLine();
                Console.WriteLine("Add : Add a new band");
                Console.WriteLine("View: View all the bands");
                Console.WriteLine("New: Add an album for a band");
                Console.WriteLine("Let: Let a band go");
                Console.WriteLine("Resign: Resign a band");
                Console.WriteLine("Prompt: Prompt for a band name and view all their albums");
                Console.WriteLine("VD: View all albums ordered by ReleaseDate");
                Console.WriteLine("VA: View all bands that are signed");
                Console.WriteLine("VNS: View all bands that are not signed");
                Console.WriteLine("Quit: Quit the program");
                var choice = Console.ReadLine().ToLower();

                if (choice == "quit")
                {
                    Console.WriteLine("===Good bye===");
                    quit = true;
                }

                else if (choice == "add")
                {
                    Console.Write("Band name: ");
                    var bandName = Console.ReadLine();

                    Console.Write("Country of origin: ");
                    var origin = Console.ReadLine();

                    Console.Write("Number of band members: ");
                    var membersNumber = int.Parse(Console.ReadLine());

                    Console.Write("Band's website: ");
                    var website = Console.ReadLine();

                    Console.Write("Band's Style: ");
                    var style = Console.ReadLine();

                    Console.Write("Is the band signed? True|False: ");

                    var isSigned = Boolean.Parse(Console.ReadLine().ToLower());

                    Console.Write("Is the band has a contact name? True|False : ");
                    var response = Console.ReadLine().ToLower();
                    string contactName = null;
                    string contactPhone = null;
                    if (response == "yes")
                    {
                        Console.Write("Contact name: ");
                        contactName = Console.ReadLine();
                        Console.Write("Contact phone: ");
                        contactPhone = Console.ReadLine();
                    }

                    var newBand = new Band
                    {
                        Name = bandName,
                        CountryOfOrigin = origin,
                        NumberOfMembers = membersNumber,
                        Website = website,
                        Style = style,
                        IsSigned = isSigned,
                        ContactName = contactName,
                        ContactPhoneNumber = contactPhone
                    };
                    bands.Add(newBand);
                    context.SaveChanges();

                    Console.WriteLine();
                    Console.WriteLine("Is there is anything else you need to do? Y to Continue N to Exit: ");
                    var res = Console.ReadLine().ToLower();
                    if (res == "n")
                    {
                        Console.WriteLine("===Good bye===");
                        quit = true;
                    }


                }
                else if (choice == "view")
                {
                    foreach (var band in bands)
                    {
                        Console.WriteLine(band.Name);
                    }

                }
            }
        }
    }

}
