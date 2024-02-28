using Serilog;

namespace EcleticaBeerControl.Job.DevicesConnectivityCheck
{
    internal class ExecutionTests
    {
        private Timer? timer;

        public ExecutionTests()
        {
            var context = new Context
            {
                StepChain = new Step("Primária")
                {
                    DuePeriod = TimeSpan.Zero,
                    Next = new Step("Secundária")
                    {
                        DuePeriod = TimeSpan.FromSeconds(5),
                        Next = new Step("Terciária")
                        {
                            DuePeriod = TimeSpan.FromSeconds(10),
                            Next = new Step("Cold Crash")
                            {
                                DuePeriod = TimeSpan.FromSeconds(5),
                            }
                        }
                    }
                }
            };

            timer = new Timer(TimerCallback, context, context.StepChain.DuePeriod, TimeSpan.Zero);
        }

        private void TimerCallback(object? state)
        {
            var context = (Context)state!;

            context.StepChain!.Execute();
            Interlocked.Exchange(ref context.StepChain, context.StepChain!.Next);

            if (context.StepChain != null)
                timer!.Change(context.StepChain.DuePeriod, TimeSpan.Zero);
            else
                timer!.Dispose();
        }
    }

    class Context
    {
        public Step? StepChain;
    }
    class Step
    {
        public Step(string title)
        {
            Title = title;
        }
        public string Title { get; init; }
        public TimeSpan DuePeriod { get; set; }
        public Step? Next { get; set; }
        public void Execute()
        {
            Log.Information("Executing {Step} at: {Now} in: {ThreadId}", Title, DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
