namespace aspnet_logging_02_vs
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Internal;

    public class MyClass
    {
        private readonly ILogger<MyClass> _logger;

        public MyClass(ILogger<MyClass> logger)
        {
            _logger = logger;
        }

        public void DoSomething(int input)
        {
            var values = new FormattedLogValues("Starting to do something with input: {0}", input);

            _logger.LogDebug(values);

            if (input >= -1 && input <= 1)
            {
                _logger.LogInformation("Input of {0} is within the optimal range.", input);
            }
            else if (input > 10)
            {
                _logger.LogWarning("Input of {0} is greater than the typical range.", input);
            }
            else if (input < -10)
            {
                _logger.LogError("Input of {0} is less than the typical range.", input);
            }

            _logger.LogDebug("Finished doing something with input: {0}", input);
        }
    }
}