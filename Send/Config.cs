using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitSN
{
    public class Config : IDisposable
    {
        #region params

        private string _hostname = "localhost";
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        #endregion
        public string HostName => _hostname;
        public ConnectionFactory Factory => _factory;
        public IModel Channel => _channel;

        public Config()
        {
            _factory = new ConnectionFactory() {HostName = _hostname};
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
