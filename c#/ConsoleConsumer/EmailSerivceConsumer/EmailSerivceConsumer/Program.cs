using EmailSerivceConsumer.Eventos.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Nodes;

var server = "localhost";
var userName = "guest";
var password = "guest";
var queue = "Var.Notificacao.Email";
var exchange = "Var.Notificacao";
var routingKey = "Var.Notificacao";

var factory = new ConnectionFactory()
{
    HostName = server,
    UserName = userName,
    Password = password,
};

var conn = factory.CreateConnection();
using var channel = conn.CreateModel();

channel.QueueDeclare(queue, true, false, true);
channel.ExchangeDeclare(exchange, "topic" , true, true);
channel.QueueBind(queue, exchange, routingKey);


var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

consumer.Received += ConsumirMensagem;

    void ConsumirMensagem(object sender, BasicDeliverEventArgs e)
{
    var eventoString = Encoding.UTF8.GetString(e.Body.ToArray());
    var evento = JsonConvert.DeserializeObject<EsqueciSenhaModel>(eventoString);


}










