namespace SalusCmd.Ecm6
{
    using Salus.Infra.Logs;
    using System;

    public class Measure : IDisposable
    {
        private string message;
        private TimeSpan medicao;

        public Measure()
        {
        }

        public Measure(string message)
        {
            this.medicao = new TimeSpan();
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
            this.medicao = new TimeSpan();
            Log.App.InfoFormat("[Medindo]: {0}", this.message);
        }

        public void Stop()
        {
            Log.App.InfoFormat("[Medindo]: {0} terminou em {1}s", this.message, this.medicao.Seconds);
            this.message = string.Empty;
        }
    }
}