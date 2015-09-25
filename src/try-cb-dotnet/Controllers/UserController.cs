using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using try_cb_dotnet.Models;
using Couchbase;

namespace try_cb_dotnet.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [ActionName("Login")]
        public object Login(string password, string user)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "Login" web api call to return a
            /// "success" status code containing a JWT token. 
            /// The JWT token is used to reference and store data about the user's trips/bookings and login credentials.
            /// Response should be in a json format like this:
            /// Round trip: 
            /// [{"success":"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU"}]

            /// Implement the method to return a "success" allowing the user to login.
            /// 
            /// step 1:
            ///     Use ClusterHelper to look-up the user document and validate the login.
            ///     If the login is valid then return 
            ///     "Success" : token
            ///     If not, return 
            ///     "Success" : false
            /// hint:
            /// The user document is stored under the key: "profile::" + user in the "default" bucket.

            return new
            {
                success = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU"
            };
        }

        [HttpPost]
        [ActionName("Login")]
        public object CreateLogin([FromBody] UserModel user)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "Login" web api call to return a
            /// "success" status code containing a JWT token. 
            /// The JWT token is used to reference and store data about the user's trips/booking and login credentials.
            /// Response should be in a json format like this:
            /// Round trip: 
            /// [{"success":"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU"}]

            /// Implement the method to create and return a valid JWT given the username and password.
            /// Be sure to check if a user already exisits and fail in that case by returning "success" : false
            /// 
            /// step 1: 
            ///     Check if user document already exisist and fail if it does.
            /// step 2:
            ///     Using this Nuget JWT library:
            ///     https://www.nuget.org/packages/JWT
            ///     Implement JWT token support:
            ///     The secret key to use for encryption and hashing is:
            ///     "UNSECURE_SECRET_TOKEN"
            ///     Store this key in Web.Config.
            /// step 3:
            ///     Store the generated JWT token under the user document key:
            ///     "profile::user" in the "default" bucket.
            /// step 4:
            ///     Update the method to return the actual JWT token created in step 2.
            /// 
            /// Hint: 
            /// Use CLusterHelper to store the document. 
            
            return new
            {
                success = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU"
            };
        }

        [HttpGet]
        [ActionName("flights")]
        public object Flights(string token)
        {
            return ClusterHelper
                    .GetBucket(CouchbaseConfigHelper.Instance.Bucket)
                    .Get<dynamic>("bookings::" + token)
                    .Value;
        }

        [HttpPost]
        [ActionName("flights")]
        public object BookFlights([FromBody] dynamic request)
        {
            List<FlightModel> flights = new List<FlightModel>();

            foreach (var flight in request.flights)
            {
                flights.Add(new FlightModel
                {
                    name = flight._data.name,
                    bookedon = DateTime.Now.ToString(),
                    date = flight._data.date,
                    destinationairport = flight._data.destinationairport,
                    sourceairport = flight._data.sourceairport
                });
            }

             ClusterHelper
                .GetBucket(CouchbaseConfigHelper.Instance.Bucket)
                .Upsert("bookings::" + request.token, flights);

            return new { added = flights.Count };
        }
    }
}