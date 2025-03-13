using Services.Parser;

namespace TestService
{
    public class UnitTest1
    {
        public UnitTest1()
        {

        }
        public static string _command = "  create database йоха";

        [Fact]
        public void Trim()
        {   // Arrange

            var start = 0;
            var span = _command.AsSpan();
            // Act
            var result = ParseRequest.Trim(ref span, start);

            // Assert
            Assert.Equal(2, result);
        }
        [Fact]
        public void GetFirstChar()
        {
            // Arrange
            var span = _command.AsSpan();// "  create database йоха".AsSpan();
            var start = 0;
            // Act
            var result = ParseRequest.GetFirstChar(ref span, start);
            Console.WriteLine(result.Item1);
            Console.WriteLine(result.Item2);
            // Assert
        }
        [Fact]
        public void GetKeyword()
        {
            // Arrange
            var span = _command.AsSpan();
            var start = 0;
            char? key;

            // Act
            (key, start) = ParseRequest.GetFirstChar(ref span, start);
            var result = ParseRequest.GetKeyword(ref span, start, key);
            // Assert
        }
        [Fact]
        public void GetToken()
        {
            var span = _command.AsSpan();
            var start = 0;
            var token = ParseRequest.ParseToken(ref span, start);
            
        }
    }
}
