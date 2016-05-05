namespace SalusCmd.Ecm6
{
    using Salus.Infra.Logs;
    using System;
    using System.Diagnostics;
    public class Measure : IDisposable
    {
        private string message;
        private TimeSpan medicao;
        private Stopwatch sw;
        
        public Measure(string message)
        {
            this.sw = new Stopwatch();
            this.sw.Start();
            this.message = message;
            Log.App.InfoFormat(message);
        }

        public void Dispose()
        {
            Log.App.InfoFormat("{0} terminou em {1}s", this.message, this.medicao.Seconds);
        }

        public void Start(string message)
        {
            this.message = message;
            this.sw = new Stopwatch();
            this.sw.Start();
            Log.App.InfoFormat("[Medindo]: {0}", this.message);
        }

        public void Stop()
        {
            this.sw.Stop();
            Log.App.InfoFormat("[Medindo]: {0} terminou em {1}s", this.message, this.sw.Elapsed.Seconds);
            this.message = string.Empty;
        }
    }
}