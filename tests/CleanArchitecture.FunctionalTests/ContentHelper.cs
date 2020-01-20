using System.Net.Http;
using System.Text;

namespace CleanArchitecture.FunctionalTests
{
    public static class ContentHelper
    {
        //public static StringContent GetStringContent(object obj)
        //    => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        public static StringContent GetStringContent(string jsonString)
    => new StringContent(jsonString, Encoding.UTF8, "application/json");
    }
}
