using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using try_cb_dotnet.Models;
using Couchbase.N1QL;
using Couchbase.Linq.Extensions;
using Newtonsoft.Json.Linq;
using Couchbase;

namespace try_cb_dotnet.Controllers
{
    public class FlightPathController : ApiController
    {
        [HttpGet]
        [ActionName("findAll")]
        public object FindAll(string from, DateTime leave, string to, string token)
        {
            string queryFrom = null;
            string queryTo = null;
            var queryLeave = (int)leave.DayOfWeek;

            // raw query
            var query1 =
                   new QueryRequest(
                       "SELECT faa as fromAirport, geo FROM `" + CouchbaseConfigHelper.Instance.Bucket + "` " +
                       "WHERE airportname = $from " +
                       "UNION SELECT faa as toAirport, geo FROM `" + CouchbaseConfigHelper.Instance.Bucket + "` " +
                       "WHERE airportname = $to")
                   .AddNamedParameter("from", from)
                   .AddNamedParameter("to", to);

            var partialResult1 = ClusterHelper
                    .GetBucket(CouchbaseConfigHelper.Instance.Bucket)
                    .Query<dynamic>(query1);

            if (partialResult1.Rows.Any())
            {
                foreach (dynamic row in partialResult1.Rows)
                {
                    if (row.fromAirport != null) queryFrom = row.fromAirport;
                    if (row.toAirport != null) queryTo = row.toAirport;
                }
            }

            // raw query
            var query2 =
                   new QueryRequest(
                       "SELECT r.id, a.name, s.flight, s.utc, r.sourceairport, r.destinationairport, r.equipment FROM " +
                       "`" + CouchbaseConfigHelper.Instance.Bucket + "` r " +
                       "UNNEST r.schedule s JOIN " +
                       "`" + CouchbaseConfigHelper.Instance.Bucket + "` " +
                       "a ON KEYS r.airlineid WHERE r.sourceairport=$from " +
                       "AND r.destinationairport=$to " +
                       "AND s.day=$leave " +
                       "ORDER BY a.name")
                   .AddNamedParameter("from", queryFrom)
                   .AddNamedParameter("to", queryTo)
                   .AddNamedParameter("leave", queryLeave);

            return ClusterHelper
                    .GetBucket(CouchbaseConfigHelper.Instance.Bucket)
                    .Query<dynamic>(query2)
                    .Rows;
        }
    }
}