using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace RentAVehicle.Testing.Builders
{
    public class StringContentBuilder
    {
        private dynamic _content;

        public StringContentBuilder(dynamic content)
        {
            _content = content;
        }

        public StringContent Build()
        {
            return new StringContent(JsonConvert.SerializeObject(_content), Encoding.UTF8, "application/json");
        }
    }
}
