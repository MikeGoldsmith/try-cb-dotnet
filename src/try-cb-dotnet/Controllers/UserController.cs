using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using try_cb_dotnet.Models;

namespace try_cb_dotnet.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [ActionName("Login")]
        public object Login(string password, string user)
        {
            /// Task:
            /// No changes to this code yet
            /// 
            /// Later we will implement a JWT token issuer and store user data in Couchbase for later look-up.
            /// The token is created for the user:
            /// username: guest
            /// passowrd: guest

            return new { success = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU" };
        }

        [HttpPost]
        [ActionName("Login")]
        public object CreateLogin([FromBody] UserModel user)
        {
            /// Task:
            /// No changes to this code yet
            /// 
            /// Later we will implement a JWT token issuer and store user data in Couchbase for later look-up.
            /// The token is created for the user:
            /// username: guest
            /// passowrd: guest
            
            return new { success = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiZ3Vlc3QiLCJpYXQiOjE0NDE4Njk5NTR9.5jPBtqralE3W3LPtS - j3MClTjwP9ggXSCDt3 - zZOoKU" };
        }

        [HttpGet]
        [ActionName("flights")]
        public object Flights(string token)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "flights" web api call to return a
            /// all bookings done by this user. 
            /// The JWT token is used to look-up the user and find all bookings.
            /// Response should be in a json format like this:
            /// Bookings:
            ///  [{"_type":"Flight","_id":"d500a3d1-2cca-43a5-8a66-f11828a35969","name":"American Airlines","flight":"AA344","date":"09/10/2015","sourceairport":"SFO","destinationairport":"LAX","bookedon":"1441881827622"},{"_type":"Flight","_id":"bf676b0d-e63b-4ff6-aade-7ac1c182b3de","name":"American Airlines","flight":"AA787","date":"09/11/2015","sourceairport":"LAX","destinationairport":"SFO","bookedon":"1441881827623"},{"_type":"Flight","_id":"f0099c24-3ad4-482e-8352-704f9cbf1a43","name":"American Airlines","flight":"AA550","date":"09/10/2015","sourceairport":"SFO","destinationairport":"LAX","bookedon":"1441881827623"}]
            ///
            /// Implement the method to return all bookings for the logged-in user.
            /// 
            /// step 1:
            ///     Use the token to look-up the "bookings" document for the user and return all bookings.
            ///     The document key is in the format
            ///     bookings::token
            ///     The great thing with using Cocuhbase (a json document store) is that we dont need to convert the value,
            ///     we can just return the actual document stores in Couchbase.
            ///  
            /// Hint: 
            ///     Use ClusterHelper to Get document in the "default" bucket and retur the value. 

            return new List<dynamic>
            {
                new {_type="Flight",_id="f0099c24-3ad4-482e-8352-704f9cbf1a43",name="American Airlines",flight="AA550",date="09/10/2015",sourceairport="SFO",destinationairport="LAX",bookedon=1441881827623},
                new {_type="Flight",_id="f0099c24-3ad4-482e-8352-704f9cbf1a43",name="American Airlines",flight="AA550",date="09/10/2015",sourceairport="SFO",destinationairport="LAX",bookedon=1441881827623},
                new {_type="Flight",_id="f0099c24-3ad4-482e-8352-704f9cbf1a43",name="American Airlines",flight="AA550",date="09/10/2015",sourceairport="SFO",destinationairport="LAX",bookedon=1441881827623},
            };
        }

        [HttpPost]
        [ActionName("flights")]
        public object BookFlights([FromBody] dynamic request)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "flights" web api call to save the selected flight in a booking's document.
            /// 
            /// The JWT token is used as a key to the users bookings.
            /// In this fake implementation we are not going to use the Token, nor store any data about the bookings.
            /// Instead we return a static value to indicate that the bokking was successfull.
            /// Response should be in a json format like this:
            /// Bookings:
            /// {"added":3}
            /// 
            /// Implement the method to return the the number of successfull bookings that where persisted in couchbase 
            /// for the user (token)
            /// 
            /// step 1:
            ///     First we need to get an understanding of the "dynamic request" value. 
            ///     Add a breakpoint to this method and use "immidiate window" in Visual studio to investigate the request value.
            ///     "request" is a list of dynamic "JToken" values. It's therefore possible to "foreach" over the collection and "select" 
            ///     the the relevant values and store them directly in Couchbase.
            /// step 2:
            ///     Create a foreach loop to itterate over the request collection and store all bookings to Couchbase.
            ///     the bookings should be stored in a document with the compound key
            ///     bookings::{token}
            ///     The token is avalible in the request, request.token
            /// 
            /// step 3:
            ///     Update the return value to reflect the actual number of stored bookings    
            /// 
            /// Hint: 
            ///     Use ClusterHelper to Upsert the document in the "default" bucket. 

            return new { added = 3 };
        }
    }
}