
using DAL.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.CreateTestData.RandomApi.Domain;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.CreateTestData
{
    class Program
    {
        static void Main(string[] args)
        {

            CreateCatagories();
            CreateArtists();
            CreateVenues();
            CreatePerformances();
            
           

            Console.ReadKey();
        }
        private static RootObject FetchPersons()
        {
            string requestUrl = "http://api.randomuser.me/?results=60";
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());

                RootObject jsonResponse
                    = objResponse as RootObject;
                return jsonResponse;
            }

        }
        private static void CreatePerformances()
        {
            IVenueDao venueDao = DALFactory.CreateVenueDao(DALFactory.CreateDatabase());
            IPerformanceDao performanceDao = DALFactory.CreatePerformanceDao(DALFactory.CreateDatabase());
            IArtistDao artistDao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());

            for(int i=0; i<3; i++)
            {
                DateTime time = DateTime.Now;
                for (int j = 1; j <= 40; j++)
                {
                    Venue venue = venueDao.findById(j);
                    Artist artist = artistDao.findById(j);
                    Performance p = new Performance();
                    p.Artist = artist;
                    p.Venue = venue;
                    p.StagingTime = time;
                }
                time.AddDays(1);
            }
           
        }
        private static void CreateCatagories()
        {
            ICatagoryDao catagoryDao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            for (int i = 1; i <= 10; i++)
            {
                Catagory c = new Catagory();
                c.Name = "Catagory " + i;
                c.Description = "Catagory Description for " + i;
                catagoryDao.Insert(c);
            }
        }
        private static void CreateArtists()
        {
            Random r = new Random();
            RootObject generatedEntries = FetchPersons();
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            ICatagoryDao catagoryDao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());

            generatedEntries.results.ToList().ForEach(x => {
                Console.WriteLine(x.user.name.first);
                Artist artist = new Artist();
                artist.Catagory = catagoryDao.findById(r.Next(1, 10));
                artist.Name = x.user.name.first + " " + x.user.name.last;
                artist.Email = x.user.email;
                artist.Country = "AUT";
                artist.Picture = GetBase64EncodedImageFromUrl(x.user.picture.thumbnail);
                dao.Insert(artist);
                Console.WriteLine("Inserted ");

            });
        }
        private static void CreateVenues()
        {

            Random rand = new Random();
            IVenueDao venueDao = DALFactory.CreateVenueDao(DALFactory.CreateDatabase());
            for (int i = 1; i <= 60; i++)
            {
                Venue v = new Venue();
                v.Address = "My Random Place " + i;
                v.Description = "This is Random Stage No " + 1;
                v.ShortDescription = "Random Stage " + 1;
                v.Latitude = rand.Next(1, 100000);
                v.Longitude = rand.Next(1, 100000);
                venueDao.Insert(v);
            }
        }

        private static Image ImageFromUrl(string url)
        {
            HttpWebRequest imageRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            imageRequest.Credentials = CredentialCache.DefaultCredentials;
            imageRequest.Proxy = new WebProxy();

            using (HttpWebResponse imageReponse = (HttpWebResponse)imageRequest.GetResponse())
            {
                using (Stream imageStream = imageReponse.GetResponseStream())
                {
                    
                    return Image.FromStream(imageStream);
                }
            }
        }

        private static String GetBase64EncodedImageFromUrl(string url)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Imaging.ImageFormat format = ImageFormat.Jpeg;
                // Convert Image to byte[]
                Image i = ImageFromUrl(url);
                i.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }

        }



    }
}
