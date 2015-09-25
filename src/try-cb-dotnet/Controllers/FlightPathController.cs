using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using try_cb_dotnet.Models;
using Newtonsoft.Json.Linq;

namespace try_cb_dotnet.Controllers
{
    public class FlightPathController : ApiController
    {
        [HttpGet]
        [ActionName("findAll")]
        public object FindAll(string from, DateTime leave, string to, string token)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "findAll" web api call to return a
            /// "trip" in a json format like this:
            /// Round trip: 
            /// [{"destinationairport":"SFO","equipment":"738","flight":"AA907","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"00:29:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"738","flight":"AA787","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"19:06:00","flighttime":1,"price":45},{"destinationairport":"SFO","equipment":"738","flight":"AA279","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"04:54:00","flighttime":1,"price":52},{"destinationairport":"SFO","equipment":"E75","flight":"DL856","id":21085,"name":"Delta Air Lines","sourceairport":"LAX","utc":"20:08:00","flighttime":1,"price":47},{"destinationairport":"SFO","equipment":"E75","flight":"DL273","id":21085,"name":"Delta Air Lines","sourceairport":"LAX","utc":"14:14:00","flighttime":1,"price":48},{"destinationairport":"SFO","equipment":"73W 73C 733","flight":"WN543","id":63986,"name":"Southwest Airlines","sourceairport":"LAX","utc":"22:16:00","flighttime":1,"price":44},{"destinationairport":"SFO","equipment":"73W 73C 733","flight":"WN828","id":63986,"name":"Southwest Airlines","sourceairport":"LAX","utc":"04:35:00","flighttime":1,"price":43},{"destinationairport":"SFO","equipment":"738","flight":"US086","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"15:06:00","flighttime":1,"price":46},{"destinationairport":"SFO","equipment":"738","flight":"US150","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"15:44:00","flighttime":1,"price":47},{"destinationairport":"SFO","equipment":"738","flight":"US437","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"23:42:00","flighttime":1,"price":52},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA666","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"05:11:00","flighttime":1,"price":44},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA978","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"19:50:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA123","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"21:13:00","flighttime":1,"price":49},{"destinationairport":"SFO","equipment":"320 319","flight":"VX929","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"00:39:00","flighttime":1,"price":49},{"destinationairport":"SFO","equipment":"320 319","flight":"VX351","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"01:37:00","flighttime":1,"price":49},{"destinationairport":"SFO","equipment":"320 319","flight":"VX703","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"05:01:00","flighttime":1,"price":47},{"destinationairport":"SFO","equipment":"320 319","flight":"VX743","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"10:36:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"320 319","flight":"VX301","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"01:32:00","flighttime":1,"price":49}]
            ///
            /// One way trip:
            /// [{"destinationairport":"SFO","equipment":"738","flight":"AA787","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"19:06:00","flighttime":1,"price":48},{"destinationairport":"SFO","equipment":"738","flight":"AA279","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"04:54:00","flighttime":1,"price":49},{"destinationairport":"SFO","equipment":"738","flight":"AA907","id":5746,"name":"American Airlines","sourceairport":"LAX","utc":"00:29:00","flighttime":1,"price":51},{"destinationairport":"SFO","equipment":"E75","flight":"DL273","id":21085,"name":"Delta Air Lines","sourceairport":"LAX","utc":"14:14:00","flighttime":1,"price":51},{"destinationairport":"SFO","equipment":"E75","flight":"DL856","id":21085,"name":"Delta Air Lines","sourceairport":"LAX","utc":"20:08:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"73W 73C 733","flight":"WN543","id":63986,"name":"Southwest Airlines","sourceairport":"LAX","utc":"22:16:00","flighttime":1,"price":50},{"destinationairport":"SFO","equipment":"73W 73C 733","flight":"WN828","id":63986,"name":"Southwest Airlines","sourceairport":"LAX","utc":"04:35:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"738","flight":"US086","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"15:06:00","flighttime":1,"price":53},{"destinationairport":"SFO","equipment":"738","flight":"US150","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"15:44:00","flighttime":1,"price":43},{"destinationairport":"SFO","equipment":"738","flight":"US437","id":59532,"name":"US Airways","sourceairport":"LAX","utc":"23:42:00","flighttime":1,"price":48},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA978","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"19:50:00","flighttime":1,"price":48},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA666","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"05:11:00","flighttime":1,"price":51},{"destinationairport":"SFO","equipment":"739 752 753 319 320 738","flight":"UA123","id":57010,"name":"United Airlines","sourceairport":"LAX","utc":"21:13:00","flighttime":1,"price":50},{"destinationairport":"SFO","equipment":"320 319","flight":"VX743","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"10:36:00","flighttime":1,"price":48},{"destinationairport":"SFO","equipment":"320 319","flight":"VX703","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"05:01:00","flighttime":1,"price":45},{"destinationairport":"SFO","equipment":"320 319","flight":"VX301","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"01:32:00","flighttime":1,"price":49},{"destinationairport":"SFO","equipment":"320 319","flight":"VX929","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"00:39:00","flighttime":1,"price":44},{"destinationairport":"SFO","equipment":"320 319","flight":"VX351","id":62018,"name":"Virgin America","sourceairport":"LAX","utc":"01:37:00","flighttime":1,"price":46}]

            /// Implement the method to return all trips that match the selected source and destination airport for the given date interval.

            /// HINT: Use N1QL to query the "travel-sample" bucket in Couchbase to find matching "trips"
            /// The travel sample does not contain any "trip" documents you will need to use "join" to build a matcing result-set.
            /// This is a two step proccess/query.
            /// 
            /// step 1: 
            ///     This API method is called with the "from" and "to" values witch represent the full airport name.
            ///     The "join" that we will do in step 2, needs the FAA (3 letter abbrivation for the airport name)
            ///     Therefore the first step is to make a N1QL query to convert the airport name to a FAA 3 letter value 
            ///     for both from and to airport.
            /// 
            /// step 2:
            ///     This is a bit tricky as we will create N1QL query that will use both UNNEST and JOIN. 
            ///     Below you can find a "template" version of the query.
            ///     This will allow you to understand the query in it's full detail and exame the paramters need for this query.
            /// 
            ///     SELECT r.id, a.name, s.flight, s.utc, r.sourceairport, r.destinationairport, r.equipment FROM 
            ///     `travel-sample` r 
            ///     UNNEST r.schedule s 
            ///     JOIN `travel-sample` a 
            ///     ON KEYS r.airlineid 
            ///     WHERE r.sourceairport='LAX' 
            ///     AND r.destinationairport='SFO' 
            ///     AND s.day=1
            ///     ORDER BY a.name  

            return new List<dynamic>
            {
                new { destinationairport="SFO",equipment=738,flight="AA907",id=5746,name="American Airlines",sourceairport="LAX",utc="00:29:00",flighttime=1,price=53},
                new { destinationairport="SFO",equipment=738,flight="AA907",id=5746,name="American Airlines",sourceairport="LAX",utc="00:29:00",flighttime=1,price=53},
                new { destinationairport="SFO",equipment=738,flight="AA907",id=5746,name="American Airlines",sourceairport="LAX",utc="00:29:00",flighttime=1,price=53}
            };
        }
    }
}