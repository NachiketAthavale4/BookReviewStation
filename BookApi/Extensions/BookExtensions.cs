using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BookApi.Extensions
{
    public static class BookExtensions
    {
        public static string JsonSerialize(this object objectToBeSerialized)
        {
            return JsonConvert.SerializeObject(objectToBeSerialized);
        }
    }
}