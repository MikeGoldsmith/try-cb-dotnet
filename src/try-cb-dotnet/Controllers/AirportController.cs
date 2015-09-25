using System.Collections.Generic;
using System.Web.Http;

namespace try_cb_dotnet.Controllers
{
    public class AirportController : ApiController
    {
        [HttpGet]
        [ActionName("findAll")]
        public object FindAll(string search, string token)
        {
            /// Task:
            /// This is a Web API call, a method that is called from the static html (index.html).
            /// The js in the static html expectes this "findAll" web api call to return a
            /// "airportname" in a json format like this:
            /// [{"airportname":"San Francisco Intl"}]
            /// 
            /// Implement the method to return a list of airport names that match the "search" string
            /// and return the result list.
            /// The result list is used by the UI to show a drop-down of matching airport names the user can select from.
            /// 
            /// Hint: use N1QL to query the "travel-sample" bucket in Couchbase and return all matching airport names. 
            return new List<dynamic>()
            {
                new {airportname = "San Francisco Intl"}
            };
        }
    }
}