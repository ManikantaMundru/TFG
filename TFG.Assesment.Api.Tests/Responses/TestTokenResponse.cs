

namespace TFG.Assesment.Api.Tests.Responses
{
    public class TestTokenResponse
    {
        public Result Result { get; set; }
    }
    public class Result
    {
        public string JwtToken { get; set; }
    }
}
