using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventGridPublisher
{
    public class Program
    {
        private const string topicEndpoint = "https://ev-gr-t1.westeurope-1.eventgrid.azure.net/api/events";
        private const string topicKey = "ranyiaYyvfoymNRBG+xkJmcZ9kb0Pqvxd/n7NPti9jI=";

        public static async Task Main(string[] args)
        {
            TopicCredentials credentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(credentials);

            List<EventGridEvent> events = new List<EventGridEvent>();

            var firstPerson = new
            {
                FullName = "Alba Sutton",
                Address = "4567 Pine Avenue, Edison, WA 97202"
            };

            EventGridEvent firstEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {firstPerson.FullName}",
                Data = firstPerson.ToString(),
                DataVersion = "1.0.0"
            };
            events.Add(firstEvent);

            var secondPerson = new
            {
                FullName = "Alexandre Doyon",
                Address = "456 College Street, Bow, WA 98107"
            };

            EventGridEvent secondEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Employees.Registration.New",
                EventTime = DateTime.Now,
                Subject = $"New Employee: {secondPerson.FullName}",
                Data = secondPerson.ToString(),
                DataVersion = "1.0.0"
            };
            events.Add(secondEvent);


            string topicHostname = new Uri(topicEndpoint).Host;
            await client.PublishEventsAsync(topicHostname, events);

            Console.WriteLine("Events published");

        }
    }
}
